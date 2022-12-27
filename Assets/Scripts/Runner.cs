using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using UnityEngine.AI;

public class Runner : MonoBehaviour
{

    public PathCreator pathCreator;
    public float timer;

    NavMeshAgent runner;
    Moves moves;
    float dstTravelled;
    bool onPath = false;

    // Start is called before the first frame update
    void Start()
    {
        runner = gameObject.GetComponent<NavMeshAgent>();
        moves = gameObject.GetComponent<Moves>();

        runner.autoBraking = false;
 

        moves.Seek(pathCreator.path.GetClosestPointOnPath(this.transform.position));
        dstTravelled = pathCreator.path.GetClosestDistanceAlongPath(this.transform.position);
    }

    float time = 0f;
    // Update is called once per frame
    void Update()
    {
        if (runner.remainingDistance <= 0.2f)
        {
            onPath = true;
        }
        if (onPath)
        {
            time += Time.deltaTime;

            if (time > timer)
            {
                dstTravelled += runner.speed * timer;
                moves.Seek(pathCreator.path.GetPointAtDistance(dstTravelled));

                time -= timer;
            }
            
        }

        
    }
}
