using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using character;

public class JUB_SkeletonBehavior : MonoBehaviour
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

    //combat elements
    public float attackRange;
    public int attackDamages;

    //flee elements
    public float fleeDistance, fleeSpeed;

    //body destruction elements
    public bool bodyIsBroken;
    public float bodyHealth;
    float thresholdLife;
    public JUB_EnnemyDamage ennemyDamage;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<JUB_Maeve>();
        ennemyDamage = GetComponent<JUB_EnnemyDamage>();
        SMBanimator = GetComponent<Animator>();
        pathfinder = GetComponent<AIPath>();
        destinationSetter = GetComponent<AIDestinationSetter>();

        SMBanimator.GetBehaviour<SkeletonSMB_Iddle>().skeleton = this;
        SMBanimator.GetBehaviour<SkeletonSMB_Pursue>().skeleton = this;
        SMBanimator.GetBehaviour<SkeletonSMB_Pause>().skeleton = this;
        SMBanimator.GetBehaviour<SkeletonSMB_Attack>().skeleton = this;
        SMBanimator.GetBehaviour<SkeletonSMB_Escape>().skeleton = this;
        SMBanimator.GetBehaviour<SkeletonSMB_ReconstructionBody>().skeleton = this;
        SMBanimator.GetBehaviour<SkeletonSMB_DestructionBody>().skeleton = this;

        thresholdLife = ennemyDamage.maxHealth - bodyHealth;
    }

    // Update is called once per frame
    void Update()
    {
        toPlayer = player.transform.position - transform.position;
        toPlayer.Normalize();
        SightCast();
        if (!playerInSight)
        {
            MemoryTime();

        }

        if(ennemyDamage.currentHealth <= thresholdLife && !bodyIsBroken)
        {
            SMBanimator.Play("Destruction");
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
