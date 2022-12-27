using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class FlockManager : MonoBehaviour
{
    public GameObject animalPrefab;
    public int numAnimals = 30;
    public GameObject[] allAnimals;
    public Vector3 flockLimits = new Vector3(5,5,5);
    public Vector3 goalPos;
    

    [Header("Animal Settings")]
    [Range(0.0f,5.0f)]
    public float minSpeed = 1f;
    [Range(0.0f, 5.0f)]
    public float maxSpeed = 4f;
    [Range(1.0f, 10.0f)]
    public float neighbourDistance = 3f;
    [Range(0.1f, 10.0f)]
    public float rotationSpeed = 0.4f;
    public float avoidVillagerDistance = 2f;

    [Header("Rule Weight")]
    
    [Range(0.0f, 1.0f)]
    public float cohesionWeight = 1f;
    [Range(0.0f, 1.0f)]
    public float separationWeight = 1f;
    [Range(0.0f, 1.0f)]
    public float alignWeight = 1f;
    [Range(0.0f, 1.0f)]
    public float goalWeight = 1f;

    [Header("Raycasting")]
    public bool activateRaycasting;
    public LayerMask avoidLayers;
    public bool showRaycast;

    [Header("Random Behaviour Settings")]
    public int randomGoal = 1000;
    public bool changeGoal = false;
    public int randomSpeed = 100;
    public int randomUpdate = 100;

    void Start()
    {
        allAnimals = new GameObject[numAnimals];
        for (int i = 0; i < numAnimals; ++i)
        {
            Vector3 pos = this.transform.position +
                            new Vector3 (Random.Range(-flockLimits.x, flockLimits.x),
                                        Random.Range(-flockLimits.y, flockLimits.y),
                                        Random.Range(-flockLimits.z, flockLimits.z));
            allAnimals[i] = (GameObject)Instantiate(animalPrefab, pos,
                                Random.rotation);
            allAnimals[i].GetComponent<Flock>().myManager = this;
            allAnimals[i].transform.SetParent(this.transform);
            
        }
        goalPos = this.transform.position;
    }

    void Update()
    {
        if (changeGoal && Random.Range(0, randomGoal) < 10)
        {
            goalPos = this.transform.position +
                            new Vector3(Random.Range(-flockLimits.x, flockLimits.x),
                                        Random.Range(-flockLimits.y, flockLimits.y),
                                        Random.Range(-flockLimits.z, flockLimits.z));
        }
    }
}
