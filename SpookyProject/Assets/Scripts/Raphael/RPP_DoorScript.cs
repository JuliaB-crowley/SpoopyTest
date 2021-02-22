using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPP_DoorScript : MonoBehaviour
{
    public JUB_InteractibleBehavior doorManager;
    public RPP_RoomMasterScript roomMaster;
    public GameObject doorObject;

    void Start()
    {
        roomMaster = GetComponentInParent<RPP_RoomMasterScript>();
        doorManager = GetComponent<JUB_InteractibleBehavior>();
    }

    void Update()
    {
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
}
