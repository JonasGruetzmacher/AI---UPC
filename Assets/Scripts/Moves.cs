
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.PlayerSettings;
using static UnityEngine.UI.Image;

public class Moves : MonoBehaviour
{
    public GameObject target;
    [HideInInspector]
    public GameObject[] waypoints;

    GameObject[] hidingSpots;
    NavMeshAgent agent;
    
    int patrolWP = 0;
    
    
    void OnEnable()
    {
        agent = this.GetComponent<NavMeshAgent>();
        
    }

    void Start()
    {
        hidingSpots = GameObject.FindGameObjectsWithTag("Hide");
    }


    public void Seek(Vector3 pos)
    {
        agent.SetDestination(pos);
    }

    public void Flee(Vector3 pos)
    {
        Vector3 fleeVector = pos - this.transform.position;
        agent.SetDestination(this.transform.position - fleeVector);
        
    }
    public void FleeTarget()
    {
        Vector3 fleeVector = target.transform.position - this.transform.position;
        agent.SetDestination(this.transform.position - fleeVector);
    }

    public void Wander(float wanderRadius = 10, int layermask = -1)
    {
        Vector3 randDirection = Random.insideUnitSphere * wanderRadius;

        randDirection += this.transform.position;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, wanderRadius, layermask);

        Seek(navHit.position);

    }

    public Vector3 WanderValue(float wanderRadius = 10, int layermask = -1)
    {
        Vector3 randDirection = Random.insideUnitSphere * wanderRadius;

        randDirection += this.transform.position;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, wanderRadius, layermask);

        return (navHit.position);

    }


    public void Pursue()
    {
        Vector3 targetDir = target.transform.position - transform.position;

        float relativeHeading = Vector3.Angle(this.transform.forward, this.transform.TransformVector(target.transform.forward));

        float toTarget = Vector3.Angle(this.transform.forward, this.transform.TransformVector(targetDir));

        if ((toTarget > 90 && relativeHeading < 20))
        {
            Seek(target.transform.position);
            return;
        }

        float lookAhead = targetDir.magnitude / (agent.speed);
        Seek(target.transform.position + target.transform.forward * lookAhead);
    }

    public void Evade()
    {
        Vector3 targetDir = target.transform.position - transform.position;
        float lookAhead = targetDir.magnitude / agent.speed;
        Flee(target.transform.position + target.transform.forward * lookAhead);
    }

    public void Hide()
    {
        float dist = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;
        Vector3 chosenDir = Vector3.zero;
        GameObject chosenGO = hidingSpots[0];

        for (int i = 0; i < hidingSpots.Length; i++)
        {
            Vector3 hideDir = hidingSpots[i].transform.position - target.transform.position;
            Vector3 hidePos = hidingSpots[i].transform.position + hideDir.normalized * 3;

            if (Vector3.Distance(this.transform.position, hidePos) < dist)
            {
                chosenSpot = hidePos;
                chosenDir = hideDir;
                chosenGO = hidingSpots[i];
                dist = Vector3.Distance(this.transform.position, hidePos);
            }
        }

        Collider hideCol = chosenGO.GetComponent<Collider>();
        Ray backRay = new Ray(chosenSpot, -chosenDir.normalized);
        RaycastHit info;
        float distance = 250.0f;
        hideCol.Raycast(backRay, out info, distance);


        Seek(info.point + chosenDir.normalized);
    }

    public void Patrol()
    {
        if (waypoints.Length > 0)
        {
            print(patrolWP);
            patrolWP = (patrolWP + 1) % waypoints.Length;
            Seek(waypoints[patrolWP].transform.position);
        }
    }
}

