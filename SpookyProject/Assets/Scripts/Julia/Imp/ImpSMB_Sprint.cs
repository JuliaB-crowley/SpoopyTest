using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpSMB_Sprint : StateMachineBehaviour
{
    public JUB_ImpBehavior imp;
    public Vector3 vectorToSprint;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.LogWarning("was called");

        imp.pathfinder.maxSpeed = imp.sprintSpeed;

        //choix de la direction de sprint
        RaycastHit2D hit2D;
        float hitLength = imp.sprintDistance;

        hit2D = Physics2D.Raycast(imp.transform.position, imp.toPlayer, hitLength, imp.blocksLOS);
        if (hit2D.collider)
        {
            hitLength = hit2D.distance;
        }

        //sprint
        imp.isAttacking = true;
        vectorToSprint = imp.toPlayer.normalized * hitLength;

        imp.pathfinder.destination = imp.transform.position + vectorToSprint;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        //passage d'état
        if (Vector3.Distance((imp.transform.position), imp.pathfinder.destination) < 1)
        {
            imp.isAttacking = false;
            Debug.LogWarning("Bidule a fini son sprint");
            animator.Play("Pause");
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

