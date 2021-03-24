using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpSMB_Pursue : StateMachineBehaviour
{
    public JUB_ImpBehavior imp;
    Vector3 vectorToTravel, toPlayer; 
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        imp.destinationSetter.target = null;
        imp.destinationSetter.enabled = false;

        imp.pathfinder.maxSpeed = imp.pursueSpeed;

        toPlayer.x = (imp.player.transform.position.x - imp.transform.position.x);
        toPlayer.y = (imp.player.transform.position.y - imp.transform.position.y);

        float distanceToPlayer = toPlayer.magnitude;
        float distanceToWalk = distanceToPlayer - imp.stopDistance;
        vectorToTravel = toPlayer.normalized * distanceToWalk;

        imp.pathfinder.destination = imp.transform.position + vectorToTravel;


       
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        toPlayer.x = (imp.player.transform.position.x - imp.transform.position.x);
        toPlayer.y = (imp.player.transform.position.y - imp.transform.position.y);

        float distanceToPlayer = toPlayer.magnitude;
        float distanceToWalk = distanceToPlayer - imp.stopDistance;
        vectorToTravel = toPlayer.normalized * distanceToWalk;

        imp.pathfinder.destination = imp.transform.position + vectorToTravel;

        if (Vector2.Distance(imp.transform.position, imp.pathfinder.destination) < 1)
        {
            Debug.LogWarning("Bidule est arrivé");
            animator.Play("Sprint");
        }

        if(!imp.playerInMemory)
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

