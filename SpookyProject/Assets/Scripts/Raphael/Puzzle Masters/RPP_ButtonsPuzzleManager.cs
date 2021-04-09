using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPP_ButtonsPuzzleManager : MonoBehaviour
{
    [SerializeField] RPP_GeneralPuzzleMaster puzzleMaster;
    public int totalButtons, buttonsActive;
    public bool puzzleSolved = false;

    private void Start()
    {
        puzzleMaster = GameObject.FindGameObjectWithTag("Puzzle Master").GetComponent<RPP_GeneralPuzzleMaster>();
    }

    private void Update()
    {
        if (totalButtons <= buttonsActive && !puzzleSolved)
        {
            puzzleSolved = true;
            puzzleMaster.puzzlesSolved++;
        }
    }
}
