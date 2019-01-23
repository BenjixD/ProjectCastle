using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SignalEffectAttack : FrameEffect
{
	Dictionary<HitboxType, int> damage = new Dictionary<HitboxType, int>()
	{
		{ HitboxType.SWEET, 20 },
		{ HitboxType.OK, 10 }
	};

	public SignalEffectAttack(Action instance) : base(instance) { }

    public override bool CanExecute(SimulatedDisplacement sim, Direction dir, Board board)
    {
        // TODO Fail upon stun or silence
        return true;
    }

    public override bool ExecuteEffect(SimulatedDisplacement sim, Direction dir, Board board)
    {
        // TODO get targetted area
        Tile target = ((Signal)action).target;
        List<Tile> aoe = new List<Tile>();
        if (board.CheckCoord(target.coordinate + Vector2.up))
        {
            aoe.Add(board.GetTile(target.coordinate + Vector2.up));
        }
        if (board.CheckCoord(target.coordinate + Vector2.down))
        {
            aoe.Add(board.GetTile(target.coordinate + Vector2.down));
        }
        if (board.CheckCoord(target.coordinate + Vector2.left))
        {
            aoe.Add(board.GetTile(target.coordinate + Vector2.left));
        }
        if (board.CheckCoord(target.coordinate + Vector2.right))
        {
            aoe.Add(board.GetTile(target.coordinate + Vector2.right));
        }
        
        if (target.unit && !IsAlreadyHit(target.unit))
        {
            target.unit.TakeDamage(damage[HitboxType.SWEET]);
            AddUnitHit(target.unit);
            Debug.Log("Ouch! " + target.unit.unitName + " just took " + damage[HitboxType.SWEET] + " damage!");
        }
        foreach(Tile t in aoe)
        {
            if (t.unit && !IsAlreadyHit(t.unit))
            {
                t.unit.TakeDamage(damage[HitboxType.OK]);
                AddUnitHit(t.unit);
                Debug.Log("Ouch! " + t.unit.unitName + " just took " + damage[HitboxType.OK] + " damage!");
            }
        }
        return true;
    }

    private bool IsAlreadyHit(Unit unit)
    {
        Signal parentAction = (Signal)action;
        return parentAction.victims.Contains(unit);
    }

    private void AddUnitHit(Unit unit)
    {
        Signal parentAction = (Signal)action;
        parentAction.victims.Add(unit);
    }
}
