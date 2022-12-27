using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Flock : MonoBehaviour
{
    public FlockManager myManager;

    float speed;
    bool turning = false;
    Vector3 direction = new Vector3(0f, 0f, 1f);
    RaycastHit hit = new RaycastHit();
    GameObject[] villager;

    void Start()
    {
        speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);
        villager = GameObject.FindGameObjectsWithTag("Villager");
    }
    private void ApplyRules()
    {
        GameObject[] gos;
        gos = myManager.allAnimals;

        Vector3 cohesion = Vector3.zero;
        Vector3 align = Vector3.zero;
        Vector3 separation = Vector3.zero;
        Vector3 goal = (myManager.goalPos - this.transform.position);

        int gSize = 0;

        foreach (GameObject go in gos)
        {
            if (go != this.gameObject)
            {
                float nDistance = Vector3.Distance(go.transform.position, transform.position);
                if (nDistance <= myManager.neighbourDistance)
                {
                    cohesion += go.transform.position;
                    align += go.GetComponent<Flock>().direction;
                    gSize++;
                    separation += (transform.position - go.transform.position) / (nDistance * nDistance);
                }
            }
        }
        if (gSize > 0)
        {
            cohesion = ((cohesion / gSize) - this.transform.position).normalized*speed;
            align /= gSize;
            speed = Mathf.Clamp(align.magnitude, myManager.minSpeed, myManager.maxSpeed);
        }

        direction = (cohesion * myManager.cohesionWeight
                + separation * myManager.separationWeight
                + align * myManager.alignWeight
                + goal * myManager.goalWeight).normalized * speed;
    }

    bool avoidVillager()
    {

        for (int i = 0; i < villager.Length; i++)
        {
            if (Vector3.Distance(transform.position, villager[i].transform.position) < myManager.avoidVillagerDistance)
            {
                direction = (transform.position - villager[i].transform.position).normalized * myManager.maxSpeed;
                speed = myManager.maxSpeed * 1.5f;
                return true;
            }
        }
        return false;
    }

    void Update()
    {
        Bounds b = new Bounds(myManager.transform.position, myManager.flockLimits * 2);
        if (!avoidVillager())
        {
            if (myManager.showRaycast)
            {
                Debug.DrawRay(this.transform.position, this.transform.forward, Color.green);
            }

            if (!b.Contains(transform.position))
            {
                turning = true;
            }
            else
            {
                turning = false;
            }

            if (turning)
            {
                direction = myManager.transform.position - transform.position;
                
            }
            else
            {
                if (Random.Range(0, myManager.randomSpeed) < 10)
                {
                    speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);
                }

                if (Random.Range(0, myManager.randomUpdate) < 10)
                {                
                    ApplyRules();
                }
                if (Physics.Raycast(transform.position, this.transform.forward * 0.5f, out hit, myManager.avoidLayers) && myManager.activateRaycasting)
                {
                    turning = false;
                    direction = Vector3.Reflect(this.transform.forward, hit.normal);
                    if (myManager.showRaycast)
                    {
                        Debug.DrawRay(this.transform.position, this.transform.forward * 0.5f, Color.red, 0.2f);
                    }
                }
            }
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction),
                                                                myManager.rotationSpeed * Time.deltaTime);
        transform.Translate(0.0f, 0.0f, Time.deltaTime * speed);
        
    }
}