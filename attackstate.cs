using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackstate : StateMachineBehaviour
{
    Transform player;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) //sadece kosul saglandiginda bir kez calistirilacak olanlar//
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; //Oyun nesneleri icinden player etiketine sahip olani bulup karakter olarak atama//

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) //kosuldan cikana kadar surekli calisacak olanlar//
    {
        float distance = Vector3.Distance(player.position, animator.transform.position); //player ve boss arasindaki mesafeyi float olarak tanimlama//
        animator.transform.LookAt(player); //sadece saldirma bool'u true oldugunda surekli player'a donme//
        if(distance > 4.5f) //eger mesafe 4.5dan fazla ise//
            animator.SetBool("isAttacking", false); //saldirabilmeyi kapat//
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
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
