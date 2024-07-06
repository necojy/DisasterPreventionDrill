using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Border : MonoBehaviour
{
    private ShowDeadCanvas showDeadCanvas;
    private void Start()
    {
        showDeadCanvas = GameObject.Find("player deadth control").GetComponent<ShowDeadCanvas>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            showDeadCanvas.deadReason = "幻想自己會飛";
            StartCoroutine(showDeadCanvas.ShowDeadCanva());
        }
    }

}
