using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Action("Hide")]
[Help("Hide from Character")]
public class HideBB : BasePrimitiveAction
{
    [InParam("thisGameObject")]
    [Help("this gameobject")]
    public GameObject thisGameobject;

    public override void OnStart()
    {
        BasicVillager basicVillager = thisGameobject.GetComponent<BasicVillager>();
        basicVillager.behaviour = Behaviour.HIDE;
    }

    public override TaskStatus OnUpdate()
    {

        return TaskStatus.COMPLETED;
    }
}

