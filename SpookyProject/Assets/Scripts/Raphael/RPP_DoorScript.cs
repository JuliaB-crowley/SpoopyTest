using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPP_DoorScript : MonoBehaviour
{
    public JUB_InteractibleBehavior doorManager;
    //public RPP_RoomMasterScript roomMaster;
    public GameObject doorObject;
    public int minPuzzlesSolved;
    [SerializeField] RPP_GeneralPuzzleMaster puzzleMaster;
    public SpriteRenderer doorSprite;
    public Sprite doorOpen, doorLocked, blueDoor, yellowDoor, greenDoor, violetDoor, brokenDoor;
    public bool needBlueKey, needYellowKey, needGreenKey, needVioletKey, doorIsBroken;

    void Start()
    {
        doorSprite = GetComponentInChildren<SpriteRenderer>();
        puzzleMaster = GameObject.FindGameObjectWithTag("Puzzle Master").GetComponent<RPP_GeneralPuzzleMaster>();
        //roomMaster = this.GetComponentInParent<RPP_RoomMasterScript>();
        doorManager = this.GetComponent<JUB_InteractibleBehavior>();
        if (needBlueKey)
        {
            doorSprite.sprite = blueDoor;
        }
        else if (needYellowKey)
        {
            doorSprite.sprite = yellowDoor;
        }
        else if (needGreenKey)
        {
            doorSprite.sprite = greenDoor;
        }
        else if (needVioletKey)
        {
            doorSprite.sprite = violetDoor;
        }
        else if (doorIsBroken)
        {
            doorSprite.sprite = brokenDoor;
        }
    }

    void Update()
    {
        if (!doorIsBroken)
        {
            if (!needBlueKey && !needYellowKey && !needGreenKey && !needVioletKey)
            {
                if (minPuzzlesSolved <= puzzleMaster.puzzlesSolved)
                {
                    doorSprite.sprite = doorOpen;

                    if (!doorManager.interacted)
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
                    doorSprite.sprite = doorLocked;
                    Debug.Log("The player has to solve a puzzle");
                }
            }

            else
            {
                if (needBlueKey)
                {
                    if (!puzzleMaster.hasBlueKey)
                    {
                        doorObject.SetActive(true);
                        doorManager.interacted = false;
                    }
                    else
                    {
                        doorSprite.sprite = doorOpen;

                        if (!doorManager.interacted)
                        {
                            doorObject.SetActive(true);
                        }
                        else
                        {
                            doorObject.SetActive(false);
                        }
                    }
                }

                if (needYellowKey)
                {
                    if (!puzzleMaster.hasYellowKey)
                    {
                        doorObject.SetActive(true);
                        doorManager.interacted = false;
                    }
                    else
                    {
                        doorSprite.sprite = doorOpen;

                        if (!doorManager.interacted)
                        {
                            doorObject.SetActive(true);
                        }
                        else
                        {
                            doorObject.SetActive(false);
                        }
                    }
                }

                if (needGreenKey)
                {
                    if (!puzzleMaster.hasGreenKey)
                    {
                        doorObject.SetActive(true);
                        doorManager.interacted = false;
                    }
                    else
                    {
                        doorSprite.sprite = doorOpen;

                        if (!doorManager.interacted)
                        {
                            doorObject.SetActive(true);
                        }
                        else
                        {
                            doorObject.SetActive(false);
                        }
                    }

                }

                if (needVioletKey)
                {
                    if (!puzzleMaster.hasVioletKey)
                    {
                        doorObject.SetActive(true);
                        doorManager.interacted = false;
                    }
                    else
                    {
                        doorSprite.sprite = doorOpen;

                        if (!doorManager.interacted)
                        {
                            doorObject.SetActive(true);
                        }
                        else
                        {
                            doorObject.SetActive(false);
                        }
                    }
                }
            }
        }
        else
        {
            doorObject.SetActive(true);
            doorManager.interacted = false;
        }
    }
}
