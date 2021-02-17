using character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JUB_MaeveCollisionDetector : MonoBehaviour
{
    public JUB_Maeve MaeveScript;
    public float radius = 0.05f;
    public LayerMask blocksPlayer, obstacleLayer;
    public bool isCollision;

    // Update is called once per frame
    void Update()
    {
        if(MaeveScript.isCrouching)
        {
            Collider2D[] collision = Physics2D.OverlapCircleAll(transform.position, radius, obstacleLayer);
            isCollision = false;
            foreach (Collider2D col in collision)
            {
                isCollision = true;
            }
        }
        else
        {
            Collider2D[] collision = Physics2D.OverlapCircleAll(transform.position, radius, blocksPlayer);
            isCollision = false;
            foreach (Collider2D col in collision)
            {
                isCollision = true;
            }

        }

    }
}
