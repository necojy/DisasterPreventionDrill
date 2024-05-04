using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighter : MonoBehaviour
{
    public GameObject flame; 
    void Start()
    {
        flame.SetActive(false);
    }

   
    public void Ignite()
    {
        flame.SetActive(!flame.active);
    }
}
