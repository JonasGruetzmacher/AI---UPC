using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

[Action("MyActions/Shout")]
[Help("The target that was robbet shouts")]
public class ShoutBB : GOAction
{
    [InParam("shoutPrefab")]
    [Help("Shout Prefab")]
    public GameObject shoutPrefab;

    [InParam("target")]
    [Help("Target that was robbed")]
    public GameObject target;

    [InParam("thisGameObject")]
    [Help("This GameObject")]
    public GameObject thisGameObject;

    private GameObject shout;
    public override void OnStart()
    {
        shout = GameObject.Instantiate(shoutPrefab, target.transform.position, Quaternion.identity) as GameObject;
        shout.transform.parent = target.transform;
        shout.GetComponent<shout>().robber = thisGameObject;

    }
}
