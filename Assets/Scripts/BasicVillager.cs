using Pada1.BBCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;


public class BasicVillager : MonoBehaviour
{
    public Behaviour behaviour;
    public float checkBehaviourTime = 0f;
    public GameObject target;

    [Header("Wander Settings")]
    public float wanderRadius = 10;
    NavMeshAgent agent;

    Moves moves;

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        moves = GetComponent<Moves>();
        moves.target = this.target;
    }

    float time = 0f;
    void Update()
    {
        time += Time.deltaTime;
        if (time > checkBehaviourTime)
        {
            switch (behaviour)
            {
                case Behaviour.SEEK:
                    moves.Seek(target.transform.position);
                    break;

                case Behaviour.FLEE:
                    moves.Flee(target.transform.position);
                    break;

                case Behaviour.WANDER:
                        moves.Wander(wanderRadius);
                    break;

                case Behaviour.PURSUE:
                    moves.Pursue();
                    break;

                case Behaviour.EVADE:
                    moves.Evade();
                    break;

                case Behaviour.HIDE:
                    //if (agent.remainingDistance < .2f)
                        moves.Hide();
                    break;

                case Behaviour.PATROL:
                    if (!agent.pathPending && agent.remainingDistance < 2f) moves.Patrol();
                    break;
            }

            time -= checkBehaviourTime;
        }
    }

    public void FleeFromChaser()
    {
        agent.speed = 2f;
        behaviour = Behaviour.FLEE;
    }

    public void HideFromChaser()
    {
        agent.speed = 2f;
        behaviour = Behaviour.HIDE;
    }
    
    public void VillagerWander()
    {
        agent.speed = 2f;
        behaviour = Behaviour.WANDER;
    }

    public void SeekTarget()
    {
        agent.speed = 2f;
        behaviour = Behaviour.SEEK;
    }

    public void PursueTarget()
    {
        agent.speed = 3.5f;
        behaviour = Behaviour.SEEK;
    }



}

public enum Behaviour
{
    SEEK,
    FLEE,
    WANDER,
    PURSUE,
    EVADE,
    HIDE,
    PATROL
}