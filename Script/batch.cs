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

                // "ray" de�i�keni ile kullan�c�n�n g�rd��� ekran �zerinde dokunulan nokta �zerinde bir sanal ���n olu�turur.
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                // Sanal ���k fizik bile�enleri verilerek, ���k g�c� z posizyonunda 100.0 birim olu�turulur.
                Physics.Raycast(ray, out hit, 100.0f);
                if (hit.collider.gameObject.tag == "last_tomato")
                {
                    if (added == null)
                    {
                        //Nesneleri parma��m�zla tutup hareket ettirebilmemiz i�in prefab olan "catcher_clone" �a��r�l�r
                        added = Instantiate(catcher_clone, hit.point, Quaternion.identity);
                        
                        //prefab nesnen�n i�inde bulunan Springjoint ba�� ���n �arp��mas� ger�ekle�en nesnenin fizik bile�eni ile ba�lan�r.
                        added.GetComponent<SpringJoint>().connectedBody = hit.collider.gameObject.GetComponent<Rigidbody>();
                        distance = Input.mousePosition - Camera.main.WorldToScreenPoint(added.transform.position);
                    }



                }
            }
            //Temas b�rak�lmas� halinde catcher_clone" yok edilir
            if (Input.GetMouseButtonUp(0))
            {
                Destroy(added);
            }
            //Temas s�rmei halinde "catcher_clone" ve ���n �arp��mas� ger�ekle�en nesnenin biribirini takip etmesi sa�lan�r.
            if (Input.GetMouseButton(0))
            {

                if (added) { added.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - distance); }

            }




        
        lt = GameObject.FindGameObjectsWithTag("last_tomato");

        // Kullan�c� yanmad��� takdirde ger�ekle�en oyun sonu 
        if (roof_cam.GetComponent<Camera>().enabled == true) 
        {
            
            if (lt.Length < 2 && isActive == true)
            {
                isActive = false;

                //ScoreManager class � i�erisinden final_score() fonksiyonuna ula��l�r bu fonksiyon oyun sonu skorunu yazd�r�r.
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
    