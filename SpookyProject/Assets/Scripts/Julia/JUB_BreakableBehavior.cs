using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JUB_BreakableBehavior : MonoBehaviour
{
    public List<GameObject> possibleLoots;
    public float breakTime;

    public void Breaking()
    {
        if(possibleLoots.Count > 0)
        {
            int index = Random.Range(0, possibleLoots.Count - 1);
            Instantiate(possibleLoots[index], transform.position, Quaternion.identity);
        }
        StartCoroutine("DestroyCoroutine");
    }

    IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(breakTime);
        Destroy(gameObject);
    }
}
