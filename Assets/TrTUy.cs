using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrTUy : MonoBehaviour
{
    public GameObject truck;
    private TTY tty;
    public bool Control = false;
    private void Start()
    {
        tty = truck.GetComponent<TTY>();

    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Player"))
        {
            Control = true;
            StartCoroutine(tty.UU());
        }
    }
}
