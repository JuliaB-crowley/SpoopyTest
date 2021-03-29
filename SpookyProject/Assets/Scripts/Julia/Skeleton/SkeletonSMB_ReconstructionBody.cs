using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSMB_ReconstructionBody : StateMachineBehaviour
{
    public JUB_SkeletonBehavior skeleton;
    public float animationTime = 1;
    float timeSinceAnimationStart;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeSinceAnimationStart = 0;
        skeleton.destinationSetter.target = skeleton.transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeSinceAnimationStart += Time.deltaTime;
        if(timeSinceAnimationStart >= animationTime)
        {
            skeleton.ennemyDamage.currentHealth += skeleton.bodyHealth;
            skeleton.bodyIsBroken = false;
            timeSinceAnimationStart = 0;
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