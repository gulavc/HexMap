using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionQueue {

    List<Action> actionQueue;
    
    void Add(Action a) {
        actionQueue.Add(a);        
    }    

    Action RemoveLast() {
        int lastIndex = actionQueue.Count - 1;
        Action a = actionQueue[lastIndex];
        actionQueue.RemoveAt(lastIndex);
        return a;
    }

    

    void Execute() {
        foreach(Action a in actionQueue) {
            a.Execute();
        }
        actionQueue.Clear();
    }

    public float TotalCastTime() {
        float castTime = 0f;
        foreach (Action a in actionQueue) {
            castTime += a.CastTime;
        }
        return castTime;
    }

}
