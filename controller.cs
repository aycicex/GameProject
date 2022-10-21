using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{
    [Header("Metrics")]
    
    public float damp;
    
    [Range(1,20)]
    public float rotationSpeed;
    
    [Range(1,20)]
    public float StrafeTurnSpeed;
    
    public float SprintFov;
    
    float normalFov;
    float inputX;
    float inputY;
    float maxSpeed;
    
    public Transform Model;

    Animator Anim;
    Vector3 StickDirection;
    Camera mainCam;

    public KeyCode SprintButton = KeyCode.LeftShift;
    public KeyCode WalkButton = KeyCode.C;
    
    public enum MovementType
    {
        Directional,
        Strafe,

    };
    public MovementType hareketTipi;

    void Start()
    {
        Anim = GetComponent<Animator>();
        mainCam = Camera.main;
        normalFov = mainCam.fieldOfView;


    }

    private void LateUpdate() 
    {
       
        InputMove();
        InputRotation();
        Movement();


    void Movement()
    {
        
        if(hareketTipi == MovementType.Strafe)
        {
            inputX = Input.GetAxis("Horizontal");
            inputY = Input.GetAxis("Vertical");
            
            Anim.SetFloat("iX", inputX, damp, Time.deltaTime * 10);
            Anim.SetFloat("iY", inputY, damp, Time.deltaTime * 10);
            Anim.SetBool("strafeMoving", true);
            
            var hareketEdiyor = inputX != 0 || inputY !=0;

            if(hareketEdiyor)
            {
                float yawCamera = mainCam.transform.rotation.eulerAngles.y;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0,yawCamera,0), StrafeTurnSpeed * Time.fixedDeltaTime);
                Anim.SetBool("strafeMoving", true);

            }

            else
            {
                Anim.SetBool("strafeMoving", false);
            }


        }
        
        
        if(hareketTipi == MovementType.Directional)
        {

            
            StickDirection = new Vector3(inputX, 0, inputY);
        
            if(Input.GetKey(SprintButton))
            {
                mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, SprintFov, Time.deltaTime * 2);
                maxSpeed = 2;
                inputX = 2 * Input.GetAxis("Horizontal");
                inputY = 2 * Input.GetAxis("Vertical");
            

            }

            else if(Input.GetKey(WalkButton))
            {
                mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, normalFov, Time.deltaTime * 2);
                maxSpeed = 0.2f;
                inputX = Input.GetAxis("Horizontal");
                inputY = Input.GetAxis("Vertical");
            }
        
            else
            {
                mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, normalFov, Time.deltaTime * 2);
                maxSpeed = 1;
                inputX = Input.GetAxis("Horizontal");
                inputY = Input.GetAxis("Vertical");
            }
        
        }
        
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
        
        StickDirection = new Vector3(inputX, 0, inputY);
        
        if(Input.GetKey(SprintButton))
        {
            mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, SprintFov, Time.deltaTime * 2);
            maxSpeed = 2;
            inputX = 2 * Input.GetAxis("Horizontal");
            inputY = 2 * Input.GetAxis("Vertical");
            

        }

        else if(Input.GetKey(WalkButton))
        {
            mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, normalFov, Time.deltaTime * 2);
            maxSpeed = 0.2f;
            inputX = Input.GetAxis("Horizontal");
            inputY = Input.GetAxis("Vertical");
        }
        
        else
        {
            mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, normalFov, Time.deltaTime * 2);
            maxSpeed = 1;
            inputX = Input.GetAxis("Horizontal");
            inputY = Input.GetAxis("Vertical");
        }
    }
    }

    void InputMove()
    {
        Anim.SetFloat("speed", Vector3.ClampMagnitude(StickDirection, maxSpeed).magnitude, damp , Time.deltaTime * 10);

    }
    
    
    void InputRotation()
    {
        
        
        Vector3 rotOfset = mainCam.transform.TransformDirection(StickDirection);
        rotOfset.y =0;
        

        Model.forward = Vector3.Slerp(Model.forward, rotOfset, Time.deltaTime * rotationSpeed);
        
    }
    
    
    void Update()
    {
        
    }
}
