using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;


public class carControl : MonoBehaviour
{
    public Rigidbody rb;
    public AudioSource au_crash,brick_crash,wbox_crash,barrel_crash;
    public AudioSource engine_sound;

    public GameObject t_barrel;
    public GameObject second_cam;
    public GameObject canvas;
    public GameObject canvas_2;
    public GameObject button_1;
    public GameObject button_2;
    public GameObject roof_cam;


    public GameObject frLeft;
    public GameObject frRight;
    public GameObject bcLeft;
    public GameObject bcRight;

    public TextMeshProUGUI tot_score;
    public TextMeshProUGUI required;
    public TextMeshProUGUI score_text;
    public TextMeshProUGUI tomato_number;

    public WheelCollider frLeftCol;
    public WheelCollider frRightCol;
    public WheelCollider bcLeftCol;
    public WheelCollider bcRightCol;

    private AudioSource point;
    private GameObject[] b_spawn_place;
    private GameObject[] destroyer;

    private Touch touch;
    private Quaternion rot;
    private Vector3 pos;

    public float maxEngineHp;
    public float maxAngle;
    public float engine;
    public float brakePower;
    public float audio_s_settings;
    public float audio_s;
    public float crRotate;

    public int score;
    public int final_score;
    public int cc_tomat_barrel;

    private bool crash_activity = false,point_check=false;
    [HideInInspector]public bool IsMove = true;
    private bool carÝsmove = true;

    private int q = 0, m = 0, tomato_num = 0;

    private float _brakeTorque;
    private void Awake()
    {
        Application.targetFrameRate = 60;
     
        //Oyun içindeki private olan nesnelerin atamasý yapýlýyor.
        b_spawn_place = GameObject.FindGameObjectsWithTag("b_spawn");
        point = GameObject.Find("player/Crash_sound/point").GetComponent<AudioSource>();
        

    }
    void Start()
    {
        
        Invoke("eng_s_start", 0.2f);
        //Araç motorunu bir fonksiyon aracýlýðý ile program baþladýktan belirli bir süre sonra çalýþtýrýyoruz.
        InvokeRepeating("eng_speed", .0f * Time.deltaTime, .025f);
       
    }
    
    void eng_speed()  
    {
        //Araç hýzýný kontrol ediyor
        if (IsMove && rb.velocity.magnitude*2 < 40.0f)
        {
            //Aracýn hareketini gerçekleþtirmesi rigidbody yani fizik bileþenine eklenen güç hesaplanýp ekleniyor.
            rb.AddForce(Vector3.forward * 200.0f * maxEngineHp * Time.deltaTime, ForceMode.Force);
        }
        
    }
    void eng_s_start() 
    {
        
        engine_sound.Play();
    }
   

    void Update()
    {
        //Araç hareket halindeyken hýz deðerlerini tutar ve motor sesini araç hýzýna göre ayarlar.
        if (carÝsmove==true) 
        {
            // "c_tom_bar" etiketine sahip oyun nesnelerini sahne içerisinde düzenli olarak destroyer dizisine atar.
            destroyer = GameObject.FindGameObjectsWithTag("c_tom_bar");

            Debug.Log(destroyer.Length);
            Debug.Log(rb.velocity.magnitude * 2);
            float x = rb.velocity.magnitude * 2;
            engine_sound.pitch = x / 100 * 3;
            audio_s = transform.InverseTransformDirection(rb.velocity).z * audio_s_settings;
        }
        

        
        //Mobil telefon ekranýna temasý kontrol eder.
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            //Eðer temas delta x pozisyonunda 15.0 piksellik bir kayma gerçekleþtirirse 
            if (touch.deltaPosition.x >15.0f)
            {
                
                //30 derecelik bir dönüþ deðeri oluþturulur
                crRotate = 30;
                //Ön tekerlerin colliderlerýnýn dönüþ açýlarýný crRotate açýsýna eþitler ve aracýn tekerlerinin dönmesini saðlar.
                frRightCol.steerAngle = crRotate;
                frLeftCol.steerAngle = crRotate;
                
           
            }
           
            
            // Temasýn delta x pozisyonunda eksi yönde yani ha<reket etmesi halinde ayný iþlevi gerçekletirir araç tekerleri sola 30 derecelik bi açý verir.
            if (touch.deltaPosition.x <-15.0f)
            {
                
                crRotate = -30f;
                frRightCol.steerAngle = crRotate;
                frLeftCol.steerAngle = crRotate;
                
               
            }
           

        }
        //Temas olmamasý halinde aracýn stabilitesini korumak için teker açýlarýný sýfýrlar
        if (Input.touchCount == 0)
        {
            crRotate = 0;
            frRightCol.steerAngle = crRotate;
            frLeftCol.steerAngle = crRotate;
        }

        _brakeTorque = brakePower * Mathf.Abs(Input.GetAxis("Jump"));

        //Fren torku belirli bir deðerin altýnda olduðu takdirde 
        if (_brakeTorque < 0.05f) 
        {
          //Ön tekerlere etki eden motor torklarý belirlenen motor gücüne göre aktarýlýr
            frLeftCol.motorTorque = engine;
            frRightCol.motorTorque = engine;

            //Tekerlere uygulanan döndürme kuvvetinin etkilenmemesi ve boþa enerji kaybý olmamasý için fren torklarý 0 deðerine sabitlenir.
            bcLeftCol.brakeTorque = 0;
            bcRightCol.brakeTorque = 0;
            frLeftCol.brakeTorque = 0;
            frRightCol.brakeTorque = 0;
        }
        else
        {
            bcLeftCol.brakeTorque = _brakeTorque;
            bcRightCol.brakeTorque = _brakeTorque;
            frLeftCol.brakeTorque = _brakeTorque;
            frRightCol.brakeTorque = _brakeTorque;

        }

        VirtualWheels();

        //Aracýn transfom bileþenindekiy deðeri sahnenin delta y 0 posizyonundan daha düþük bir deðere sahip loursa
        if (transform.position.y < 0.0f) 
        {
           //Kullanýcýnýn karþýsýna kullanýcý arayüzü(UI) ekranlarý gelir
            canvas.SetActive(true);
            engine_sound.Stop();
            canvas_2.SetActive(false);

            button_1.SetActive(false);
            button_2.SetActive(false);
           
            //Oyunun zaman ölçeði 0 deðerine yani hareketsizliðe eþitlenir
            Time.timeScale = 0;
        }

        if (point_check == true)
        {
            InvokeRepeating("move_barrel", .0f, 2.0f);
            
        }
        

    }

    //OnTriggerEnter fonksiyonu colliderlar arasýnda yumuþak bir çarpýþma olduðu sýrada fonksiyon aktif olur
    private void OnTriggerEnter(Collider other)
    {
        
        //Aracýn colliderý etiketi nos olan bir oyun nesnesiyle yumuþak çarpýþma gerçekleþtirdiði zaman
        if (other.gameObject.tag == "nos")
        {

            //Toplanýlan ödül 1 arttýrýlý UI ekranýna yazýlýr.
            tomato_num += 1;
            tomato_number.SetText(tomato_num.ToString());
            //Oyun içi AudioSource bileþeni aktif edilir ve 8-bitlik kazaným sesi çalýnýr.
            point.Play();
            //Temas edilen oyun nesnesi sahneden silinir
            Destroy(other.gameObject);

            //Prefab olarak oluþturulan oyun nesnesi her temas halinde sahne içerisinde belirlenen transform noktasýna Instantine edilir.
            q = destroyer.Length-1;
            GameObject new_t_barrel = Instantiate(t_barrel, new Vector3(b_spawn_place[q].transform.position.x, b_spawn_place[q].transform.position.y, b_spawn_place[q].transform.position.z), Quaternion.identity);
            //Instantine edilen nesne ile araç nesnemizin transformlarý birbirine sabitlenir.
            new_t_barrel.transform.parent = gameObject.transform;
                

        }
        if (other.gameObject.tag == "Finish")
        { 
            //Sistemi yormamak için daha önce sistemde çalýþtýrmýþ olduðumuz Invokelar iptal edilir
            CancelInvoke();

            if (crash_activity == false)
            {

                engine_sound.Stop();
                carÝsmove = false;
                
                //crash_activity deðiþkeni ayný collider içerisindeki ince katman çarpýþmalarýný engellemek ve sistemde açýk býrakmamak için her çarpýþmada aktif olur.
                crash_activity = true;

                // "inactive" fonksiyonu 1.0sn gecikmeli çaðýrýlarak çarpýþma bileþenini tekrar aktif eder.
                Invoke("inactive", 1.0f);
                

            }
            
            GameObject[] c_tom = GameObject.FindGameObjectsWithTag("c_tom_bar");

            //Sahne içerisindeki kýrýlmayý engellemek için kullanýcýn 0 ödül toplama yapmasý halinde alternatif oyun sonuna gidilir.
            if (c_tom.Length <= 1) 
            {

                canvas_2.SetActive(false);
                canvas.SetActive(true);

                Time.timeScale = 0;
            
            
            }
        }

        if (other.gameObject.tag == "point")
        {
            Debug.Log("Flying Barrel");

            //Prefab olarak çaðýrýlan nesnelerin sahne içerisinde referans olarak bulunmasý gerektiði için dizi içerisinde referans nesnesini yok saymak için referans nesnesi dizide yok sayýlýr
            m = destroyer.Length - 1;
            point_check = true;
            GetComponent<BoxCollider>().enabled = false;
            

        }
         

    }
    private void  move_barrel() 
    {

        point_check = false;
        
        
        if (m>0) 
        {
            //destroyer dizisi içerisindeki nesneler "point_2" nesnesini bulunduðu konuma animasyon aracýlýðý ile konumu eþitlenir. 
            destroyer[m].transform.LeanMove(GameObject.Find("point_2").transform.position, 4).setEase(LeanTweenType.easeInOutSine);
            destroyer[m].LeanRotate(new Vector3(0, 180, 0), 1.4f).setEase(LeanTweenType.easeInBounce);
           
                m -= 1;
            

        }


    }
  
    //OnCollisonEnter colliderler arasýnda sert çarpýþamlarý tetikler.
    private void OnCollisionEnter(Collision collision)
    {

        Debug.Log(collision.gameObject.tag);

//Sahnede bulunan engeller taglarý ile biribirinden ayrýlýr 
        if (collision.gameObject.tag == "tire")
        {

            
            //Çarpýþma ikileminii önlemek için "crash_activity" ile koþul deðerlendirilir 
            if (crash_activity==false) 
            {
               
                tomato_minus();
                // Motor sesi, çarpýþma seslerini tetikler 
                engine_sound.pitch -= .05f;
                au_crash.Play();
                crash_activity = true;
                Invoke("inactive", 1.0f);

                //destroyer dizisinde eleman var ise çarpýþam sonucu topladýðý bir kazanýmý oyun sahnesinden siler.
                if (destroyer.Length>1) { Destroy(destroyer[destroyer.Length-1]); }
               
                
               
            
            }
            

        }
        if (collision.gameObject.tag == "brick") 
        {
            if (crash_activity == false)
            {
                tomato_minus();
                engine_sound.pitch -= .05f;
                brick_crash.Play();
                crash_activity = true;
                Invoke("inactive", 1.0f);

                if (destroyer.Length > 1) { Destroy(destroyer[destroyer.Length - 1]); }




            }
        }
        if (collision.gameObject.tag == "box")
        {
            if (crash_activity == false)
            {
                tomato_minus();
                engine_sound.pitch -= .05f;
                wbox_crash.Play();
                crash_activity = true;
                Invoke("inactive", 1.0f);

                if (destroyer.Length > 1) { Destroy(destroyer[destroyer.Length - 1]); }




            }
        }
        if (collision.gameObject.tag == "barrel")
        {
            if (crash_activity == false)
            {
                tomato_minus();
                engine_sound.pitch -= .05f;
                barrel_crash.Play();
                crash_activity = true;
                Invoke("inactive", 1.0f);

                if (destroyer.Length > 1) { Destroy(destroyer[destroyer.Length - 1]); }




            }
        }

    }

    //UI ekranýndaki sol köþede bulunan domates adedinin eksilmesini saðlar.
    private void tomato_minus() 
    {

        if (tomato_num <= 0)
        {
            tomato_num = 0;

            //UI üzerinde "tomato_number" text içerisine müdahele eder.
            tomato_number.SetText(tomato_num.ToString());
        }
        else
        {
            tomato_num -= 1;
            tomato_number.SetText(tomato_num.ToString());
        }
    }

    
    void inactive() 
    {
        crash_activity = false;
    }
   
  
    //Aracýn hareket etmesi için sanal tekerler.
    void VirtualWheels()
    {
        //Aracýn tüm tekerleklerinin colliderlarýný, mesh pozisyonlarýný ve rotasyonlarýný "GetWorldPose" ile sahnenin delta x,y,z posizyonlarýna göre sabitler.
        frLeftCol.GetWorldPose(out pos, out rot);
        frLeft.transform.position = pos;
        frLeft.transform.rotation = rot;

        frRightCol.GetWorldPose(out pos, out rot);
        frRight.transform.position = pos;
        frRight.transform.rotation = rot;

        bcLeftCol.GetWorldPose(out pos, out rot);
        bcLeft.transform.position = pos;
        bcLeft.transform.rotation = rot;

        bcRightCol.GetWorldPose(out pos, out rot);
        bcRight.transform.position = pos;
        bcRight.transform.rotation = rot;


    }
}
