using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasEffect : MonoBehaviour
{
    public GameObject gas;
    void Start()
    {
        gas = GameObject.Find("gas effect");
        gas.SetActive(false);
    }

    void Update()
    {
        
    }

    public void showEffect()
    {
        gas.SetActive(true);
    }
    public void closeEffect()
    {
        gas.SetActive(false);
    }
}