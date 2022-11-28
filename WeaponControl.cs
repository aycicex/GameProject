using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    public GameObject trailBS; //kilicin arkasindan iz birakmasi icin oyun (unity) icerisinden trail tanimlanmasi//
    public GameObject trailBSRH;
    public GameObject trailBSLH;
    public GameObject trailK;
    public GameObject trailTB;
    
    bool canAttack = true; //saldirabilir mi degiskeni icin true bir bool tanimlanmasi//
    bool isStrafe = false; //kilitli hareket mi yapiyor degiskeni icin false bir bool tanimlanmasi//
    
    Animator anim; //unity animatorunun tanimlanmasi//

    public GameObject rightbs; //karakterin sag elindeki bicagin tanimlanmasi//
    public GameObject leftbs; //karakterin sol elindeki bicagin tanimlanmasi//
    public GameObject bs; //karakterin cift elli bicagin tanimlanmasi//
    public GameObject backbs; //karakterin bicagi sirtina birakmasi//
    public GameObject katanarh; //karakterin sag eldeki katanasinin tanimlanmasi//
    public GameObject katanaback; //karakterin katanayi birakmasi//
    public GameObject katanabackempty; //katana kilifinin tanimlanmasi//
    public GameObject tirpanrh; //karakterin sag elindeki tirpanin tanimlanmasi//
    public GameObject tirpanempty; //karakterin tirpani sirtina takmasi//


    private void Start()
    {
        anim = GetComponent<Animator>(); //animatorun cagirilmasi//
        trailClose(); //kilic izi kapatma metodunun cagirilmasi//
    }
    
    
    void Update()
    {
        anim.SetBool("iS", isStrafe); //basta tanimladigimiz kilitli hareket boolunu unity animatore iS adiyla gonderilmesi//
        
        if(Input.GetKeyDown(KeyCode.F)) //eger F tusuna basilirsa//
        {
            isStrafe = !isStrafe; // Strafe bool'unu tersi ile degistir//
        }

        if(Input.GetKey(KeyCode.R)) //R tusuna basilinca//
        {
            anim.SetBool("bladesword", true); //animatore cift bicagi ac komutu gonder//
            anim.SetBool("katana", false); //animatore katanayi kapat komutu gonder//
            anim.SetBool("tirpan", false); //animatore tirpani kapat komutu gonder//
            equipbs();
        
        }

        if(Input.GetKey(KeyCode.Q))
        {

            anim.SetBool("katana",true); //animatore katanayi ac komutu gonder//
            anim.SetBool("tirpan",false); //animatore tirpani kapat komutu gonder//
            anim.SetBool("bladesword",false); //animatore cift bicagi kapat komutu gonder//
            equipkatana();
          
        }

        if(Input.GetKey(KeyCode.E))
        {

            anim.SetBool("katana",false); //animatore katanayi kapat komutu gonder//
            anim.SetBool("tirpan",true); //animatore tirpani ac komutu gonder//
            anim.SetBool("bladesword",false); //animatore cift bicagi kapat komutu gonder//
            equiptirpan();
                          
        }

        if(Input.GetKey(KeyCode.Space) && isStrafe == true && canAttack == true) //eger sol mouse basildiysa ve karakter kilitli hareketteyse ve saldirabilir bool'u dogru ise//
        {
            anim.SetTrigger("Saldir"); //animatore saldir trigger'i (tetikleyicisi) gonderilmesi//
            
        }

        if(isStrafe == true) //eger kilitli hareket yapiliyorsa//
        {
            GetComponent<Contoller>().hareketTipi = Contoller.MovementType.Strafe; //onceki karakter hareket dosyasindan (Controller) kilitli olan hareket tipinin getirilmesi//
            GetComponent<IKLook>().azal(); //karakterin kamera yonunde bakma metodunun azalarak kapanma fonksiyonunun, diger kod dosyasi olan IKLook'dan getirilmesi//

            if(Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.LeftAlt)) //kosma ya da yurume tuslarina basilirsa//
            {
                isStrafe = false; //karakterin kilitli hareketi birakmasi//
            }
        
        }
        
        if(isStrafe == false) //karakter kilitli harekette degilse//
        {
            GetComponent<Contoller>().hareketTipi = Contoller.MovementType.Directional; //onceki karakter hareket dosyasindan (Controller) serbest olan hareket tipinin getirilmesi//
            GetComponent<IKLook>().art(); //karakterin kamera yonunde bakma metodunun artarak acilma fonksiyonunun, diger kod dosyasi olan IKLook'dan getirilmesi//

            if(Input.GetKeyDown(KeyCode.Space)) //eger sol mouse basilirsa//
        {
            isStrafe = true; //tekrar kilitli harekete gecmesi//
        }
        }

    }
    
            
    void blades() //cift elli bicagi tek ele bolme metodunun tanimlanmasi//
    {
        bs.SetActive(false);
        rightbs.SetActive(true); 
        leftbs.SetActive(true); 
        backbs.SetActive(false);
        katanaback.SetActive(true);
        katanarh.SetActive(false);
        katanabackempty.SetActive(true);
        tirpanrh.SetActive(false);
        tirpanempty.SetActive(true);
    }
    
    void equipbs() //cift elli bicagi kusanma metodunun tanimlanmasi//
    {
        bs.SetActive(true); 
        rightbs.SetActive(true);
        backbs.SetActive(false);
        katanaback.SetActive(true);
        katanarh.SetActive(false);
        katanabackempty.SetActive(true);
        tirpanrh.SetActive(false);
        tirpanempty.SetActive(true);
    }    
    void sword() //tek elli bicaklari birlestirip cift elli yapma metodunun tanimlanmasi//
    {
        leftbs.SetActive(false); 
        rightbs.SetActive(true);
        bs.SetActive(true); 
        backbs.SetActive(false);
        katanaback.SetActive(true);
        katanarh.SetActive(false);
        katanabackempty.SetActive(true);
        tirpanrh.SetActive(false);
        tirpanempty.SetActive(true);
    }    
    void unequipbs() //cift bicagi birakma metodunun tanimlanmasi//
    {
        
        leftbs.SetActive(false); 
        rightbs.SetActive(false); 
        bs.SetActive(false);
        backbs.SetActive(true);
        katanaback.SetActive(true);
        katanarh.SetActive(false);
        katanabackempty.SetActive(true);
        tirpanrh.SetActive(false);
        tirpanempty.SetActive(true);
    }

    void equipkatana() //katanayi alma metodunun tanimlanmasi//
    {
        
        leftbs.SetActive(false); 
        rightbs.SetActive(false); 
        bs.SetActive(false); 
        backbs.SetActive(true);
        katanaback.SetActive(true);
        katanarh.SetActive(true);
        katanabackempty.SetActive(false);
        tirpanrh.SetActive(false);
        tirpanempty.SetActive(true);
    }
    void unequipkatana() //katanayi birakma metodunun tanimlanmasi//
    {
        leftbs.SetActive(false); 
        rightbs.SetActive(false); 
        bs.SetActive(false); 
        backbs.SetActive(true);
        katanaback.SetActive(false);
        katanarh.SetActive(true);
        katanabackempty.SetActive(true);
        tirpanrh.SetActive(false);
        tirpanempty.SetActive(true);
    }

    void equiptirpan() //tirpani alma metodunun tanimlanmasi//
    {
        
        leftbs.SetActive(false); 
        rightbs.SetActive(false); 
        bs.SetActive(false); 
        backbs.SetActive(true);
        katanaback.SetActive(true);
        katanarh.SetActive(false);
        katanabackempty.SetActive(true);
        tirpanrh.SetActive(true);
        tirpanempty.SetActive(false);
    }
    
    void unequiptirpan() //tirpani birakma metodunun tanimlanmasi//
    {
        leftbs.SetActive(false); 
        rightbs.SetActive(false); 
        bs.SetActive(false);
        backbs.SetActive(true);
        katanaback.SetActive(true);
        katanarh.SetActive(false);
        katanabackempty.SetActive(true);
        tirpanrh.SetActive(false);
        tirpanempty.SetActive(true);
    }

    public void trailOpen() //kilicin iz cikarma metodunun tanimlanmasi//
    {
    
        trailBS.SetActive(true);
        trailBSLH.SetActive(true);
        trailBSRH.SetActive(true);
        trailK.SetActive(true);
        trailTB.SetActive(true);

    }
    
    public void trailClose() //kilic izinin kapanma metodunun tanimlanmasi//
    {
    
        trailBS.SetActive(false);
        trailBSLH.SetActive(false);
        trailBSRH.SetActive(false);
        trailK.SetActive(false);
        trailTB.SetActive(false);
        
        
    }
      

}
