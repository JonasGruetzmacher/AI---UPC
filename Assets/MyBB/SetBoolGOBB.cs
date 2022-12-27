using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;
using System;

/// <summary>
/// It is a basic action to associate a Boolean to a variable.
/// </summary>
[Action("MyAction/SetBoolChased")]
[Help("Sets a value to a boolean variable on an game object")]
public class SetBool : BasePrimitiveAction
{
    ///<value>Input Boolean Parameter.</value>
    [InParam("value")]
    [Help("Value")]
    public bool value;

    [InParam("gameObject")]
    [Help("game object")]
    public GameObject gameobject;

    [InParam("chaser")]
    [Help("game object that starts chasing")]
    public GameObject chaser;

    /// <summary>Initialization Method of SetBool.</summary>
    /// <remarks>Initializes the Boolean value.</remarks>
    public override void OnStart()
    {
        gameobject.GetComponent<Robber>().isChased = value;
        gameobject.GetComponent<Robber>().chasedBy = chaser;
    }
        /// <summary>Method of Update of SetBool.</summary>
        /// <remarks>Complete the task.</remarks>
        public override TaskStatus OnUpdate()
        {
            return TaskStatus.COMPLETED;
        }
    }

