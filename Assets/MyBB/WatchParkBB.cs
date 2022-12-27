using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Action("My/Actions/WatchPark")]
[Help("Watches the park for a nearby victim. Returns the robber go")]
public class WatchParkBB : BasePrimitiveAction
{
    [InParam("thisGameObject")]
    [Help("this game object")]
    public GameObject thisGameObject;


    [InParam("shoutDistance")]
    [Help("max distance the cop reacts to a shout")]
    public float hearingDistance;

    [OutParam("robber")]
    [Help("Robber Gameobject")]
    public GameObject robber;

    public override TaskStatus OnUpdate()
    {
        float dist = float.MaxValue;
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Shout"))
        {
            float newdist = (go.transform.position + thisGameObject.transform.position).sqrMagnitude;
            if (newdist < dist)
            {
                dist = newdist;
                robber = go;
            }
        }
        return TaskStatus.COMPLETED;
    }
}
