using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpSMB_Stun : StateMachineBehaviour
{
    public JUB_ImpBehavior imp;
    float timeSinceStun;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.LogWarning("is flashed");
        timeSinceStun = 0;
        imp.destinationSetter.target = null;
        imp.destinationSetter.enabled = imp.pathfinder.enabled = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeSinceStun += Time.deltaTime;
        if (timeSinceStun >= imp.stunTime)
        {
            Debug.LogWarning(timeSinceStun);
            timeSinceStun = 0;
            imp.hasBeenStunned = false;
            animator.Play("Escape");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        imp.destinationSetter.enabled = imp.pathfinder.enabled = true;
    }
}
