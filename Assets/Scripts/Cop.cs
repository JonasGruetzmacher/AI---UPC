using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cop : MonoBehaviour
{
    public bool isChasing = false;

    public void StartChase()
    {
        isChasing = true;
    }
    public void StopChase()
    {
        isChasing=false;
    }
}
