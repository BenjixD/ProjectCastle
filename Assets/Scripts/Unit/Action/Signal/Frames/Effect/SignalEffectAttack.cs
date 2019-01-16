using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SignalEffectAttack : FrameEffect
{ 
    public SignalEffectAttack(Action instance) : base(instance) { }

    public override bool CanExecute(SimulatedDisplacement sim, Direction dir, Board board)
    {
        // TODO Fail upon stun or silence
        return true;
    }

    public override bool ExecuteEffect(SimulatedDisplacement sim, Direction dir, Board board)
    {
        // TODO get targetted area
        Tile target = ((Signal)action).Target;
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
            target.unit.TakeDamage(Signal.CENTERDMG);
            AddUnitHit(target.unit);
            Debug.Log("Ouch! " + target.unit.unitName + " just took " + Signal.CENTERDMG + " damage!");
        }
        foreach(Tile t in aoe)
        {
            if (t.unit && !IsAlreadyHit(t.unit))
            {
                t.unit.TakeDamage(Signal.AOEDMG);
                AddUnitHit(t.unit);
                Debug.Log("Ouch! " + t.unit.unitName + " just took " + Signal.AOEDMG + " damage!");
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
