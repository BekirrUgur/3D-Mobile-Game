using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t_stop : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Player") 
        {
            Time.timeScale = 0;
        }
        
    }
}
