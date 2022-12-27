using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class Pumpkin : MonoBehaviour
{
    private void Awake()
    {
        transform.parent = GameObject.Find("Pumpkins").transform;
        transform.localScale = Vector3.zero;
        StartCoroutine(Grow());
    }
    
    IEnumerator Grow()
    {
        Vector3 scale = Vector3.zero;
        Vector3 growScale = new Vector3(0.005f, 0.005f, 0.005f);
        while (scale.x < 0.5f)
        {      
            scale += growScale;
            transform.localScale = scale;
            yield return new WaitForSeconds(0.02f);
        }
    }
}
