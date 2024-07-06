using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagLeftTop : MonoBehaviour
{
    public bool isPuting = false;
    private GameObject mainCamera;
    private Renderer objectRenderer; // 渲染器


    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        objectRenderer = GetComponent<Renderer>();
        objectRenderer.material.color = Color.red;
    }

    private void Update()
    {
        transform.position = new Vector3(mainCamera.transform.position.x + 0.2f, mainCamera.transform.position.y + 1f, mainCamera.transform.position.z + 0.3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bag"))
        {
            isPuting = true;
            objectRenderer.material.color = Color.green;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bag"))
        {
            isPuting = false;
            objectRenderer.material.color = Color.red;
        }
    }
}
