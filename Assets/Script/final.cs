using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class final : MonoBehaviour
{

    public GameObject fin_cam;
    public GameObject sec_cam;
    public GameObject health_bar;



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Player") 
        {
            
            fin_cam.SetActive(true);
            sec_cam.SetActive(false);

            //As soon as the player touches the collider of the last object, the animation is triggered that makes the vehicle reach the specified point.
            GetComponent<BoxCollider>().enabled = false;
            GameObject.Find("player").transform.LeanMove(GameObject.Find("Point").transform.position, 4).setEase(LeanTweenType.easeInOutSine);
            GameObject.Find("player").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            GameObject.Find("player").GetComponent<carControl>().IsMove = false;
            GameObject.Find("player").LeanRotate(new Vector3(0, 180, 0), 1.4f).setEase(LeanTweenType.easeInBounce);
        }
        
    }
}