using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JUB_Autodestruction : MonoBehaviour
{
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, time);
    }


}
