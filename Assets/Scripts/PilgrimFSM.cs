using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PilgrimFSM : MonoBehaviour
{
    Moves moves;
    UnityEngine.AI.NavMeshAgent agent;

    private GameObject[] benches;
    private GameObject chosenGO;

    private WaitForSeconds wait = new WaitForSeconds(0.2f);
    delegate IEnumerator State();
    private State state;

    IEnumerator Start()
    {
        moves = gameObject.GetComponent<Moves>();
        agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        benches = GameObject.FindGameObjectsWithTag("Bench"); ;

        yield return wait;

        state = Wander;

        while (enabled)
            yield return StartCoroutine(state());
    }

    IEnumerator Wander()
    {
        //Debug.Log(transform.name + ": Wander state");

        while (Random.Range(0, 1000) < 970)
        {
            if (Random.Range(0, 100) < 20)
            {
                moves.Wander();
            }
            yield return wait;
        };

        state = Approaching;
    }

    IEnumerator Approaching()
    {
        //Debug.Log(transform.name + ": Approaching state");

        float dist = Mathf.Infinity;
        Vector3 chosenBench = Vector3.zero;
        Vector3 chosenDir = Vector3.zero;
        chosenGO = benches[0];

        for (int i = 0; i < benches.Length; i++)
        {
            Vector3 benchDir = benches[i].transform.position - this.transform.position;
            Vector3 benchPos = benches[i].transform.position + benchDir.normalized * 100;

            if (Vector3.Distance(this.transform.position, benchPos) < dist)
            {
                chosenGO = benches[i];
                dist = Vector3.Distance(this.transform.position, benchPos);
            }
        }
        moves.Seek(chosenGO.transform.position);

        while (Vector3.Distance(chosenGO.transform.position, this.transform.position) > 2f)
        {
            yield return wait;
        };

        state = Sit;
    }

    IEnumerator Sit()
    {
        //Debug.Log(transform.name + ": Sit state");
        agent.enabled = false;
        this.transform.position = chosenGO.transform.position;
        this.transform.position += new Vector3(0, 0.5f, 0);
        transform.rotation = Quaternion.LookRotation(chosenGO.transform.right, Vector3.up);
        yield return new WaitForSeconds(5);

        if(Random.Range(0,100) < 30)
        {
            agent.enabled = true;

            state = Wander;
        }
        else
        {
            this.transform.position -= new Vector3(0, 0.5f, 0);
            state = Sleep;
        }

    }

    IEnumerator Sleep()
    {
        //Debug.Log(transform.name + ": Sleep state");

        transform.rotation = Quaternion.LookRotation(chosenGO.transform.forward, Vector3.up);
        yield return new WaitForSeconds(10);

        agent.enabled = true;

        state = Wander;
    }
}
