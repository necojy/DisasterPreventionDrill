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

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Player"))
        {
            Open();
        }
    }
    private void OnTriggerExit(Collider coll)
    {
        if (coll.CompareTag("Player"))
        {

            //Close();
        }
    }

    public void Open()
    {
        if (anim.GetBool("isOpening") == false)
        {
            anim.SetBool("isClosing", false);
            anim.SetBool("isOpening", true);
            AudioManager.instance.PlayElevatorSound("Open_Close_Door");
        }
    }

    public void Close()
    {
        if (anim.GetBool("isClosing") == false)
        {
            anim.SetBool("isOpening", false);
            anim.SetBool("isClosing", true);
            AudioManager.instance.PlayElevatorSound("Open_Close_Door");
        }
    }
}
