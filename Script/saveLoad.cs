 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class saveLoad : MonoBehaviour
{
    public GameObject lock_btn;
    public GameObject actv_btn;
    public GameObject balance;

    public TextMeshProUGUI tot_score;
    public TextMeshProUGUI balance_text;



    public static saveLoad saver;

    public int level = 1;
    public static int level_required = 10;

    public int lvl_rqr;

    // Start is called before the first frame update
    void Start()
    {
        //IndexedDB ile tutulan  "lvl_rqrd" ve "levelNumber" verileri kontrol edilir ve data resetlemelerinden kaynkal�
        //0.seviye ve 0 puan sorunlar�n� ilk atama yaparak d�ng� k�r�lmas�n� engelleriz.

        if (PlayerPrefs.GetInt("lvl_rqrd") < 10) 
        {
            PlayerPrefs.SetInt("lvl_rqrd", 10);
        }
        if (PlayerPrefs.GetInt("levelNumber") < 1)
        {
            PlayerPrefs.SetInt("levelNumber", 1);
        }

    }

    // Update is called once per frame
    void Update()
    {

        lvl_rqr = PlayerPrefs.GetInt("lvl_rqrd");
        
    }
    public void level_up() 
    {
        level = PlayerPrefs.GetInt("levelNumber")+1;
        // Playerprefs.SetInt ile IndexedDB ile tutulan "levelNumber" verisi yoksa o isimde bir veri olu�turur ve level de�i�kenini i�ine atar
        // e�er "levelNumber" verisi varsa �zerine level de�i�kenini yazar.
        PlayerPrefs.SetInt("levelNumber", level);
        level_required = PlayerPrefs.GetInt("lvl_rqrd") + 100;
        PlayerPrefs.SetInt("lvl_rqrd",level_required);
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    //Rstart butonuna bas�ld��� anda aktif olur.
    public void res_scene()
    {
        //"SampleScene" sahnesine tekil ge�i� moduyla ge�er.
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    //S�radaki seviyeyi aktif etme butonu
    public void active_lvl() 
    {
        // Kasada biriktirilen skorun yeni seviyeyi a�mak i�in gereken skordan y�ksek veya e�it olmas� hainde devreye girer
        if (PlayerPrefs.GetInt("score_account") >= lvl_rqr)
        {
            Debug.Log("Score account: "+PlayerPrefs.GetInt("score_account")+" Level Required: "+level_required);
            actv_btn.SetActive(true);
            lock_btn.SetActive(false);
            int x = PlayerPrefs.GetInt("score_account") - PlayerPrefs.GetInt("lvl_rqrd");
            PlayerPrefs.SetInt("score_account", x);
            tot_score.SetText(PlayerPrefs.GetInt("score_account").ToString());
            
        }
        else 
        {
            int y = PlayerPrefs.GetInt("lvl_rqrd")- PlayerPrefs.GetInt("score_account");
            balance.SetActive(true);
            //Kasada biriken skorun yeterli olmamas� halinde verilen hata bilgisi
            balance_text.SetText("You need " +y.ToString()+ " points to unlock the next stage");
           
            
        }
    }
}
