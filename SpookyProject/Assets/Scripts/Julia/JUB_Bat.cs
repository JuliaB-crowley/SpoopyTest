using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class JUB_Bat : MonoBehaviour
{
    AIDestinationSetter destinationSetter;
    GameObject player;
    [SerializeField]
    LayerMask ennemies;
    // Start is called before the first frame update
    void Start()
    {
        destinationSetter = GetComponent<AIDestinationSetter>();
        player = GameObject.FindGameObjectWithTag("Player");
        destinationSetter.target = player.transform;
    }

    private void Update()
    {
        Vector3 decalage = Vector3.zero;
        Collider2D[] hitEnnemies = Physics2D.OverlapCircleAll(transform.position, 0.2f, ennemies);
        foreach(Collider2D other in hitEnnemies)
        {
            if(other.GetComponent<JUB_Bat>())
            {
                decalage += (transform.position - other.transform.position);
            }
        }
        decalage.z = 0;
        transform.position += decalage*Time.deltaTime;

    }

}
