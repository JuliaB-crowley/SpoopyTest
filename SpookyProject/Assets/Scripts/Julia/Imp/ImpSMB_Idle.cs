using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpSMB_Idle : StateMachineBehaviour
{
    public JUB_ImpBehavior imp;
    bool cyclicPatrol, isCounting = false;
    float timeElapsed;
    Transform currentTarget;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        imp.destinationSetter.enabled = true;
        timeElapsed = 0;
        cyclicPatrol = imp.cyclicPatrol;
        currentTarget = imp.transform;
        float firstTargetDistance = Mathf.Infinity;
        foreach (Transform target in imp.patrolTargets)
        {
            if((target.position - imp.transform.position).magnitude < firstTargetDistance)
            {
                currentTarget = target;
                firstTargetDistance = (target.position - imp.transform.position).magnitude;
                //Debug.LogWarning("distance = " + firstTargetDistance);
            }
        }
        imp.destinationSetter.target = currentTarget;
       
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if((imp.transform.position - currentTarget.position).magnitude < 1 && !isCounting)
        {
            //Debug.LogWarning("Searching new destination");
            NewDestination();
        }
        if(isCounting)
        {
            timeElapsed += Time.deltaTime;
            if(timeElapsed > imp.patrolWaitTime)
            {
                isCounting = false;
                NewDestinationCoroutine();
            }
        }

        if (imp.playerInSight)
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
        if (imp.patrolTargets.IndexOf(currentTarget) >= (imp.patrolTargets.Count - 1))
        {
            //Debug.LogWarning("reached end of list");
            if (cyclicPatrol)
            {
                currentTarget = imp.patrolTargets[0];
            }
            else
            {
                imp.patrolTargets.Reverse();
                currentTarget = imp.patrolTargets[0];
            }
        }
        else
        {
            currentTarget = imp.patrolTargets[imp.patrolTargets.IndexOf(currentTarget) + 1];

        }
        imp.destinationSetter.target = currentTarget;
        //Debug.LogWarning(imp.patrolTargets.IndexOf(currentTarget));
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
}
