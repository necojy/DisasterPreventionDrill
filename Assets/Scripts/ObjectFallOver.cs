using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFallOver : MonoBehaviour
{
    public bool activate;

    public float v = 10;

    public void Update()
    {
        fallOver();
    }

    public void fallOver()
    {
        if(true == activate)
        {   
            GetComponent<Rigidbody>().AddTorque(transform.forward * -v);
            activate = !activate;
        }
    }
}
