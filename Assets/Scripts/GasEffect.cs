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

    public void ShowEffect()
    {
        gas.SetActive(true);
    }
    public void CloseEffect()
    {
        gas.SetActive(false);
    }
}
