using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPP_RoomMasterScript : MonoBehaviour
{
    public bool playerIsPresent, firstRoom;
    public Vector2 roomZone;
    public LayerMask playerLayer;
    public static float cameraTransitionSpeed = 100f;
    public Transform cameraTransform, roomCenter;

    private void Start()
    {
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        roomCenter = this.transform;

        if (firstRoom)
        {
            cameraTransform.position = roomCenter.position;
        }
    }

    private void Update()
    {
        Collider2D[] playerCheck = Physics2D.OverlapBoxAll(this.transform.position, roomZone, 0f, playerLayer);

        if(playerCheck.Length == 0)
        {
            playerIsPresent = false;
        }
        else
        {
            playerIsPresent = true;
            if(cameraTransform != roomCenter)
            {
                cameraTransform.position = Vector3.MoveTowards(cameraTransform.position, roomCenter.position, cameraTransitionSpeed * Time.deltaTime);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(this.transform.position, roomZone);
    }
}
