using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class batch : MonoBehaviour
{
    RaycastHit hit;
    Vector3 distance;

    public GameObject catcher_clone,tomato;
    public GameObject canvas, canvas2;

    public TextMeshProUGUI tot_score,required;
    
    private GameObject added;
    public GameObject roof_cam;
    private GameObject[] lt;

    private bool isActive = true;
     
    private void FixedUpdate()
    {
        

           
            if (Input.GetMouseButtonDown(0) && roof_cam.GetComponent<Camera>().enabled==true)
            {

                
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                
                Physics.Raycast(ray, out hit, 100.0f);
                if (hit.collider.gameObject.tag == "last_tomato")
                {
                    if (added == null)
                    {
                        //The prefab "catcher_clone" is called so that we can grab and move objects with our finger
                        added = Instantiate(catcher_clone, hit.point, Quaternion.identity);

                        //The SpringJoint bond inside the prefab object connects with the physics component of the object whose beam collision occurred.
                        added.GetComponent<SpringJoint>().connectedBody = hit.collider.gameObject.GetComponent<Rigidbody>();
                        distance = Input.mousePosition - Camera.main.WorldToScreenPoint(added.transform.position);
                    }



                }
            }
            //The catcher_clone" is destroyed if contact is released
            if (Input.GetMouseButtonUp(0))
            {
                Destroy(added);
            }
            //In case of continued contact, the "catcher_clone" and the object with beam collision will follow each other.
            if (Input.GetMouseButton(0))
            {

                if (added) { added.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - distance); }

            }




        
        lt = GameObject.FindGameObjectsWithTag("last_tomato");

        //Endgame if the player doesn't die 
        if (roof_cam.GetComponent<Camera>().enabled == true) 
        {
            
            if (lt.Length < 2 && isActive == true)
            {
                isActive = false;

                //The final_score() function is accessed from within the ScoreManager class, this function prints the endgame score.
                scoreManager.Instance.final_score();

                required.SetText("Unlock Stage: "+PlayerPrefs.GetInt("lvl_rqrd").ToString());
                canvas.SetActive(true);
                canvas2.SetActive(false);
                Time.timeScale = 0;

            }
        }
        


    }

    
    
        
    
}
    // Start is called before the first frame update
    