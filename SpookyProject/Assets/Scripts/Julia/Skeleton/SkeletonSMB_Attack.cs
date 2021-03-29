using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSMB_Attack : StateMachineBehaviour
{
    public JUB_SkeletonBehavior skeleton;
    bool isInBuildup, isInHitspan, isInRecover;
    public float buildupTime, hitspanTime, recoverTime;
    float currentBuildupTime, currentHitspanTime, currentRecoverTime;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        skeleton.destinationSetter.target = skeleton.transform;
        currentHitspanTime = currentBuildupTime = currentRecoverTime = 0;
        isInBuildup = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(isInBuildup)
        {
            currentBuildupTime += Time.deltaTime;
            if(currentBuildupTime >= buildupTime)
            {
                isInHitspan = true;
                isInBuildup = false;
                currentBuildupTime = currentHitspanTime = 0;
            }
        }

        if(isInHitspan)
        {
            currentHitspanTime += Time.deltaTime;
            HitSpan();
            if(currentHitspanTime >= hitspanTime)
            {
                isInHitspan = false;
                isInRecover = true;
                currentHitspanTime = currentRecoverTime = 0;
            }
        }

        if(isInRecover)
        {
            currentRecoverTime += Time.deltaTime;
            if(currentRecoverTime >= recoverTime)
            {
                isInRecover = false;
                currentRecoverTime = 0;
                animator.Play("Pause");
            }
        }
    }

    bool HitSpan()
    {
        if(skeleton.toPlayer.magnitude - skeleton.player.GetComponent<CircleCollider2D>().radius < skeleton.attackRange)
        {
            skeleton.player.TakeDamages(skeleton.attackDamages);
            return true;
        }
        return false;
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