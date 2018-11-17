using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Phase : MonoBehaviour {
    public abstract IEnumerator Play(Game game, IEnumerator next);
}