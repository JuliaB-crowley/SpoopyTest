using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JUB_ImpTrail : JUB_DamagingEvent
{
    public float decayTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TrailDestructionCoroutine());
    }

    IEnumerator TrailDestructionCoroutine()
    {
        yield return new WaitForSeconds(decayTime);
        Destroy(gameObject);
    }
}
