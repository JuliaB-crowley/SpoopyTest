using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireSMB_Attack : StateMachineBehaviour
{
    float timeSinceBuildup, timeSinceRecovery;
    bool isInBuildup, isInRecovery;

    public JUB_VampireBehavior vampire;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeSinceBuildup = timeSinceRecovery = 0;
        isInBuildup = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (isInBuildup)
        {
            timeSinceBuildup += Time.deltaTime;
            if (timeSinceBuildup >= vampire.buildupTime)
            {
                isInRecovery = true;
                isInBuildup = false;
                timeSinceBuildup = 0;
                Spawn();
            }
        }

        if(isInRecovery)
        {
            timeSinceRecovery += Time.deltaTime;
            if(timeSinceRecovery >= vampire.recoveryTime)
            {
                timeSinceRecovery = 0;
                animator.Play("Pause");
            }
        }
    }

    void Spawn()
    {
        Instantiate(vampire.batPrefab, vampire.transform.position, Quaternion.LookRotation(Vector3.forward, vampire.toPlayer));
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
