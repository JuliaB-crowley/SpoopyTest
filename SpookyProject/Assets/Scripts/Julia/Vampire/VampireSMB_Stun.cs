using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireSMB_Stun : StateMachineBehaviour
{
    public JUB_VampireBehavior vampire;
    float timeSinceStun;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.LogWarning("is flashed");
        timeSinceStun = 0;
        vampire.destinationSetter.target = null;
        vampire.destinationSetter.enabled = vampire.pathfinder.enabled = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeSinceStun += Time.deltaTime;
        if(timeSinceStun >= vampire.stunTime)
        {
            Debug.LogWarning(timeSinceStun);
            timeSinceStun = 0;
            vampire.hasBeenStunned = false;
            animator.Play("Escape");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        vampire.destinationSetter.enabled = vampire.pathfinder.enabled = true;
    }

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
