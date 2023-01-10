using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TaiChiLeader : MonoBehaviour
{
    // Start is called before the first frame update
    NavMeshAgent agent;
    Vector3 startPos;
    Quaternion startRotation;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        startPos = agent.transform.position;
        startRotation = agent.transform.rotation;
        StartCoroutine(MoveForward());
    }

    IEnumerator MoveForward()
    {
        agent.destination = startPos + new Vector3(4,0,0);
        while (agent.remainingDistance > 0)
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(MoveToStart());
    }

    IEnumerator MoveLeft()
    {
        agent.destination = startPos + new Vector3(0,0,3);
        while (agent.remainingDistance > 0)
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(MoveToStart());
    }

    IEnumerator MoveRight()
    {
        agent.destination = startPos + new Vector3(0, 0, -3);
        while (agent.remainingDistance > 0)
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(MoveToStart());
    }

    IEnumerator MoveToStart()
    {
        agent.destination = startPos;
        while (agent.remainingDistance > 0)
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.2f);
        int num = Random.Range(0, 2);
        switch (num)
        {
            case 0:
                StartCoroutine(MoveForward());
                break;
            case 1:
                StartCoroutine(MoveRight());
                break;
            case 2:
                StartCoroutine(MoveLeft());
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation = startRotation;
    }
}
