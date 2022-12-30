using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using Unity.VisualScripting;
using UnityEngine.AI;

public class GhostAgent : Agent
{
    Rigidbody body;

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
            this.transform.localPosition = new Vector3(-4.6f, -0f, 5.7f);
        }

        // Move the target to a new spot
        Target = targets[Random.Range(0, targets.Length - 1)];

        
        minDis = Vector3.Distance(this.transform.localPosition, Target.localPosition);
        Timer = 0;

    }


    public override void CollectObservations(VectorSensor sensor)
    {
        // Target and Agent positions
        sensor.AddObservation(Target.localPosition);
        sensor.AddObservation(this.transform.localPosition);

        // Agent velocity
        sensor.AddObservation(body.velocity.x);
        sensor.AddObservation(body.velocity.z);
        sensor.AddObservation(body.angularVelocity);
    }

    public float forceMultiplier = 10;
    public float dragMultiplier = 5;

    public float Timer;
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Actions, size = 2
        body.AddForce(transform.forward * actionBuffers.ContinuousActions[0] * forceMultiplier * Time.timeScale);
        body.AddTorque(transform.up * actionBuffers.ContinuousActions[1] * dragMultiplier * Time.timeScale);

        float distanceToTarget = Vector3.Distance(this.transform.localPosition, Target.localPosition);

        if(minDis - distanceToTarget >= 1)
        {
            AddReward(0.1f);
            Debug.Log("closer!");
            minDis = distanceToTarget;
        }

        if (distanceToTarget < 2f)
        {
            SetReward(50.0f);
            
            Target = targets[Random.Range(0, targets.Length - 1)];
            EndEpisode();

        }
        if (transform.localPosition.y < -2)
        {
            SetReward(-10);
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
        if (other.transform.tag == ("Villager") ||
            other.transform.tag == ("Object") ||
            other.transform.tag == ("Hide") ||
            other.transform.tag == ("Bench"))
        {
            Debug.Log("Test");
            AddReward(-1f);
            EndEpisode();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == ("Villager") ||
            collision.transform.tag == ("Object") ||
            collision.transform.tag == ("Hide") ||
            collision.transform.tag == ("Bench"))
        {
            Debug.Log("Test");
            AddReward(-1f);
            EndEpisode();        
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continousActionsOut = actionsOut.ContinuousActions;
        continousActionsOut[1] = Input.GetAxis("Horizontal");
        continousActionsOut[0] = Input.GetAxis("Vertical");
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Target.position,2);
    }
}
