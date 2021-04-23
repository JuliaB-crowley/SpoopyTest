using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using character;

public class JUB_VampireBehavior : MonoBehaviour
{
    public Animator SMBanimator;
    public AIPath pathfinder;
    public AIDestinationSetter destinationSetter;
    public JUB_Maeve player;

    //sight cast parameters 
    public float maxSight;
    public Vector2 toPlayer;
    public LayerMask blocksLOS, isPlayer;
    public bool playerInSight, playerInMemory;
    public float timeBeforeForget, secondSinceLastSeen;

    //iddle elements
    public bool cyclicPatrol = false;
    public List<Transform> patrolTargets = new List<Transform>();
    public float patrolWaitTime, iddleSpeed;

    //pursue elements
    public float stopDistance, pursueSpeed;

    //pause elements
    public float pauseTime;

    //flee elements
    public float securityThreshold, fleeDistance, fleeSpeed;
    public bool isEscaping;

    //attack elements
    public GameObject batPrefab;
    public float buildupTime, recoveryTime;

    //stun elements
    public float stunTime;
    public JUB_FlashManager flashManager;
    public bool hasBeenStunned;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<JUB_Maeve>();
        SMBanimator = GetComponent<Animator>();
        pathfinder = GetComponent<AIPath>();
        destinationSetter = GetComponent<AIDestinationSetter>();
        flashManager = GetComponentInChildren<JUB_FlashManager>();

        SMBanimator.GetBehaviour<VampireSMB_Idle>().vampire = this;
        SMBanimator.GetBehaviour<VampireSMB_Attack>().vampire = this;
        SMBanimator.GetBehaviour<VampireSMB_Escape>().vampire = this;
        SMBanimator.GetBehaviour<VampireSMB_Stun>().vampire = this;
        SMBanimator.GetBehaviour<VampireSMB_Pause>().vampire = this;
        SMBanimator.GetBehaviour<VampireSMB_Pursue>().vampire = this;
    }

    // Update is called once per frame
    void Update()
    {
        toPlayer = player.transform.position - transform.position;
        Vector2 distanceFromPlayer = toPlayer;
        toPlayer.Normalize();
        SightCast();
        if (!playerInSight)
        {
            MemoryTime();

        }

        if((distanceFromPlayer.magnitude < securityThreshold) && !isEscaping && !hasBeenStunned)
        {
            SMBanimator.Play("Escape");
        }

        if(flashManager.flashed && !hasBeenStunned)
        {
            hasBeenStunned = true;
            SMBanimator.Play("Stun");
        }
    }

    void SightCast()
    {
        RaycastHit2D hit2D;
        //RaycastHit hit;
        float hitLength = maxSight;
        if (player.isCrouching)
        {
            hitLength /= 2;
        }
        hit2D = Physics2D.Raycast(transform.position, toPlayer, hitLength, blocksLOS);
        //Physics.Raycast(transform.position, toPlayer, out hit, hitLength, blocksLOS);
        if (hit2D.collider)
        {
            hitLength = hit2D.distance;
        }



        hit2D = Physics2D.Raycast(transform.position, toPlayer, hitLength, isPlayer);
        if (hit2D.collider)
        {
            playerInSight = true;
            hitLength = hit2D.distance;
            playerInMemory = true;
            secondSinceLastSeen = 0;
        }
        else
        {
            playerInSight = false;
        }

        Debug.DrawRay(transform.position, toPlayer * hitLength, Color.red);



    }

    void MemoryTime()
    {
        secondSinceLastSeen += Time.deltaTime;
        if (secondSinceLastSeen >= timeBeforeForget)
        {
            playerInMemory = false;
            secondSinceLastSeen = 0;
        }
    }
}
