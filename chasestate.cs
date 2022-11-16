using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class chasestate : StateMachineBehaviour
{
    Transform player;
    NavMeshAgent agent;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) //sadece kosul basladiginda bir kez calisacak olanlar//
    {
        agent = animator.GetComponent<NavMeshAgent>(); //boss'un yuruyebilecegi yerlerin taranmasi ve engellerin yurunemez olarak isaretlenmesi// 
        player = GameObject.FindGameObjectWithTag("Player").transform; //player tagiyle etiketli olan nesneyi bulup karakter olarak atanmasi//

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) //kosul devam ettigi surece devamli calisacak olanlar//
    {
        agent.SetDestination(player.position); //takip edilecek rotayi player'in konumu olarak tanimlanmasi//
        float distance = Vector3.Distance(player.position, animator.transform.position); //player ile boss'un arasindaki mesafenin float olarak tanimlanmasi//

        if(distance > 20.1f) //mesafe 20.1den buyukse//
            animator.SetBool("isChasing", false); //takip komutunun kapanmasi//
        
        if(distance < 4.5f) //mesafe 4.5den kucukse//
            animator.SetBool("isAttacking", true);  //saldiri komutuna gecilmesi//
            
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) //sadece kosul biterken bir kez yapilacak olanlar//
    {
        agent.SetDestination(animator.transform.position); //takip edilecek rotayi boss'un kendi konumu olarak secerek yerinde kalmasinin saglanmasi//
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
