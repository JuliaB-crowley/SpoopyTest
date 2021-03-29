using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSMB_Iddle : StateMachineBehaviour
{
    public JUB_SkeletonBehavior skeleton;
    bool cyclicPatrol, isCounting = false;
    float timeElapsed;
    Transform currentTarget;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        skeleton.destinationSetter.enabled = true;

        skeleton.pathfinder.maxSpeed = skeleton.iddleSpeed;

        timeElapsed = 0;
        cyclicPatrol = skeleton.cyclicPatrol;
        currentTarget = skeleton.transform;
        float firstTargetDistance = Mathf.Infinity;
        foreach (Transform target in skeleton.patrolTargets)
        {
            if ((target.position - skeleton.transform.position).magnitude < firstTargetDistance)
            {
                currentTarget = target;
                firstTargetDistance = (target.position - skeleton.transform.position).magnitude;
                //Debug.LogWarning("distance = " + firstTargetDistance);
            }
        }
        skeleton.destinationSetter.target = currentTarget;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if ((skeleton.transform.position - currentTarget.position).magnitude < 1 && !isCounting)
        {
            Debug.LogWarning("Searching new destination");
            NewDestination();
        }
        if (isCounting)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed > skeleton.patrolWaitTime)
            {
                isCounting = false;
                NewDestinationCoroutine();
            }
        }

        if (skeleton.playerInSight)
        {
            animator.Play("Pursue");
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

    void NewDestination()
    {
        //Debug.LogWarning(imp.patrolTargets.IndexOf(currentTarget));
        isCounting = true;
        timeElapsed = 0;
    }

    void NewDestinationCoroutine()
    {
        if (skeleton.patrolTargets.IndexOf(currentTarget) >= (skeleton.patrolTargets.Count - 1))
        {
            //Debug.LogWarning("reached end of list");
            if (cyclicPatrol)
            {
                currentTarget = skeleton.patrolTargets[0];
            }
            else
            {
                skeleton.patrolTargets.Reverse();
                currentTarget = skeleton.patrolTargets[0];
            }
        }
        else
        {
            currentTarget = skeleton.patrolTargets[skeleton.patrolTargets.IndexOf(currentTarget) + 1];

        }
        skeleton.destinationSetter.target = currentTarget;
        //Debug.LogWarning(imp.patrolTargets.IndexOf(currentTarget));
    }
}
