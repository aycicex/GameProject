using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControler : MonoBehaviour
{
    bool isStrafe;
    Animator Anim;
    private void Start()
    {
        Anim = GetComponent<Animator>();

    }
    void Update()
    {
        Anim.SetBool("iS",isStrafe);

        if (Input.GetKeyDown(KeyCode.F))
        {
            isStrafe = !isStrafe;

        }

        if(isStrafe == true)
        {
            GetComponent<controller>().hareketTipi = controller.MovementType.Strafe;
        }
        if(isStrafe == false)
        {
            GetComponent<controller>().hareketTipi = controller.MovementType.Directional;
        }

    }
}