using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControl : MonoBehaviour
{
    public ElevatorControl elevatorControl;
    void Start()
    {
        elevatorControl = FindObjectOfType<ElevatorControl>();
    }

    
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider coll)
    {
        if (coll.CompareTag("Right Hand") || coll.CompareTag("Left Hand"))
        {
            elevatorControl.Close();
        }
    }
}
