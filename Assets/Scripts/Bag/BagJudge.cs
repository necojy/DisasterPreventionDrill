using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagJudge : MonoBehaviour
{
    public bool isPuting = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bag"))
        {
            Debug.Log("yes");
            isPuting = true;
        }
    }
}
