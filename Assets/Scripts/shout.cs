using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class shout : MonoBehaviour
{
    public float shoutTime = 3f;
    public GameObject robber;

    private float timer = 0f;

    void OnDrawGizmos()
    {
        Handles.Label(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), "Help!");
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > shoutTime)
        {
            Destroy(gameObject);
        }
    }
}
