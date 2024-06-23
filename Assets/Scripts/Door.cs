using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator doorAnimator;

    private void Start()
    {
        doorAnimator = GameObject.Find("Door_canOpen").GetComponent<Animator>();

    }

    public void OpenDoor()
    {
        doorAnimator.SetBool("isOpening", true);
    }
    // private void OnTriggerStay(Collider coll)
    // {
    //     if (coll.tag == "Left Hand" || coll.tag == "Right Hand")
    //     {
    //         doorAnimator.SetBool("isOpening", true);

    //     }
    // }
}
