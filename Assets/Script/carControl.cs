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
    private bool car›smove = true;

    private int q = 0, m = 0, tomato_num = 0;

    private float _brakeTorque;
    private void Awake()
    {
        Application.targetFrameRate = 60;
     
        
        b_spawn_place = GameObject.FindGameObjectsWithTag("b_spawn");
        point = GameObject.Find("player/Crash_sound/point").GetComponent<AudioSource>();
        

    }
    void Start()
    {
        
        Invoke("eng_s_start", 0.2f);
        //We start the vehicle engine through a function after a certain period of time after the program starts.
        InvokeRepeating("eng_speed", .0f * Time.deltaTime, .025f);
       
    }
    
    void eng_speed()  
    {
        //Controlling vehicle speed
        if (IsMove && rb.velocity.magnitude*2 < 40.0f)
        {
            //The power added to the rigidbody, that is, the physics component, is calculated and added to the movement of the vehicle.
            rb.AddForce(Vector3.forward * 200.0f * maxEngineHp * Time.deltaTime, ForceMode.Force);
        }
        
    }
    void eng_s_start() 
    {
        
        engine_sound.Play();
    }
   

    void Update()
    {
        //While the vehicle is in motion, it keeps the speed values and adjusts the engine sound according to the vehicle speed.
        if (car›smove==true) 
        {
            //Regularly assigns game objects labeled "c_tom_bar" to the destroyer array within the scene.
            destroyer = GameObject.FindGameObjectsWithTag("c_tom_bar");

            Debug.Log(destroyer.Length);
            Debug.Log(rb.velocity.magnitude * 2);
            float x = rb.velocity.magnitude * 2;
            engine_sound.pitch = x / 100 * 3;
            audio_s = transform.InverseTransformDirection(rb.velocity).z * audio_s_settings;
        }



        //Controls contact with mobile phone screen.
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            
            if (touch.deltaPosition.x >15.0f)
            {
                
                
                crRotate = 30;
                //It equalizes the rotation angles of the colliders of the front wheels to the angle of crRotate and enables the wheels of the vehicle to rotate.
                frRightCol.steerAngle = crRotate;
                frLeftCol.steerAngle = crRotate;
                
           
            }
           
            if (touch.deltaPosition.x <-15.0f)
            {
                
                crRotate = -30f;
                frRightCol.steerAngle = crRotate;
                frLeftCol.steerAngle = crRotate;
                
               
            }
           

        }
        //Resets wheel angles to maintain vehicle stability in the absence of contact
        if (Input.touchCount == 0)
        {
            crRotate = 0;
            frRightCol.steerAngle = crRotate;
            frLeftCol.steerAngle = crRotate;
        }

        _brakeTorque = brakePower * Mathf.Abs(Input.GetAxis("Jump"));

        if (_brakeTorque < 0.05f) 
        {
            //Engine torques acting on the front wheels are transferred according to the determined engine power.
            frLeftCol.motorTorque = engine;
            frRightCol.motorTorque = engine;

            //Brake torques are fixed to 0 so that the turning force applied to the wheels is not affected and there is no wasted energy loss.
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

        //Occurs if the y value in the tool's transform component is lower than the delta y 0 position of the scene.
        if (transform.position.y < 0.0f) 
        {
           
            canvas.SetActive(true);
            engine_sound.Stop();
            canvas_2.SetActive(false);

            button_1.SetActive(false);
            button_2.SetActive(false);

            //The game's time scale is set to 0, i.e. inactivity
            Time.timeScale = 0;
        }

        if (point_check == true)
        {
            InvokeRepeating("move_barrel", .0f, 2.0f);
            
        }
        

    }

    //The part where the vehicle interacts with collectible objects
    private void OnTriggerEnter(Collider other)
    {
        
        
        if (other.gameObject.tag == "nos")
        {

            //The collected reward is written on the UI screen with 1 increment.
            tomato_num += 1;
            tomato_number.SetText(tomato_num.ToString());
            //The in-game AudioSource component is activated and 8-bit gain audio is played.
            point.Play();
            //The touched game object is deleted from the scene
            Destroy(other.gameObject);

            //The game object created as a prefab is instantinated to the specified transform point in the scene at each contact.
            q = destroyer.Length-1;
            GameObject new_t_barrel = Instantiate(t_barrel, new Vector3(b_spawn_place[q].transform.position.x, b_spawn_place[q].transform.position.y, b_spawn_place[q].transform.position.z), Quaternion.identity);
            //The transforms of the instantine object and our vehicle object are fixed to each other.
            new_t_barrel.transform.parent = gameObject.transform;
                

        }
        if (other.gameObject.tag == "Finish")
        {
            //Invokes that we have run on the system before are canceled in order not to tire the system.
            CancelInvoke();

            if (crash_activity == false)
            {

                engine_sound.Stop();
                car›smove = false;

                //The crash_activity variable is active in every collision to prevent thin layer collisions within the same collider and not leave it open in the system.
                crash_activity = true;

                //The "inactive" function is called with a delay of 1.0s and reactivates the collision component.
                Invoke("inactive", 1.0f);
                

            }
            
            GameObject[] c_tom = GameObject.FindGameObjectsWithTag("c_tom_bar");

            //In order to prevent the break in the stage, if the user collects 0 prizes, the alternative game ends.
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

            //The reference object is ignored in the array to ignore the reference object in the array, as objects called prefabs must be referenced in the scene
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
            //The objects in the destroyer array are synchronized to the location of the "point_2" object through animation. 
            destroyer[m].transform.LeanMove(GameObject.Find("point_2").transform.position, 4).setEase(LeanTweenType.easeInOutSine);
            destroyer[m].LeanRotate(new Vector3(0, 180, 0), 1.4f).setEase(LeanTweenType.easeInBounce);
           
                m -= 1;
            

        }


    }

    //Collisions with hard physical contact with the vehicle.
    private void OnCollisionEnter(Collision collision)
    {

        Debug.Log(collision.gameObject.tag);

        
        if (collision.gameObject.tag == "tire")
        {


            //Condition is evaluated with "crash activity" to avoid collision dilemma.
            if (crash_activity==false) 
            {
               
                tomato_minus();
                
                engine_sound.pitch -= .05f;
                au_crash.Play();
                crash_activity = true;
                Invoke("inactive", 1.0f);

                //If there is an element in the destroyer array, it deletes an achievement(tomato) from the game scene as a result of the collision.
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

    //It reduces the number of tomatoes in the left corner of the UI screen.
    private void tomato_minus() 
    {

        if (tomato_num <= 0)
        {
            tomato_num = 0;
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


    //Virtual wheels for the vehicle to move.
    void VirtualWheels()
    {
        //It fixes the colliders, mesh positions and rotations of all wheels of the vehicle with "GetWorldPose" according to the delta x,y,z positions of the scene.
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
