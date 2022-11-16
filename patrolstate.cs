using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class patrolstate : StateMachineBehaviour
{
    Transform player; //playerin nesne olarak tanimlanmasi//
    float chaseRange = 20; //kovalama mesafesinin 20 olarak tanimlanmasi//
    float timer; //zamanlayici tanimlanmasi//
    List<Transform> waypoints = new List<Transform>(); //belirli devriye noktalarinin belirlenmesi(boss bunlar arasinda gidip gelecek)//
    NavMeshAgent agent; //bossun yuruyebilecegi yerlerin boss'a tanimlanmasi//

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>(); //animatorda boss'a yuruyebilecegi yerlerin atanmasi//
        timer = 0; //zamanlayicinin 0'dan baslamasi//
        player = GameObject.FindGameObjectWithTag("Player").transform; //oyun nesneleri icinden player tagi olan nesnenin bulunup karakter olarak atanmasi//
        GameObject wp = GameObject.FindGameObjectWithTag("waypoint"); //devriye noktalari tagi tanimlanp belirli noktalara atanmasi//
        foreach(Transform t in wp.transform) //bu tagin wp objesi icindeki butun cocuklarina tek tek atanmasi islemi//
            waypoints.Add(t);

        agent.SetDestination(waypoints[Random.Range(0, waypoints.Count)].position); //0 ile devriye noktalarinin sayisi arasinda rastgele bir sayi secilip yeni hedef olarak belirlenmesi//

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(timer > 5 && agent.remainingDistance <= agent.stoppingDistance) //eger zamanlayici 5'i gecerse ve kalan mesafe durma mesafesinden kucuk ya da esitse//
            agent.SetDestination(waypoints[Random.Range(0, waypoints.Count)].position); //0 ile devriye noktalarinin sayisi arasinda rastgele bir sayi secilip yeni hedef olarak belirlenmesi//

        timer += Time.deltaTime; //zamanlayicinin gecen sureye gore arttirilmasi//
        if (timer > 10) //eger zamanlayici 10'u gecerse//
            animator.SetBool("isPatroling", false); //devriye gezmeyi birak// (burdan sonra oldugu yerde soluklanma animasyonu oynatilir)
        
        float distance = Vector3.Distance(player.position, animator.transform.position); //player ile boss arasindaki mesafenin tanimlanmasi//
        
        if(distance <= chaseRange) //eger mesafe, kovalama-takip mesafesinden azsa//
            {
            animator.SetBool("isChasing", true); //kovalamayi baslat//
            animator.SetBool("isPatroling", false); //devriye gezmeyi kapat//
            }
                
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position); //hedef konumu, boss'un kendi konumu olarak atayarak yerinde kalmasini saglamak//
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
