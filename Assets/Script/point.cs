using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class point : MonoBehaviour
{ 
    public GameObject fin_cam;
    public GameObject sec_cam;
    public GameObject roof_cam;
    public GameObject tomat;
    public GameObject tomat_spawn;

    public AudioSource point_audio;

    private GameObject[] barrels;

    int x = 0;

    bool set = true;

   
    void Update()
    {
        if (set) { barrels = GameObject.FindGameObjectsWithTag("c_tom_bar"); }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //It checks whether the barrels we move with animations are hit or not.
        if (other.gameObject.tag == "c_tom_bar") 
        {
            point_audio.Play();
            set = false;
            Destroy(other.gameObject);
            //The "tomat" object that will be used for the next scene of the game is instantiated to the scene.
            GameObject new_tomato = Instantiate(tomat, tomat_spawn.transform.position, Quaternion.identity);
            x += 1;

            //If all the collision is over, the roof camera will be switched to and the other scene will be active.
            if (barrels.Length-1==x) 
            {
                fin_cam.SetActive(false);
                sec_cam.SetActive(false);
                roof_cam.SetActive(true);
                roof_cam.GetComponent<Camera>().enabled = true;
            }
        }
    }
}
