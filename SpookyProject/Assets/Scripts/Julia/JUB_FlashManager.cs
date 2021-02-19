using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JUB_FlashManager : MonoBehaviour
{
    public bool burned, flashed;
    public float flashTime = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FlashEnd()
    {
        StartCoroutine(FlashEndCoroutine());
    }
    public IEnumerator FlashEndCoroutine()
    {
        yield return new WaitForSeconds(flashTime);
        flashed = false;
        Debug.Log("flash désactivé");
    }
    public void BurnEnd()
    {
        StartCoroutine(BurnEndCoroutine());
    }
    public IEnumerator BurnEndCoroutine()
    {
        yield return new WaitForSeconds(flashTime);
        burned = false;
    }
}
