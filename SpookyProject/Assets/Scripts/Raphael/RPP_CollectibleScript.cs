using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPP_CollectibleScript : MonoBehaviour
{
    public GameObject collectibleObject;
    public int collectibleValeur;

    private void Start()
    {
        collectibleObject = this.gameObject;
    }
}
