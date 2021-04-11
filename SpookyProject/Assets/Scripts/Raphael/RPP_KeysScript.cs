using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPP_KeysScript : MonoBehaviour
{
    public GameObject keyObject;
    [SerializeField] RPP_GeneralPuzzleMaster puzzleMaster;
    public bool isYellowKey, isBlueKey, isGreenKey, isVioletKey;

    void Start()
    {
        keyObject = this.gameObject;
        puzzleMaster = GameObject.FindGameObjectWithTag("Puzzle Master").GetComponent<RPP_GeneralPuzzleMaster>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isYellowKey)
            {
                puzzleMaster.hasYellowKey = true;
                keyObject.SetActive(false);
            }
            else if (isBlueKey)
            {
                puzzleMaster.hasBlueKey = true;
                keyObject.SetActive(false);
            }
            else if (isGreenKey)
            {
                puzzleMaster.hasGreenKey = true;
                keyObject.SetActive(false);
            }
            else if (isVioletKey)
            {
                puzzleMaster.hasVioletKey = true;
                keyObject.SetActive(false);
            }
        }
    }
}
