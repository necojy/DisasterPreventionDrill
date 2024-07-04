using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagLeftTop : MonoBehaviour
{
    public bool isPuting = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPuting = true;
        }
    }
}
