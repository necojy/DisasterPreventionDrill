using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GripControl : MonoBehaviour
{
    bool putLeftHand = false;
    bool putRightHand = false;
    public GameObject Grip;

    private void Start()
    {
        Grip.SetActive(false);
    }
    private void OnTriggerStay(Collider coll)
    {
        if (coll.tag == "Left Hand")
        {
            putLeftHand = true;
        }
        if (coll.tag == "Right Hand")
        {
            putRightHand = true;
        }
    }

    private void OnTriggerExit(Collider coll)
    {
        if (coll.tag == "Left Hand")
        {
            putLeftHand = false;
        }
        if (coll.tag == "Right Hand")
        {
            putRightHand = false;
        }
    }

    private void Update()
    {
        if (putLeftHand && putRightHand)
        {
            Grip.SetActive(true);
        }
    }

}
