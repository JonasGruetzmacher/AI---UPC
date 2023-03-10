using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Action("wander")]
[Help("Wander")]
public class WanderBB : BasePrimitiveAction
{
    [InParam("thisGameObject")]
    [Help("this gameobject")]
    public GameObject thisGameobject;

    [InParam("wanderRadius")]
    public float wanderRadius;

    public override void OnStart()
    {
        BasicVillager basicVillager = thisGameobject.GetComponent<BasicVillager>();
        basicVillager.behaviour = Behaviour.WANDER;
        basicVillager.wanderRadius = wanderRadius;
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.COMPLETED;
    }
}
