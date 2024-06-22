using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorControl : MonoBehaviour
{
    public GameObject elevator;
    public Animator anim;
    
    void Start()
    {
        elevator = GameObject.Find("Elevator");
        anim = elevator.GetComponent<Animator>();
    }

    private void OnTriggerStay(Collider coll)
    {
        if (coll.CompareTag("Player")) 
        {   
            Debug.Log("Enter");
            Open();
        }
    }
    private void OnTriggerExit(Collider coll)
    {
        if (coll.CompareTag("Player"))
        {   
            Debug.Log("Leave");
            Close();
        }
    }

    public void Open(){
        anim.SetBool("isClosing", false);
        anim.SetBool("isOpening", true);
    }

    public void Close(){
        anim.SetBool("isOpening", false);
        anim.SetBool("isClosing", true);
    }
}
