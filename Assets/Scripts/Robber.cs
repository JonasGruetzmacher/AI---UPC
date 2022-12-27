using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Robber : MonoBehaviour
{
    public bool isChased = false;
    [SerializeField] GameObject cops;
    public GameObject chasedBy;

    [SerializeField] NavMeshAgent agent;

    public void GetChased()
    {
        isChased = true;
    }

    void findChaser()
    {
        foreach (Cop cop in cops.GetComponentsInChildren<Cop>())
        {
            if (cop.isChasing)
            {
                chasedBy = cop.gameObject;
                break;
            }
        }
    }

    private void Update()
    {
        if (isChased)
        {
            if (chasedBy == null)
            {
                findChaser();
            }
            else
            {
                agent.SetDestination(this.transform.position * 2 - chasedBy.transform.position);
                agent.isStopped = false;
            }
        }
    }
}
