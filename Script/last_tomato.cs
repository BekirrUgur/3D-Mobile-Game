using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class last_tomato : MonoBehaviour
{
    private GameObject[] tm;

    private AudioSource squish;

    private MeshRenderer rc1,rc2,rc3,rc4;

    private BoxCollider rc11, rc22, rc33, rc44;

    private TextMeshProUGUI tomato;

    // Start is called before the first frame update
    void Start()
    {
        squish = GameObject.Find("player/Crash_sound/tomato_squish").GetComponent<AudioSource>();
        tomato = GameObject.Find("Canvas_2/tomato_num").GetComponent<TextMeshProUGUI>();
        
        //"last_tomato" prefabýnýn MeshRenderer bileþeni nesne oluþtuðu anda eklenir.
        rc1 = GameObject.FindGameObjectWithTag("rc1").GetComponent<MeshRenderer>();
        rc2 = GameObject.FindGameObjectWithTag("rc2").GetComponent<MeshRenderer>();
        rc3 = GameObject.FindGameObjectWithTag("rc3").GetComponent<MeshRenderer>();
        rc4 = GameObject.FindGameObjectWithTag("rc4").GetComponent<MeshRenderer>();

        //"last_tomato" prefabýnýn BoxCollider bileþeni nesne oluþtuðu anda eklenir.
        rc11 = GameObject.FindGameObjectWithTag("rc1").GetComponent<BoxCollider>();
        rc22 = GameObject.FindGameObjectWithTag("rc2").GetComponent<BoxCollider>();
        rc33= GameObject.FindGameObjectWithTag("rc3").GetComponent<BoxCollider>();
        rc44 = GameObject.FindGameObjectWithTag("rc4").GetComponent<BoxCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {

//Nesne if döngüleri içerisinde isimleri belirtilen nesnelerle sert çarpýþma gerçekleþtirdiði anda sahneden silinir.
        if (collision.gameObject.name== "sptp_building_02") 
        {
           
            squish.Play();
            tomato_pcs();
            Destroy(this.gameObject);
        }
        if (collision.gameObject.name == "jar_gr")
        {
            
            squish.Play();
            tomato_pcs();

            rc4.enabled = true;
            rc44.enabled = true;

            //ScoreManager class ý içerisindeki add_score(int) fonksiyonuna belirlenen deðer ile gönderilir.
            scoreManager.Instance.add_score(10);

            Destroy(this.gameObject);
            
        }
        if (collision.gameObject.name == "red_cyc_4")
        {
            
            squish.Play();
            tomato_pcs();
            rc4.enabled = false;
            rc44.enabled = false;

            rc3.enabled = true;
            rc33.enabled = true;

            scoreManager.Instance.add_score(10);

            Destroy(this.gameObject);
        }
        if (collision.gameObject.name == "red_cyc_3")
        {
            
            squish.Play();
            tomato_pcs();
            rc3.enabled = false;
            rc33.enabled = false;

            rc2.enabled = true;
            rc22.enabled = true;

            scoreManager.Instance.add_score(10);

            Destroy(this.gameObject);
        }
        if (collision.gameObject.name == "red_cyc_2")
        {
          
            squish.Play();
            tomato_pcs();
            rc2.enabled = false;
            rc22.enabled = false;

            rc1.enabled = true;
            rc11.enabled = true;

            scoreManager.Instance.add_score(10);

            Destroy(this.gameObject);
        }
        if (collision.gameObject.name == "red_cyc_1")
        {
           
            squish.Play();
            tomato_pcs();
            scoreManager.Instance.add_score(10);
            Destroy(this.gameObject);
        }

    }
    //Her atýþtan sonra sahnede kalan "last_tomato" nesnesini kontrol eder.
    private void tomato_pcs() 
    {
        
        tm = GameObject.FindGameObjectsWithTag("last_tomato");
        int t = tm.Length-2;
        //UI ekranýna yazdýrýlýr.
        tomato.SetText(t.ToString());
    
    }
    
 
}
