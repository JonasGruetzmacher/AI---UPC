using Pada1.BBCore.Framework;
using Pada1.BBCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Condition("MyBasic/CheckIfChased")]
[Help("Checks whether two booleans have the same value on a Gameobject")]
public class CheckBoolGOBB : ConditionBase
{
    [InParam("gameObject")]
    [Help("Gameobject to check for")]
    public GameObject thisGameobject;
    public override bool Check()
    {
        return thisGameobject.GetComponent<Robber>().isChased;
    }
    
}
