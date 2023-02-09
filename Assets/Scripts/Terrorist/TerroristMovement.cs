using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TerroristMovement : MonoBehaviour
{

    public Animator animator;

    public NavMeshAgent nav;

    public bool canMove = true;

    public float maxWaitTime;

    float waitTime = 0;

    bool waiting = true;

    public Transform[] waypoints;


    public void SetCanMove(bool newVal){
        canMove = newVal;
        SetAnimatorWalkingCapabilities(newVal);
    }

    public void SetAnimatorWalkingCapabilities(bool bl){
        nav.isStopped = !bl;
        animator.SetBool("Walking",bl);
    }

    void Update()
    {
        if(!canMove || waypoints.Length == 0){
            return;
        }

        if(waiting){
            if(waitTime>0){
                waitTime-=Time.deltaTime;
            }else{
                SetAnimatorWalkingCapabilities(true);
                waiting = false;
                nav.SetDestination(waypoints[Random.Range(0,waypoints.Length)].position);
            }
        }else{
            if(nav.remainingDistance<=2){
                waitTime = maxWaitTime;
                waiting = true;
                SetAnimatorWalkingCapabilities(false);
            }
        }


    }
}
