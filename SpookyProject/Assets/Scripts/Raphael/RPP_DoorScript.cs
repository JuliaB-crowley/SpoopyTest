using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPP_DoorScript : MonoBehaviour
{
    public JUB_InteractibleBehavior doorManager;
    public RPP_RoomMasterScript roomMaster;
    public GameObject doorObject;
    public int minPuzzlesSolved;
    [SerializeField] RPP_GeneralPuzzleMaster puzzleMaster;
    public GameObject doorBlock;

    void Start()
    {
        puzzleMaster = GameObject.FindGameObjectWithTag("Puzzle Master").GetComponent<RPP_GeneralPuzzleMaster>();
        roomMaster = this.GetComponentInParent<RPP_RoomMasterScript>();
        doorManager = this.GetComponent<JUB_InteractibleBehavior>();
    }

    void Update()
    {
        if (minPuzzlesSolved <= puzzleMaster.puzzlesSolved)
        {
            //Debug.Log("The player has solved a puzzle");
            doorBlock.SetActive(false);
            if (!roomMaster.playerIsPresent)
            {
                doorObject.SetActive(false);
                doorManager.interacted = false;
            }

            if (roomMaster.playerIsPresent && !doorManager.interacted)
            {
                doorObject.SetActive(true);
            }
            else
            {
                doorObject.SetActive(false);
            }
        }
        else
        {
            doorObject.SetActive(true);
            doorManager.interacted = false;
            doorBlock.SetActive(true);
            Debug.Log("The player has to solve a puzzle");
        }       
    }
}
