using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPP_BoutonScript : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer; // Je modifie le sprite pour avoir du feedback visuel  
    [SerializeField] JUB_FlashManager flashManager; //flash manager � mettre sur layer flashable
    [SerializeField] Sprite boutonActiv�, boutonD�sactiv�; //diff�rents sprites du bouton
    [SerializeField] RPP_ButtonsPuzzleManager buttonsManager;

    public bool hasBeenFlashed = false;

    void Start()
    {
        buttonsManager = GetComponentInParent<RPP_ButtonsPuzzleManager>();
        spriteRenderer =GetComponent<SpriteRenderer>();
        flashManager = GetComponentInChildren<JUB_FlashManager>();
        spriteRenderer.sprite = boutonD�sactiv�;
    }

    void Update()
    {
        if (flashManager.flashed && !hasBeenFlashed && !buttonsManager.puzzleSolved)
        {
            StartCoroutine(ActivateButton());
        }
        if (buttonsManager.puzzleSolved)
        {
            spriteRenderer.sprite = boutonActiv�;
        }
    }

    IEnumerator ActivateButton()
    {
        spriteRenderer.sprite = boutonActiv�;
        hasBeenFlashed = true;
        buttonsManager.buttonsActive++;
        yield return new WaitForSeconds(flashManager.flashTime);
        spriteRenderer.sprite = boutonD�sactiv�;
        hasBeenFlashed = false;
        buttonsManager.buttonsActive--;
    }
}
