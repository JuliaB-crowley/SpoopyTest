using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPP_TorchesPuzzleManager : MonoBehaviour
{
    [SerializeField] RPP_GeneralPuzzleMaster puzzleMaster;
    public int totalTorches, torchesLit;
    [SerializeField] bool puzzleSolved = false;

    private void Start()
    {
        puzzleMaster = GameObject.FindGameObjectWithTag("Puzzle Master").GetComponent<RPP_GeneralPuzzleMaster>();
    }

    private void Update()
    {
        if(totalTorches <= torchesLit && !puzzleSolved)
        {
            puzzleSolved = true;
            puzzleMaster.puzzlesSolved++;
        }
    }
}
