using UnityEngine;
using UnityEngine.AI;

public class Formation : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject target;
    public Vector3 pos;
    public Quaternion rot;

    void Start()
    {
        transform.rotation = target.transform.rotation;
        transform.position = target.transform.position + pos;
    }

    void Update()
    {
        agent.destination = target.transform.position + pos;
        transform.rotation = target.transform.rotation;
    }
}
