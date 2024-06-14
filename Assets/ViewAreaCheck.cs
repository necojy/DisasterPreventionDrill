using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewAreaCheck : MonoBehaviour
{
    private bool ViewCheckUp = false;
    private bool ViewCheckDown = false;
    private void OnTriggerStay(Collider other) 
    {
        if(other.CompareTag("viewArea_Up"))
        {
            ViewCheckUp = true;
        }
        if(other.CompareTag("viewArea_Down"))
        {
            ViewCheckDown = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("viewArea_Up"))
        {
            ViewCheckUp = false;
        }
        if(other.CompareTag("viewArea_Down"))
        {
            ViewCheckDown = false;
        }
    }

    private void Update() 
    {
        if(ViewCheckUp && ViewCheckDown)    
        {
            Debug.Log("123");
        }
    }
}
