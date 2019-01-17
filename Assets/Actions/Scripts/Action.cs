using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : MonoBehaviour {

    float castTime;

    public float CastTime {
        get {
            return castTime;
        }
    }

    public abstract void Execute();
}
