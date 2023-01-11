using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Action("Evade")]
[Help("Evade from Character")]
public class EvadeBB : BasePrimitiveAction
{
    [InParam("thisGameObject")]
    [Help("this gameobject")]
    public GameObject thisGameobject;

    public override void OnStart()
    {
        BasicVillager basicVillager = thisGameobject.GetComponent<BasicVillager>();
        basicVillager.behaviour = Behaviour.EVADE;
    }

    public override TaskStatus OnUpdate()
    {

        return TaskStatus.COMPLETED;
    }
}

