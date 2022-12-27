using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Action("flee")]
[Help("Flee from Character")]
public class FleeBB : BasePrimitiveAction
{
    [InParam("thisGameObject")]
    [Help("this gameobject")]
    public GameObject thisGameobject;

    public override void OnStart()
    {
        GameObject chaser = thisGameobject.GetComponent<Robber>().chasedBy;
        Moves moves = thisGameobject.GetComponent<Moves>();
        moves.target = chaser;
        moves.Evade();
    }

    public override TaskStatus OnUpdate()
    {
        
        return TaskStatus.COMPLETED;
    }
}
