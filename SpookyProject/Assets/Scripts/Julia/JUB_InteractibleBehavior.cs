using character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JUB_InteractibleBehavior : MonoBehaviour
{
    public bool interactible, interacted;
    public GameObject player;
    public JUB_Maeve maeveScript;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        maeveScript = player.GetComponent<JUB_Maeve>();
    }

    void Update()
    {
        if(interactible)
        {
            Vector2 thisToPlayer = transform.position - player.transform.position;
            float distanceToPlayer = thisToPlayer.magnitude;
            if(distanceToPlayer > maeveScript.interactAndPushableRange)
            {
                interactible = false;
            }
        }
    }
}
