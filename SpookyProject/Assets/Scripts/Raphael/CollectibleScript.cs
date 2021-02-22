using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleScript : MonoBehaviour
{
    public GameObject collectibleObject;
    public int collectibleValeur;

    private void Start()
    {
        collectibleObject = this.gameObject;
    }
}
