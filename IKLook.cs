using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKLook : MonoBehaviour
{

    Animator Anim;
    Camera mainCam;
    
    void Start()
    {
      Anim = GetComponent<Animator>();
      mainCam = Camera.main;


    }

    private void OnAnimatorIK(int layerIndex)
    {
        
        Anim.SetLookAtWeight(.6f, .5f, 1.2f, .5f, .5f);
        Ray lookAtRay = new Ray(transform.position, mainCam.transform.forward);
        Anim.SetLookAtPosition(lookAtRay.GetPoint(25));
    



    } 
    



}
