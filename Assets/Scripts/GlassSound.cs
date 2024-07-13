using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassSound : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            int randomInt = Random.Range(0, 2); // 使用整數範圍
            Debug.Log(randomInt);

            if (randomInt == 0)
                AudioManager.instance.PlayItemSound("glass01");
            else
                AudioManager.instance.PlayItemSound("glass02");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int randomInt = Random.Range(0, 2); // 使用整數範圍
            Debug.Log(randomInt);

            if (randomInt == 0)
                AudioManager.instance.PlayItemSound("glass01");
            else
                AudioManager.instance.PlayItemSound("glass02");
        }
    }

}
