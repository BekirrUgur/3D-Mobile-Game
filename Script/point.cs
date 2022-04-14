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

   
    // Update is called once per frame
    void Update()
    {
        if (set) { barrels = GameObject.FindGameObjectsWithTag("c_tom_bar"); }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Animasyonlarla harekket ettirid�imiz varillerin �arp�p �arpmad���n� kontrol eder.
        if (other.gameObject.tag == "c_tom_bar") 
        {
            point_audio.Play();
            set = false;
            //�arpan nesne yok edilir.
            Destroy(other.gameObject);
            //oyunun devam�ndaki sahne i�in kullan�lacak olan "tomat" nesnesi sahneye instant edilir.
            GameObject new_tomato = Instantiate(tomat, tomat_spawn.transform.position, Quaternion.identity);
            x += 1;

             //T�m �arp��ma bitti�i takdirde �at� kameras�na ge�ilir ve di�er sahne aktif olur.
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
