using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakObject : MonoBehaviour
{
    //Usage: remove the original prefab, swap it with the broken prefab.
    public GameObject fractured;
    public float breakForce;
    public string musicName = "Breaking_glass";

    void OnCollisionEnter(Collision collision)
    {
        Break();
    }

    public void Break()
    {
        GameObject frac = Instantiate(fractured, transform.position, transform.rotation);

        foreach (Rigidbody rb in frac.GetComponentsInChildren<Rigidbody>())
        {
            Vector3 force = (rb.transform.position - transform.position).normalized * breakForce;
            rb.AddForce(force);
            AudioManager.instance.PlayItemSound(musicName);
        }

        Destroy(gameObject);
    }
}
