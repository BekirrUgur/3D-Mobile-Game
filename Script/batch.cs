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

                // "ray" deðiþkeni ile kullanýcýnýn gördüðü ekran üzerinde dokunulan nokta üzerinde bir sanal ýþýn oluþturur.
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                // Sanal ýþýk fizik bileþenleri verilerek, ýþýk gücü z posizyonunda 100.0 birim oluþturulur.
                Physics.Raycast(ray, out hit, 100.0f);
                if (hit.collider.gameObject.tag == "last_tomato")
                {
                    if (added == null)
                    {
                        //Nesneleri parmaðýmýzla tutup hareket ettirebilmemiz için prefab olan "catcher_clone" çaðýrýlýr
                        added = Instantiate(catcher_clone, hit.point, Quaternion.identity);
                        
                        //prefab nesnenþn içinde bulunan Springjoint baðý ýþýn çarpýþmasý gerçekleþen nesnenin fizik bileþeni ile baðlanýr.
                        added.GetComponent<SpringJoint>().connectedBody = hit.collider.gameObject.GetComponent<Rigidbody>();
                        distance = Input.mousePosition - Camera.main.WorldToScreenPoint(added.transform.position);
                    }



                }
            }
            //Temas býrakýlmasý halinde catcher_clone" yok edilir
            if (Input.GetMouseButtonUp(0))
            {
                Destroy(added);
            }
            //Temas sürmei halinde "catcher_clone" ve ýþýn çarpýþmasý gerçekleþen nesnenin biribirini takip etmesi saðlanýr.
            if (Input.GetMouseButton(0))
            {

                if (added) { added.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - distance); }

            }




        
        lt = GameObject.FindGameObjectsWithTag("last_tomato");

        // Kullanýcý yanmadýðý takdirde gerçekleþen oyun sonu 
        if (roof_cam.GetComponent<Camera>().enabled == true) 
        {
            
            if (lt.Length < 2 && isActive == true)
            {
                isActive = false;

                //ScoreManager class ý içerisinden final_score() fonksiyonuna ulaþýlýr bu fonksiyon oyun sonu skorunu yazdýrýr.
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
    