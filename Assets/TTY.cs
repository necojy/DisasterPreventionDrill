using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TTY : MonoBehaviour
{
    // private Animator fade_animator;
    // private GameObject locomotion;
    // private GameObject deadPanel;
    // private Text deadReasonText;
    // public string deadReason;
    // public bool isDead = false;


    // private void Start()
    // {
    //     fade_animator = GameObject.Find("Fade Canvas").GetComponent<Animator>();
    //     locomotion = GameObject.Find("Locomotion System");
    //     deadPanel = GameObject.Find("DeadPanel");
    //     deadReasonText = GameObject.Find("deadReason").GetComponent<Text>();

    //     deadPanel.SetActive(false);
    // }
    // private void OnTriggerEnter(Collider collider)
    // {
    //     if (collider.CompareTag("Player"))
    //     {
    //         StartCoroutine(ShowDeadCanvas());
    //     }
    // }
    // public IEnumerator ShowDeadCanvas()
    // {
    //     Debug.Log(deadReason);

    //     fade_animator.SetBool("fadein", true);

    //     deadReasonText.text = deadReason;
    //     yield return new WaitForSeconds(2f);
    //     deadPanel.SetActive(true);

    //     // StartCoroutine(DeadRecip());
    // }
}
