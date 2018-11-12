using UnityEngine;
using System.Collections;

public abstract class Frame {
	public Direction relativeDir;
	public abstract bool Execute(Unit unit, Direction dir, Board board);
}