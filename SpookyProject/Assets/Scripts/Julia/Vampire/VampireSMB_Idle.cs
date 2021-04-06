using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireSMB_Idle : StateMachineBehaviour
{
    public JUB_VampireBehavior vampire;
    bool cyclicPatrol, isCounting = false;
    float timeElapsed;
    Transform currentTarget;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        vampire.destinationSetter.enabled = true;

        vampire.pathfinder.maxSpeed = vampire.iddleSpeed;

        timeElapsed = 0;
        cyclicPatrol = vampire.cyclicPatrol;
        currentTarget = vampire.transform;
        float firstTargetDistance = Mathf.Infinity;
        foreach (Transform target in vampire.patrolTargets)
        {
            if ((target.position - vampire.transform.position).magnitude < firstTargetDistance)
            {
                currentTarget = target;
                firstTargetDistance = (target.position - vampire.transform.position).magnitude;
                //Debug.LogWarning("distance = " + firstTargetDistance);
            }
        }
        vampire.destinationSetter.target = currentTarget;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if ((vampire.transform.position - currentTarget.position).magnitude < 1 && !isCounting)
        {
            Debug.LogWarning("Searching new destination");
            NewDestination();
        }
        if (isCounting)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed > vampire.patrolWaitTime)
            {
                isCounting = false;
                NewDestinationCoroutine();
            }
        }

        if (vampire.playerInSight)
        {
            animator.Play("Pursue");
        }
    }

    void NewDestination()
    {
        //Debug.LogWarning(imp.patrolTargets.IndexOf(currentTarget));
        isCounting = true;
        timeElapsed = 0;
    }

    void NewDestinationCoroutine()
    {
        if (vampire.patrolTargets.IndexOf(currentTarget) >= (vampire.patrolTargets.Count - 1))
        {
            //Debug.LogWarning("reached end of list");
            if (cyclicPatrol)
            {
                currentTarget = vampire.patrolTargets[0];
            }
            else
            {
                vampire.patrolTargets.Reverse();
                currentTarget = vampire.patrolTargets[0];
            }
        }
        else
        {
            currentTarget = vampire.patrolTargets[vampire.patrolTargets.IndexOf(currentTarget) + 1];

        }
        vampire.destinationSetter.target = currentTarget;
        //Debug.LogWarning(imp.patrolTargets.IndexOf(currentTarget));
    }
}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}

