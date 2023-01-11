using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Action("seek")]
[Help("Seek from Character")]
public class SeekBB : BasePrimitiveAction
{
    [InParam("thisGameObject")]
    [Help("this gameobject")]
    public GameObject thisGameobject;

    public override void OnStart()
    {
        BasicVillager basicVillager = thisGameobject.GetComponent<BasicVillager>();
        basicVillager.behaviour = Behaviour.SEEK;
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.COMPLETED;
    }
}
