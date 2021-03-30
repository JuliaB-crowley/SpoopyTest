using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPP_IndividualStep : MonoBehaviour
{
    // Script qui g�re chaque dalle individuellement
    public bool hasBeenStepped = false; // Ce bool empeche que le joueur puisse r�utiliser une seule dalle � l'infinit
    public bool isCorrectStep; // Ce bool d�termine si cette dalle est correcte, comme �a j'ai pas besoin de faire un script pour chaque type de dalle
    [SerializeField] RPP_StepPuzzleMaster puzzleMaster; // R�ference au puzzle master
    [SerializeField] SpriteRenderer stepSprite; // Je modifie le material juste pour avoir du feedback de test, pas besoin de le maintenir

    private void Start()
    {
        stepSprite = this.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //Change la couleur des Steps pour donner du feedback
        if (puzzleMaster.playerHasSucceeded)
        {
            Debug.Log("steps are green");
            stepSprite.color = Color.green;
        }
        else if (puzzleMaster.playerHasFailed)
        {
            stepSprite.color = Color.red;
        }
        else if (hasBeenStepped)
        {
            stepSprite.color = Color.gray;
        }
        else
        {
            stepSprite.color = Color.white;
        }

        //Reset le Puzzle pour que le joueur puisse reessayer
        if (!puzzleMaster.playesIsPresent && puzzleMaster.playerHasFailed)
        {
            hasBeenStepped = false;
        }
    }

    //D�tecte le joueur
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasBeenStepped && !puzzleMaster.puzzleHasBeenCompleted)
        {
            hasBeenStepped = true;
            StepCheck();
        }
    }

    //Check quel type de dalle viens d'�tre activ� puis modifie les ints du puzzle master en accord
    void StepCheck()
    {
        if (isCorrectStep)
        {
            puzzleMaster.currentSteps ++; 
            puzzleMaster.correctSteps++;
        }
        else
        {
            puzzleMaster.currentSteps ++;
        }
    }   
}
