using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Action("MyActions/Wander")]
[Help("Get the Vector3 for wandering")]
public class WanderBB : BasePrimitiveAction
{
    [InParam("game Object")]
    [Help("this game object")]
    public GameObject thisGameObject;

    [InParam("wander radius")]
    [Help("Wander radius")]
    public float wanderRadius;

    [InParam("layer mask")]
    [Help("Layer mask")]
    public LayerMask layerMask;

    [OutParam("wander")]
    [Help("Vector3 for wandering")]
    public Vector3 wander;

    public override TaskStatus OnUpdate()
    {
        Moves moves = thisGameObject.GetComponent<Moves>();
        wander = moves.WanderValue(wanderRadius,layerMask);
        return TaskStatus.COMPLETED;
    }

}
