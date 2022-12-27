using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;
using Pada1.BBCore.Framework;
using BBUnity.Actions;

/// <summary>
/// It is an action to find the closest game object with a given label.
/// </summary>
[Action("MyActions/ClosestGameObjectWithTagWithoutUser")]
[Help("Finds the closest game object with a given tag, without the user of the behaviour")]
public class ClosestGameObjectWithTagWithoutUser : GOAction
{
    ///<value>Input Tag of the target game object.</value>
    [InParam("tag")]
    [Help("Tag of the target game object")]
    public string tag;

    [InParam("gameObject")]
    [Help("This game object")]
    public GameObject thisGameObject;

    ///<value>OutPut The closest game object with the given tag Parameter.</value>
    [OutParam("foundGameObject")]
    [Help("The closest game object with the given tag")]
    public GameObject foundGameObject;

    /// <summary>Initialization Method of ClosestGameObjectWithTag.</summary>
    /// <remarks>Get all the GamesObject with that tag and check which is the closest one.</remarks>
    public override void OnStart()
    {
        float dist = float.MaxValue;
        foreach (GameObject go in GameObject.FindGameObjectsWithTag(tag))
        {
            float newdist = Vector3.Distance(go.transform.position, thisGameObject.transform.position);
            if (newdist < dist  && go != thisGameObject)
            {
                dist = newdist;
                foundGameObject = go;
                //Debug.Log(foundGameObject.name);
            }
        }
    }
    /// <summary>Method of Update of ClosestGameObjectWithTag.</summary>
    /// <remarks>Complete the task.</remarks>
    public override TaskStatus OnUpdate()
    {
        return TaskStatus.COMPLETED;
    }
}
