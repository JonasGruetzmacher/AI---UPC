using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Action("MyAction/GetRobber")]
[Help("Get the robber that stole a wallet")]
public class GetRobberBB : BasePrimitiveAction
{
    [InParam("shout")]
    [Help("Shout of the victim")]
    public GameObject shout;

    [OutParam("robber")]
    [Help("GameObject of the robber")]
    public GameObject robber;

    public override void OnStart()
    {
        robber = shout.GetComponent<shout>().robber;
    }
    public override TaskStatus OnUpdate()
    {
        return TaskStatus.COMPLETED;
    }
}
