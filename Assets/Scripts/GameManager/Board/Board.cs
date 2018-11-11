using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

    public float tileWidth;
    public float tileHeight;

    public static Vector2 CoordsToPosition(int x, int y)
    {
        return new Vector2((y-x)*tileWidth, (x+y)*-tileHeight);
    }
}
