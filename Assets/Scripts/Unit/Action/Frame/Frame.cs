using UnityEngine;
using System.Collections;

public abstract class Frame {
	public Direction relativeDir;
    public GameObject icon { get; set; }
    public abstract bool Execute(Unit unit, Direction dir, Board board);
}