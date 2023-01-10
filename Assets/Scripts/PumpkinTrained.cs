using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class PumpkinTrained : MonoBehaviour
{
    private void Start()
    {
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

    public IEnumerator LifeTime(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
