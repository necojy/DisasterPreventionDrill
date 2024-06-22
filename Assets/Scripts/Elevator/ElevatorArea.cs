using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorArea : MonoBehaviour
{
    
    public bool inElevator = false;
    private void OnTriggerStay(Collider coll)
    {
        if (coll.CompareTag("Player")) 
        {   
            inElevator = true;
        }
    }
    private void OnTriggerExit(Collider coll)
    {
        if (coll.CompareTag("Player")) 
        {   
            inElevator = false;
        }
    }
    

 
}
