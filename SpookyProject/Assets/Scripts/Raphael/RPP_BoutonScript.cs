using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPP_BoutonScript : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer; // Je modifie le sprite pour avoir du feedback visuel  
    [SerializeField] JUB_FlashManager flashManager; //flash manager à mettre sur layer flashable
    [SerializeField] Sprite boutonActivé, boutonDésactivé; //différents sprites du bouton
    public bool buttonIsActive = false;

    void Start()
    {
        spriteRenderer =GetComponent<SpriteRenderer>();
        flashManager = GetComponentInChildren<JUB_FlashManager>();

        if (!buttonIsActive)
        {
            spriteRenderer.sprite = boutonDésactivé;
        }
        else
        {
            spriteRenderer.sprite = boutonActivé;
        }
    }

    void Update()
    {
        if (flashManager.flashed)
        {
            spriteRenderer.sprite = boutonActivé;
            buttonIsActive = true;
        }
        else
        {
            spriteRenderer.sprite = boutonDésactivé;
            buttonIsActive = false;
        }
    }
}
