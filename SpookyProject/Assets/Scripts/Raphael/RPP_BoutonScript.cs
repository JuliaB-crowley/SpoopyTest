using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPP_BoutonScript : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer; // Je modifie le sprite pour avoir du feedback visuel  
    [SerializeField] JUB_FlashManager flashManager; //flash manager � mettre sur layer flashable
    [SerializeField] Sprite boutonActiv�, boutonD�sactiv�; //diff�rents sprites du bouton
    public bool buttonIsActive = false;

    void Start()
    {
        spriteRenderer =GetComponent<SpriteRenderer>();
        flashManager = GetComponentInChildren<JUB_FlashManager>();

        if (!buttonIsActive)
        {
            spriteRenderer.sprite = boutonD�sactiv�;
        }
        else
        {
            spriteRenderer.sprite = boutonActiv�;
        }
    }

    void Update()
    {
        if (flashManager.flashed)
        {
            spriteRenderer.sprite = boutonActiv�;
            buttonIsActive = true;
        }
        else
        {
            spriteRenderer.sprite = boutonD�sactiv�;
            buttonIsActive = false;
        }
    }
}
