using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagLeftTop : MonoBehaviour
{
    public bool isPuting = false;
    private GameObject mainCamera;

    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void Update()
    {
        transform.position = new Vector3(mainCamera.transform.position.x + 0.2f, mainCamera.transform.position.y + 1f, mainCamera.transform.position.z + 0.3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bag"))
        {
            Debug.Log("yes");

            isPuting = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bag"))
        {
            isPuting = false;
        }
    }
}
