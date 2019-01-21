using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncinerateFrameEffectAttack : FrameEffect
{
	private Dictionary<Vector2, HitboxType> targetTiles;

	public IncinerateFrameEffectAttack(Action instance) : base(instance) { }

	public override bool CanExecute(SimulatedDisplacement sim, Direction dir, Board board)
	{
		// TODO Fail upon silence
		if (sim.displacement.unit.statusController.HasStatus(new StunEffect(0))) {
			return false;
		}
		return true;
	}

	private void InitializeTargetTiles()
	{
		targetTiles = new Dictionary<Vector2, HitboxType>();
		targetTiles.Add(Vector2.zero, HitboxType.OK);
		targetTiles.Add(Vector2.up, HitboxType.OK);
		targetTiles.Add(Vector2.down, HitboxType.OK);
		targetTiles.Add(Vector2.left, HitboxType.OK);
		targetTiles.Add(Vector2.right, HitboxType.OK);
		targetTiles.Add(Vector2.up + Vector2.left, HitboxType.SOUR);
		targetTiles.Add(Vector2.up + Vector2.right, HitboxType.SOUR);
		targetTiles.Add(Vector2.down + Vector2.left, HitboxType.SOUR);
		targetTiles.Add(Vector2.down + Vector2.right, HitboxType.SOUR);
	}

	public override bool ExecuteEffect(SimulatedDisplacement sim, Direction dir, Board board)
	{
		InitializeTargetTiles();
		Vector2 targetCoord = ((TargetAreaAction)action).target.coordinate;
		foreach (KeyValuePair<Vector2, HitboxType> pair in targetTiles) {
			if (board.CheckCoord(targetCoord + pair.Key)) {
				Tile t = board.GetTile(targetCoord + pair.Key);
				if (t.unit && !IsAlreadyHit(t.unit)) {
					t.unit.TakeDamage(((TargetAreaAction)action).damage[pair.Value]);
					AddUnitHit(t.unit);
					Debug.Log("Ouch! " + t.unit.unitName + " just took " + ((TargetAreaAction)action).damage[pair.Value] + " damage!");
				}
			}
		}
		return true;
	}

	private bool IsAlreadyHit(Unit unit) {
		Incinerate parentAction = (Incinerate)action;
		return parentAction.victims.Contains(unit);
	}

	private void AddUnitHit(Unit unit) {
		Incinerate parentAction = (Incinerate)action;
		parentAction.victims.Add(unit);
	}
}
