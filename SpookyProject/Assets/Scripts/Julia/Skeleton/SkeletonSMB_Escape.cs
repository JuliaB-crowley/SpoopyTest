using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSMB_Escape : StateMachineBehaviour
{

    public JUB_SkeletonBehavior skeleton;
    public float maxFleeRate = 0.5f, deviationAngle = 5, fleeLenght;
    float timeSinceFleeBegins;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        skeleton.destinationSetter.target = null;
        skeleton.destinationSetter.enabled = false;

        skeleton.pathfinder.maxSpeed = skeleton.fleeSpeed; 
        skeleton.pathfinder.destination = FleeTarget();
        skeleton.pathfinder.SearchPath();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeSinceFleeBegins += Time.deltaTime;
        if(timeSinceFleeBegins >= maxFleeRate)
        {
            skeleton.pathfinder.destination = FleeTarget();
            skeleton.pathfinder.SearchPath();
            timeSinceFleeBegins = 0;
        }
        Debug.LogWarning(skeleton.toPlayer.magnitude);
        if(Vector2.Distance(skeleton.transform.position, skeleton.player.transform.position) > skeleton.fleeDistance)
        {
            animator.Play("Reconstruction");
        }
    }  

    Vector3 FleeTarget()
    {
        Vector3 result = Vector3.zero;
        RaycastHit2D hit2D;
        float hitLenght = fleeLenght;
        Vector3 angle = -skeleton.toPlayer;
        for (int i = 0; i*deviationAngle < 180; i++)
        {
            angle = -skeleton.toPlayer.normalized;
            angle = Quaternion.AngleAxis(deviationAngle * i, Vector3.forward) * angle;
            hit2D = Physics2D.Raycast(skeleton.transform.position, angle, hitLenght, skeleton.blocksLOS);
            if (hit2D.collider)
            {
                angle = -skeleton.toPlayer.normalized;
                angle = Quaternion.AngleAxis(-deviationAngle * i, Vector3.forward) * angle;
                hit2D = Physics2D.Raycast(skeleton.transform.position, angle, hitLenght, skeleton.blocksLOS);
                if (!hit2D.collider)
                {
                    result = (skeleton.transform.position + angle * fleeLenght);
                    break;
                }
            }
            else
            {
                result = skeleton.transform.position + angle * fleeLenght;
                break;
            }

        }
        if (result == Vector3.zero)
        {

            angle = -skeleton.toPlayer.normalized;
            hit2D = Physics2D.Raycast(skeleton.transform.position, angle, hitLenght, skeleton.blocksLOS);
            hitLenght = hit2D.distance;
            result = skeleton.transform.position + angle * hitLenght;
        }

        return result;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        skeleton.destinationSetter.enabled = true;
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