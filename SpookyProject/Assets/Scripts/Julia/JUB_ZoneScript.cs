using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JUB_ZoneScript : MonoBehaviour
{
    [SerializeField]
    bool isInterior, playerIsHere;
    public GameObject player;
    public Vector3 offSet;
    public Transform cameraTransform, playerTransform, roomCameraPoint;
    public float cameraSpeed, smoothSpeed = 0.125f;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        //glisser déposer le roomCameraPoint si isInterior
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isInterior == false && playerIsHere == true)
        {
            Vector3 desiredPosition = playerTransform.position + offSet;
            Vector3 smoothedPosition = Vector3.Lerp(cameraTransform.position, desiredPosition, smoothSpeed);
            cameraTransform.position = smoothedPosition;
            cameraTransform.LookAt(playerTransform);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("is in zone !" + isInterior.ToString());

            playerIsHere = true;
            if(isInterior == true)
            {
                cameraTransform.position = Vector3.MoveTowards(cameraTransform.position, roomCameraPoint.position, cameraSpeed * Time.deltaTime);
            }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
            playerIsHere = false;
    }
}
