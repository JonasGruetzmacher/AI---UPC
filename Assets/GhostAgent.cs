using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using Unity.VisualScripting;

public class GhostAgent : Agent
{
    Rigidbody body;
    [SerializeField] float runSpeed;
    [SerializeField] Vector4 bounds;

    [SerializeField] Transform[] targets;
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    public Transform Target;

    public float minDis;
    public override void OnEpisodeBegin()
    {
        // If the Agent fell, zero its momentum
        if (this.transform.localPosition.y < 0)
        {
            this.body.angularVelocity = Vector3.zero;
            this.body.velocity = Vector3.zero;
            this.transform.localPosition = new Vector3(-7.7f, -0.497f, -18.3f);
        }

        // Move the target to a new spot
        Target = targets[Random.Range(0, targets.Length - 1)];

        minDis = Vector3.Distance(this.transform.position, Target.position);
        Timer = 0;

    }

    public void MoveAgent(ActionSegment<int> act)
    {
        var dirToGo = Vector3.zero;
        var rotateDir = Vector3.zero;

        var action = act[0];

        switch (action)
        {
            case 1:
                dirToGo = transform.forward * 1f;
                break;
            case 2:
                dirToGo = transform.forward * -1f;
                break;
            case 3:
                rotateDir = transform.up * 0.5f;
                break;
            case 4:
                rotateDir = transform.up * -0.5f;
                break;
            case 5:
                dirToGo = transform.right * -0.75f;
                break;
            case 6:
                dirToGo = transform.right * 0.75f;
                break;
        }
        transform.Rotate(rotateDir, Time.fixedDeltaTime * 200f);
        body.AddForce(dirToGo * runSpeed, ForceMode.VelocityChange);

    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Target and Agent positions
        sensor.AddObservation(Target.localPosition);
        sensor.AddObservation(this.transform.localPosition);

        // Agent velocity
        sensor.AddObservation(body.velocity.x);
        sensor.AddObservation(body.velocity.z);

        sensor.AddObservation(new Vector2( bounds.x, bounds.y));
        sensor.AddObservation(new Vector2(bounds.y, bounds.w));
    }

    public float forceMultiplier = 10;

    public float Timer;
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        if(transform.localPosition.x < bounds.x || transform.localPosition.z < bounds.y || 
            transform.localPosition.x > bounds.z || transform.localPosition.z > bounds.w)
        {
            AddReward(-1);
            EndEpisode();
        }

        MoveAgent(actionBuffers.DiscreteActions);
        
        // Rewards
        float distanceToTarget = Vector3.Distance(this.transform.position, Target.position);
        /*
        // Reached target
        if (distanceToTarget < 2f)
        {
            AddReward(1.0f);
            EndEpisode();
        }

        if (distanceToTarget < minDis)
        {
            AddReward(0.01f);
            distanceToTarget = minDis;
        }*/

        AddReward(minDis - distanceToTarget);
        minDis = distanceToTarget;

        if (distanceToTarget < 2f)
        {
            AddReward(20.0f);
            Target = targets[Random.Range(0, targets.Length - 1)];
            

        }
        // Fell off platform
        else if (this.transform.localPosition.y < -1.5f)
        {
            EndEpisode();
        }

        if (Timer > 180)
        {
            EndEpisode();
        }
        Timer += Time.deltaTime;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.transform.parent.name.Contains("BaseTiles"))
            AddReward(-5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.parent.name.Contains("BaseTiles"))
            EndEpisode();
            AddReward(-5f);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;
        if (Input.GetKey(KeyCode.D))
        {
            discreteActionsOut[0] = 3;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            discreteActionsOut[0] = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            discreteActionsOut[0] = 4;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            discreteActionsOut[0] = 2;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Target.position,2);
    }
}
