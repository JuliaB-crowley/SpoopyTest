using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireSMB_Escape : StateMachineBehaviour
{
    public JUB_VampireBehavior vampire;
    public float maxFleeRate = 0.5f, deviationAngle = 5, fleeLenght;
    float timeSinceFleeBegins;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.LogWarning("is attempting to escape !");
        vampire.isEscaping = true;
        vampire.destinationSetter.target = null;
        vampire.destinationSetter.enabled = false;

        vampire.pathfinder.maxSpeed = vampire.fleeSpeed;
        vampire.pathfinder.destination = FleeTarget();
        vampire.pathfinder.SearchPath();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeSinceFleeBegins += Time.deltaTime;
        if (timeSinceFleeBegins >= maxFleeRate)
        {
            vampire.pathfinder.destination = FleeTarget();
            vampire.pathfinder.SearchPath();
            timeSinceFleeBegins = 0;
        }
        //Debug.LogWarning(vampire.toPlayer.magnitude);
        if (Vector2.Distance(vampire.transform.position, vampire.player.transform.position) > vampire.fleeDistance)
        {
           animator.Play("Pause");
        }
    }

    Vector3 FleeTarget()
    {
        Vector3 result = Vector3.zero;
        RaycastHit2D hit2D;
        float hitLenght = fleeLenght;
        Vector3 angle = -vampire.toPlayer;
        for (int i = 0; i * deviationAngle < 180; i++)
        {
            angle = -vampire.toPlayer.normalized;
            angle = Quaternion.AngleAxis(deviationAngle * i, Vector3.forward) * angle;
            hit2D = Physics2D.Raycast(vampire.transform.position, angle, hitLenght, vampire.blocksLOS);
            if (hit2D.collider)
            {
                angle = -vampire.toPlayer.normalized;
                angle = Quaternion.AngleAxis(-deviationAngle * i, Vector3.forward) * angle;
                hit2D = Physics2D.Raycast(vampire.transform.position, angle, hitLenght, vampire.blocksLOS);
                if (!hit2D.collider)
                {
                    result = (vampire.transform.position + angle * fleeLenght);
                    break;
                }
            }
            else
            {
                result = vampire.transform.position + angle * fleeLenght;
                break;
            }

        }
        if (result == Vector3.zero)
        {

            angle = -vampire.toPlayer.normalized;
            hit2D = Physics2D.Raycast(vampire.transform.position, angle, hitLenght, vampire.blocksLOS);
            hitLenght = hit2D.distance;
            result = vampire.transform.position + angle * hitLenght;
        }

        return result;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        vampire.destinationSetter.enabled = true;
        vampire.destinationSetter.target = null;
        vampire.isEscaping = false;
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
