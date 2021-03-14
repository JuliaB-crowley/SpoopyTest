using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class JUB_ImpBehavior : MonoBehaviour
{
    public Animator SMBanimator;
    public AIPath pathfinder;
    public AIDestinationSetter destinationSetter;
    public bool cyclicPatrol = false;
    public List<Transform> patrolTargets = new List<Transform>();
    public float patrolWaitTime;

    // Start is called before the first frame update
    void Start()
    {
        SMBanimator = GetComponent<Animator>();
        pathfinder = GetComponent<AIPath>();
        destinationSetter = GetComponent<AIDestinationSetter>();

        SMBanimator.GetBehaviour<ImpSMB_Idle>().imp = this;
        /*SMBanimator.GetBehaviour<ImpSMB_Pursue>().imp = this;
        SMBanimator.GetBehaviour<ImpSMB_Sprint>().imp = this;
        SMBanimator.GetBehaviour<ImpSMB_Pause>().imp = this;*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
