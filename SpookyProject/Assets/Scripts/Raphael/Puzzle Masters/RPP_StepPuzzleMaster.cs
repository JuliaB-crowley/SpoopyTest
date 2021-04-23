using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPP_StepPuzzleMaster : MonoBehaviour
{
    //Le script qui gère le puzzle des dalles
    public int stepsRequired; // Combien de dalles le joueur doit bien activer pour réussir
    public int currentSteps = 0; // Combien de dalles le joueur à marché
    public int correctSteps = 0; // Combien de dalles correctes le joueur à marché
    public bool playesIsPresent = false, playerHasFailed = false, playerHasSucceeded = false, puzzleHasBeenCompleted = false; // des bools qui checkent la progréssion du joueur dans le niveau
    [SerializeField] RPP_GeneralPuzzleMaster puzzleMaster;

    private void Start()
    {
        puzzleMaster = GameObject.FindGameObjectWithTag("Puzzle Master").GetComponent<RPP_GeneralPuzzleMaster>();
    }

    void Update()
    {
        if(currentSteps == stepsRequired) //Check de tentative du joueur
        {
            if(correctSteps >= stepsRequired && !puzzleHasBeenCompleted) //Le joueur à réussit
            {
                puzzleHasBeenCompleted = true;
                playerHasSucceeded = true;
                puzzleMaster.puzzlesSolved++;
                Debug.Log("The Player Has Succeeded");                
            }
            else if(correctSteps < stepsRequired) // Le joueur à échoué. Il doit sortir des dalles pour les reset puis récommencer
            {
                playerHasFailed = true;
                correctSteps = 0;
                currentSteps = 0;
            }

        }
    }


    //Check si le joueur est présent
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playesIsPresent = true;
        }
    }

    //Check si le joueur n'est plus présent
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(ResetTiles());
        }
    }

    //J'ai besoin que les deux bools soyent opposés pendant quelques frames pour que je puisse reset les dalles
    IEnumerator ResetTiles()
    {
        playesIsPresent = false;
        yield return new WaitForSeconds(0.02f);
        playerHasFailed = false;
    }   
}
