using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kinematic : MonoBehaviour
{
    public GameObject target;

    public bool seek, flee;

    public float maxVelocity = 10f;
    public float stopDistance = 1f;
    public float turnAcceleration = 10f;
    public float maxTurnSpeed = 100f;
    public float acceleration = 10f;
    public float maxSpeed = 100f;
    public float frequency = 0.2f;

    float turnSpeed = 0f;
    float movSpeed = 0f;
    float freq = 0f;

    Vector3 movement;
    Quaternion rotation;


    // Update is called once per frame
    void Update()
    {
        if (target != null && Vector3.Distance(target.transform.position, transform.position) <
        stopDistance)
        {
            return;
        }

        freq += Time.deltaTime;
        if (freq > frequency)
        {
            if (target != null)
            {
                if (seek)
                {
                    Seek(target);
                }
                if (flee)
                {
                    Flee(target);
                }
            }
            freq -= frequency;
        }
        turnSpeed += turnAcceleration * Time.deltaTime;
        turnSpeed = Mathf.Min(turnSpeed, maxTurnSpeed);
        movSpeed += acceleration * Time.deltaTime;
        movSpeed = Mathf.Min(movSpeed, maxSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation,
                                              rotation, Time.deltaTime * turnSpeed);
        transform.position += transform.forward.normalized * movSpeed *
                              Time.deltaTime;

    }

    void Seek(GameObject target)
    {
        Vector3 direction = target.transform.position - transform.position;
        direction.y = 0;
        movement = direction.normalized * maxVelocity;
        float angle = Mathf.Rad2Deg * Mathf.Atan2(movement.x, movement.z);
        rotation = Quaternion.AngleAxis(angle, Vector3.up); 
    }   

    void Flee(GameObject target)
    {
        Vector3 direction = transform.position - target.transform.position;
        direction.y = 0;
        movement = direction.normalized * maxVelocity;
        float angle = Mathf.Rad2Deg * Mathf.Atan2(movement.x, movement.z);
        rotation = Quaternion.AngleAxis(angle, Vector3.up);
    }

}
