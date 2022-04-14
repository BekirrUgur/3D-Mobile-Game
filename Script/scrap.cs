using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrap : MonoBehaviour
{

    public AudioSource au_crash;
    public AudioSource eng_sound;

    public GameObject button_1;
    public GameObject button_2;
    public GameObject stg_completed;
    public GameObject canvas;
    public GameObject canvas_2;
    public GameObject second_cam;

    bool crash_activity;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ground") 
        {
            if (crash_activity == false)
            {
                eng_sound.Stop();
                Debug.Log("TOUCH THE GORUND");
                au_crash.Play();
                
                canvas_2.SetActive(false);
                canvas.SetActive(true);
                button_1.SetActive(false);
                button_2.SetActive(false);
               


                crash_activity = true;
                Invoke("inactive", 1.7f);

            }
        }
    }
    void inactive()
    {

        crash_activity = false;
        Time.timeScale=0;

    }
}
