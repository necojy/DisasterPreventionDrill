using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakObject : MonoBehaviour
{
    //Usage: remove the original prefab, swap it with the broken prefab.
    public GameObject fractured;
    public float breakForce;

    void OnCollisionEnter(Collision collision)
    {
        Break();
    }

    public void Break()
    {
        GameObject frac = Instantiate(fractured, transform.position, transform.rotation);

        int music_index = 1;
        foreach (Rigidbody rb in frac.GetComponentsInChildren<Rigidbody>())
        {
            Vector3 force = (rb.transform.position - transform.position).normalized * breakForce;
            rb.AddForce(force);
            if (music_index == 1) AudioManager.instance.PlayItemSound("Breaking_glass");
            // else if (music_index == -1) AudioManager.instance.PlayItemSound("Breaking_glass_2");
            // music_index *= -1;
        }

        Destroy(gameObject);
    }
}
