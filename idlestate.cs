using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idlestate : StateMachineBehaviour
{
    Transform player; //player nesnesinin tanimlanmasi//
    float timer; //zamanlayici tanimlanmasi//
    float chaseRange = 20; //takip mesafesinin 20 olarak tanimlanmasi//
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
        timer = 0; //zamanlayici 0'dan baslasin//
        player = GameObject.FindGameObjectWithTag("Player").transform; //player tagi olan oyun nesnesinin karakter olarak tanimlanmasi//
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        timer += Time.deltaTime; //zamanlayicinin degerini gecen sureye gore arttirma// 
        
        if(timer > 5) //zamanlayici 5'i gecerse//
            animator.SetBool("isPatroling", true); //devriye gezmeyi ac//
        
        float distance = Vector3.Distance(player.position, animator.transform.position); //oyuncu ile boss arasindaki mesafenin tanimlanmasi//
        
        if(distance <= chaseRange) //eger mesafe, takip mesafesinden azsa//
            animator.SetBool("isChasing", true); //kovalama-takip'i ac//
        
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
