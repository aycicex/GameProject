using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contoller : MonoBehaviour
{
    [Header("Metrics")]
    public float damp;
    [Range(1,20)]
    public float rotationSpeed; //arayuzden 1ve 20 arasinda bir deger ayarlanması//
    [Range(1,20)]
    public float StrafeTurnSpeed; //kilitli hareketteyken kamera donus hizi tanimlanmasi//
    float normalFow; //normal gorus acisinin tanimlanmasi//
    public float SprintFow; //kosarkenki gorus acisinin tanimlanmasi//
    public float StrafeFow; //kilitli hareket gorus acisinin tanimlanmasi//
    
    float maxSpeed; //max hiz degiskeninin tanimlanmasi//
    float inputX;
    float inputY;

    Animator Anim; //unity animatorunun burda tanimlanmasi//
    Vector3 StickDirection; //vector3 unityde yon, hareket ve konumla ilgili bir fonksiyon//
    Camera mainCam; //ana kameranin tanimlanmasi//
    
    public Transform Model; //karakterin model olarak tanimlanmasi//
    public KeyCode SprintButton = KeyCode.LeftShift; //kosma tusu atamasi//
    public KeyCode WalkButton = KeyCode.C; //yurume tusu atamasi//
    public KeyCode RollButton = KeyCode.LeftControl; //yuvarlanma tusu atamasi//
    //tus atamalari menuden secilerek degistirilebiliyor burdakilerin pek onemi yok//
    public enum MovementType //hareket tiplerinin tanimlanmasi//
    {
        Directional, //yonlu hareket (serbest)//
        Strafe, //yonsuz hareket (karsiya kilitli)//
    };
    public MovementType hareketTipi; //hareket tipi belirlenmesi icin tanim yapilmasi//
    
    
    void Start() //program calistigi anda ilk yapilacak islemler start icinde yazilir//
    {
        Anim = GetComponent<Animator>(); //unity'den animatorun cagirilmasi//
        mainCam = Camera.main; //kameranin ana kamera olarak tanimlanmasi//
        normalFow = mainCam.fieldOfView; //kameraya gorus acisi tanimlanmasi//
    }

    void Update() //program calistigi surece yapilacak islemler (surekli check edilir) update icinde yazilir//
    {
        
        Movement(); //hareket metodunun cagirilmasi//
        

    }
    void Movement() //hareket metodunun tanimlanmasi//
    {

        if(hareketTipi == MovementType.Strafe) //eger hareket tipi kilitli ise//
        {
            //kamera acisinin daraltilmasi//
            mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, StrafeFow, Time.deltaTime * 2);
            // kullanici koordinatlarinin alinmasi//
            inputX = Input.GetAxis("Horizontal");
            inputY = Input.GetAxis("Vertical");
            //koordinatlarin animatore gonderilmesi//
            Anim.SetFloat("iX", inputX, damp, Time.deltaTime * 50);
            Anim.SetFloat("iY", inputY, damp, Time.deltaTime * 50);
            //animatorde strafe moving bool'unun true olarak degistirilmesi//
            Anim.SetBool("strafeMoving", true);

            //hareket ediyor degiskeninin sadece x ve y 0'a esit degil seklinde tanimlanmasi//
            var hareketEdiyor = inputX != 0 || inputY !=0;

            if (hareketEdiyor) //eger x ve y 0'degilse yani hareket ediyor true ise//
            {
                float yawCamera = mainCam.transform.rotation.eulerAngles.y; //kameranin karsiya kilitlenmesi//
                //donus acisinin ve hizinin ayarlanmasi//
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), StrafeTurnSpeed * Time.fixedDeltaTime);
                //unity animatorune strafe moving bool'unun true olarak gonderilmesi//
                Anim.SetBool("strafeMoving", true);
            }
            else //hareket ediyor var'i false yani x ve y 0'a esitse//
            {
                //unity animatore strafe moving bool'unun false olarak gonderilmesi//
                Anim.SetBool("strafeMoving", false);
            }

        }
        
        if(hareketTipi == MovementType.Directional) //hareket tipi serbest ise//
        {
            InputMove(); //yon tuslarinin cagirilmasi//
            InputRotation(); //kamera donus acisinin cagirilmasi//
            StickDirection = new Vector3(inputX, 0, inputY); //yurume acilarinin ayarlanmasi//
            DodgeRoll(); //yuvarlanma metodunun cagirilmasi//

            if(Input.GetKey(SprintButton)) //eger kosma butonunna basilirsa//
            {
                //gorus acisinin arttirilmasi//
                mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, SprintFow, Time.deltaTime * 2);
                //max hareket hizinin 2.5 katina cikmasi//
                maxSpeed = 2.5f;
                inputX = 2.5f* Input.GetAxis("Horizontal");
                inputY = 2.5f* Input.GetAxis("Vertical");
            }
       
            else if(Input.GetKey(WalkButton)) //eger yurume butonuna basilirsa//
            {
                //gorus acisinin normal kalmasi//
                mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, normalFow, Time.deltaTime * 2);
                //max hareket hizinin 10'da 2'ye dusurulmesi//
                maxSpeed = 0.2f;
                inputX = Input.GetAxis("Horizontal");
                inputY = Input.GetAxis("Vertical");
            }
            else //eger normal yurunuyorsa//
            {
                //normal gorus acisina alinmasi//
                mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, normalFow, Time.deltaTime * 2);
                //hizin degismemesi (1 katina esitlenmesi)//
                maxSpeed = 1;
                inputX = Input.GetAxis("Horizontal");
                inputY = Input.GetAxis("Vertical");
            }
        }
        
    
    }
    void InputMove() //yon tuslarina w,a,s,d basildiginda max hiza ulasana kadar hizlanmasi//
    {
        Anim.SetFloat("speed", Vector3.ClampMagnitude(StickDirection, maxSpeed).magnitude, damp , Time.deltaTime * 10);

    }
    void InputRotation() //kameranin donus acisinin ayarlanmasi//
    {
        
        
        Vector3 rotOfset = mainCam.transform.TransformDirection(StickDirection);
        rotOfset.y =0;
        

        Model.forward = Vector3.Slerp(Model.forward, rotOfset, Time.deltaTime * rotationSpeed);
        
    }

    void DodgeRoll() //kacis yuvarlanmasi metodunun tanimlanmasi//
    {
        GetComponent<IKLook>().azal();
        bool roll = true;
        //yuvarlanma tusuna basildiysa ve roll bool'u ture ise//
        if(Input.GetKey(RollButton) && roll == true) 
        {
        Anim.SetBool("canRoll", true); //unity animatorune canRoll bool'unun true olarak gonderilmesi//
        roll = false; 
        //roll bool'unu false hale getirilmesi (yuvarlanma animasyonu bitmeden tekrar yuvarlanamasin diye cunku if kosulunun calismasi icin bunun true olmasi lazim)
        }
        
        else //eger yuvarlanma tusuna basilmamissa//
        {
        GetComponent<IKLook>().art();
        Anim.SetBool("canRoll", false);
        //yuvarlanmaya tekrar hazir olmasi icin unity animatore canRoll bool'unun false olarak gonderilmesi//
        }
    }
    
}
