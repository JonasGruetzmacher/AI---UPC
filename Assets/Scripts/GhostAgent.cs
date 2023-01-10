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
        //this.transform.localPosition = new Vector3(Random.Range(-33,-13), 0, Random.Range(-37,-18));

        // Move the target to a new spot
        //Target = targets[Random.Range(0, targets.Length - 1)];
        Target.transform.localPosition = new Vector3(Random.Range(-33, -13), 0, Random.Range(-37, -18));

        minDis = Vector3.Distance(this.transform.localPosition, Target.localPosition);

    }


    public override void CollectObservations(VectorSensor sensor)
    {
        // Target and Agent positions
        sensor.AddObservation(Target.localPosition);
        sensor.AddObservation(this.transform.localPosition);

    }

    public float Timer;
    public float moveSpeed = 1f;
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        float moveX = actionBuffers.ContinuousActions[0];
        float moveZ = actionBuffers.ContinuousActions[1];

        if (moveX != 0 && moveZ != 0)
            transform.forward = new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;

        transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;
        
        float distanceToTarget = Vector3.Distance(this.transform.localPosition, Target.localPosition);

        if (minDis - distanceToTarget >= 1)
        {
            AddReward(0.1f);
            minDis = distanceToTarget;
        }

        if (minDis - distanceToTarget <= -1)
        {
            Debug.Log(GetCumulativeReward());
            AddReward(-0.1f);
            minDis = distanceToTarget;
        }

        if (distanceToTarget < 2f)
        {
            SetReward(5.0f);      
            EndEpisode();

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Wall>(out Wall wall) ||
            other.TryGetComponent<Avoid>(out Avoid avoid))
        {
            AddReward(-1f);
            //EndEpisode();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<Avoid>(out Avoid avoid))

        {
            AddReward(-1f);
            //EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continousActionsOut = actionsOut.ContinuousActions;
        continousActionsOut[0] = Input.GetAxis("Vertical");
        continousActionsOut[1] = Input.GetAxis("Horizontal");
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Target.position,2);
    }
}
