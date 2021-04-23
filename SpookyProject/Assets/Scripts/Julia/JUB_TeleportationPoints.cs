using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JUB_TeleportationPoints : MonoBehaviour
{
    public Transform pointA, pointB;
    public GameObject player;
    public float cooldown = 1;
    public bool pointADesactivated, pointBDesactivated;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(player.transform.position, pointA.position) < 1 && !pointADesactivated)
        {
            player.transform.position = pointB.transform.position;
            pointBDesactivated = true;
            StartCoroutine(Reactivate());
        }
        else if(Vector2.Distance(player.transform.position, pointB.position) < 1 && !pointBDesactivated)
        {
            player.transform.position = pointA.transform.position;
            pointADesactivated = true;
            StartCoroutine(Reactivate());
        }
    }

    IEnumerator Reactivate()
    {
        yield return new WaitForSeconds(cooldown);
        pointBDesactivated = false;
        pointADesactivated = false;
    }
}
