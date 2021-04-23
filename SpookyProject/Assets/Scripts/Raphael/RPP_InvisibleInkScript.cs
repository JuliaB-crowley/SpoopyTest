using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPP_InvisibleInkScript : MonoBehaviour
{
    public SpriteRenderer sprite; // Je modifie le material juste pour avoir du feedback de test, pas besoin de le maintenir    
    public JUB_FlashManager flashManager; //flash manager à mettre en enfant et sur layer flashable

    void Start()
    {
        sprite = this.GetComponent<SpriteRenderer>();
        sprite.enabled = false;

        flashManager = GetComponentInChildren<JUB_FlashManager>();
    }

    void Update()
    {
        if (flashManager.flashed)
        {
            sprite.enabled = true;
        }
        else
        {
            sprite.enabled = false;
        }
    }
}
