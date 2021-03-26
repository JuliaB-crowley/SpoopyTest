using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSMB_Pursue : StateMachineBehaviour
{
    public JUB_SkeletonBehavior skeleton;
    Vector3 vectorToTravel, toPlayer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("skeleton has seen player");
        skeleton.destinationSetter.target = null;
        skeleton.destinationSetter.enabled = false;

        skeleton.pathfinder.maxSpeed = skeleton.pursueSpeed;

        toPlayer.x = (skeleton.player.transform.position.x - skeleton.transform.position.x);
        toPlayer.y = (skeleton.player.transform.position.y - skeleton.transform.position.y);

        float distanceToPlayer = toPlayer.magnitude;
        float distanceToWalk = distanceToPlayer - skeleton.stopDistance;
        vectorToTravel = toPlayer.normalized * distanceToWalk;

        skeleton.pathfinder.destination = skeleton.transform.position + vectorToTravel;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        toPlayer.x = (skeleton.player.transform.position.x - skeleton.transform.position.x);
        toPlayer.y = (skeleton.player.transform.position.y - skeleton.transform.position.y);

        float distanceToPlayer = toPlayer.magnitude;
        float distanceToWalk = distanceToPlayer - skeleton.stopDistance;
        vectorToTravel = toPlayer.normalized * distanceToWalk;

        skeleton.pathfinder.destination = skeleton.transform.position + vectorToTravel;

        if (Vector2.Distance(skeleton.transform.position, skeleton.pathfinder.destination) < 1)
        {
            Debug.LogWarning("Skeleton est arrivé");
            animator.Play("Attack");
        }

        if (!skeleton.playerInMemory)
        {
            Debug.LogWarning("returned in patrol");
            animator.Play("Idle");
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
}