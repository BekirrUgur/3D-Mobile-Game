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
    private bool car�smove = true;

    private int q = 0, m = 0, tomato_num = 0;

    private float _brakeTorque;
    private void Awake()
    {
        Application.targetFrameRate = 60;
     
        //Oyun i�indeki private olan nesnelerin atamas� yap�l�yor.
        b_spawn_place = GameObject.FindGameObjectsWithTag("b_spawn");
        point = GameObject.Find("player/Crash_sound/point").GetComponent<AudioSource>();
        

    }
    void Start()
    {
        
        Invoke("eng_s_start", 0.2f);
        //Ara� motorunu bir fonksiyon arac�l��� ile program ba�lad�ktan belirli bir s�re sonra �al��t�r�yoruz.
        InvokeRepeating("eng_speed", .0f * Time.deltaTime, .025f);
       
    }
    
    void eng_speed()  
    {
        //Ara� h�z�n� kontrol ediyor
        if (IsMove && rb.velocity.magnitude*2 < 40.0f)
        {
            //Arac�n hareketini ger�ekle�tirmesi rigidbody yani fizik bile�enine eklenen g�� hesaplan�p ekleniyor.
            rb.AddForce(Vector3.forward * 200.0f * maxEngineHp * Time.deltaTime, ForceMode.Force);
        }
        
    }
    void eng_s_start() 
    {
        
        engine_sound.Play();
    }
   

    void Update()
    {
        //Ara� hareket halindeyken h�z de�erlerini tutar ve motor sesini ara� h�z�na g�re ayarlar.
        if (car�smove==true) 
        {
            // "c_tom_bar" etiketine sahip oyun nesnelerini sahne i�erisinde d�zenli olarak destroyer dizisine atar.
            destroyer = GameObject.FindGameObjectsWithTag("c_tom_bar");

            Debug.Log(destroyer.Length);
            Debug.Log(rb.velocity.magnitude * 2);
            float x = rb.velocity.magnitude * 2;
            engine_sound.pitch = x / 100 * 3;
            audio_s = transform.InverseTransformDirection(rb.velocity).z * audio_s_settings;
        }
        

        
        //Mobil telefon ekran�na temas� kontrol eder.
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            //E�er temas delta x pozisyonunda 15.0 piksellik bir kayma ger�ekle�tirirse 
            if (touch.deltaPosition.x >15.0f)
            {
                
                //30 derecelik bir d�n�� de�eri olu�turulur
                crRotate = 30;
                //�n tekerlerin colliderler�n�n d�n�� a��lar�n� crRotate a��s�na e�itler ve arac�n tekerlerinin d�nmesini sa�lar.
                frRightCol.steerAngle = crRotate;
                frLeftCol.steerAngle = crRotate;
                
           
            }
           
            
            // Temas�n delta x pozisyonunda eksi y�nde yani ha<reket etmesi halinde ayn� i�levi ger�ekletirir ara� tekerleri sola 30 derecelik bi a�� verir.
            if (touch.deltaPosition.x <-15.0f)
            {
                
                crRotate = -30f;
                frRightCol.steerAngle = crRotate;
                frLeftCol.steerAngle = crRotate;
                
               
            }
           

        }
        //Temas olmamas� halinde arac�n stabilitesini korumak i�in teker a��lar�n� s�f�rlar
        if (Input.touchCount == 0)
        {
            crRotate = 0;
            frRightCol.steerAngle = crRotate;
            frLeftCol.steerAngle = crRotate;
        }

        _brakeTorque = brakePower * Mathf.Abs(Input.GetAxis("Jump"));

        //Fren torku belirli bir de�erin alt�nda oldu�u takdirde 
        if (_brakeTorque < 0.05f) 
        {
          //�n tekerlere etki eden motor torklar� belirlenen motor g�c�ne g�re aktar�l�r
            frLeftCol.motorTorque = engine;
            frRightCol.motorTorque = engine;

            //Tekerlere uygulanan d�nd�rme kuvvetinin etkilenmemesi ve bo�a enerji kayb� olmamas� i�in fren torklar� 0 de�erine sabitlenir.
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

        //Arac�n transfom bile�enindekiy de�eri sahnenin delta y 0 posizyonundan daha d���k bir de�ere sahip loursa
        if (transform.position.y < 0.0f) 
        {
           //Kullan�c�n�n kar��s�na kullan�c� aray�z�(UI) ekranlar� gelir
            canvas.SetActive(true);
            engine_sound.Stop();
            canvas_2.SetActive(false);

            button_1.SetActive(false);
            button_2.SetActive(false);
           
            //Oyunun zaman �l�e�i 0 de�erine yani hareketsizli�e e�itlenir
            Time.timeScale = 0;
        }

        if (point_check == true)
        {
            InvokeRepeating("move_barrel", .0f, 2.0f);
            
        }
        

    }

    //OnTriggerEnter fonksiyonu colliderlar aras�nda yumu�ak bir �arp��ma oldu�u s�rada fonksiyon aktif olur
    private void OnTriggerEnter(Collider other)
    {
        
        //Arac�n collider� etiketi nos olan bir oyun nesnesiyle yumu�ak �arp��ma ger�ekle�tirdi�i zaman
        if (other.gameObject.tag == "nos")
        {

            //Toplan�lan �d�l 1 artt�r�l� UI ekran�na yaz�l�r.
            tomato_num += 1;
            tomato_number.SetText(tomato_num.ToString());
            //Oyun i�i AudioSource bile�eni aktif edilir ve 8-bitlik kazan�m sesi �al�n�r.
            point.Play();
            //Temas edilen oyun nesnesi sahneden silinir
            Destroy(other.gameObject);

            //Prefab olarak olu�turulan oyun nesnesi her temas halinde sahne i�erisinde belirlenen transform noktas�na Instantine edilir.
            q = destroyer.Length-1;
            GameObject new_t_barrel = Instantiate(t_barrel, new Vector3(b_spawn_place[q].transform.position.x, b_spawn_place[q].transform.position.y, b_spawn_place[q].transform.position.z), Quaternion.identity);
            //Instantine edilen nesne ile ara� nesnemizin transformlar� birbirine sabitlenir.
            new_t_barrel.transform.parent = gameObject.transform;
                

        }
        if (other.gameObject.tag == "Finish")
        { 
            //Sistemi yormamak i�in daha �nce sistemde �al��t�rm�� oldu�umuz Invokelar iptal edilir
            CancelInvoke();

            if (crash_activity == false)
            {

                engine_sound.Stop();
                car�smove = false;
                
                //crash_activity de�i�keni ayn� collider i�erisindeki ince katman �arp��malar�n� engellemek ve sistemde a��k b�rakmamak i�in her �arp��mada aktif olur.
                crash_activity = true;

                // "inactive" fonksiyonu 1.0sn gecikmeli �a��r�larak �arp��ma bile�enini tekrar aktif eder.
                Invoke("inactive", 1.0f);
                

            }
            
            GameObject[] c_tom = GameObject.FindGameObjectsWithTag("c_tom_bar");

            //Sahne i�erisindeki k�r�lmay� engellemek i�in kullan�c�n 0 �d�l toplama yapmas� halinde alternatif oyun sonuna gidilir.
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

            //Prefab olarak �a��r�lan nesnelerin sahne i�erisinde referans olarak bulunmas� gerekti�i i�in dizi i�erisinde referans nesnesini yok saymak i�in referans nesnesi dizide yok say�l�r
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
            //destroyer dizisi i�erisindeki nesneler "point_2" nesnesini bulundu�u konuma animasyon arac�l��� ile konumu e�itlenir. 
            destroyer[m].transform.LeanMove(GameObject.Find("point_2").transform.position, 4).setEase(LeanTweenType.easeInOutSine);
            destroyer[m].LeanRotate(new Vector3(0, 180, 0), 1.4f).setEase(LeanTweenType.easeInBounce);
           
                m -= 1;
            

        }


    }
  
    //OnCollisonEnter colliderler aras�nda sert �arp��amlar� tetikler.
    private void OnCollisionEnter(Collision collision)
    {

        Debug.Log(collision.gameObject.tag);

//Sahnede bulunan engeller taglar� ile biribirinden ayr�l�r 
        if (collision.gameObject.tag == "tire")
        {

            
            //�arp��ma ikileminii �nlemek i�in "crash_activity" ile ko�ul de�erlendirilir 
            if (crash_activity==false) 
            {
               
                tomato_minus();
                // Motor sesi, �arp��ma seslerini tetikler 
                engine_sound.pitch -= .05f;
                au_crash.Play();
                crash_activity = true;
                Invoke("inactive", 1.0f);

                //destroyer dizisinde eleman var ise �arp��am sonucu toplad��� bir kazan�m� oyun sahnesinden siler.
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

    //UI ekran�ndaki sol k��ede bulunan domates adedinin eksilmesini sa�lar.
    private void tomato_minus() 
    {

        if (tomato_num <= 0)
        {
            tomato_num = 0;

            //UI �zerinde "tomato_number" text i�erisine m�dahele eder.
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
   
  
    //Arac�n hareket etmesi i�in sanal tekerler.
    void VirtualWheels()
    {
        //Arac�n t�m tekerleklerinin colliderlar�n�, mesh pozisyonlar�n� ve rotasyonlar�n� "GetWorldPose" ile sahnenin delta x,y,z posizyonlar�na g�re sabitler.
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
