using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TTY : MonoBehaviour
{
    public Animator fade_animator;
    public GameObject deadPanel;
    public Text deadReasonText;

    public string deadReason;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            StartCoroutine(ShowDeadCanvas());
        }
    }
    public IEnumerator ShowDeadCanvas()
    {
        fade_animator.SetBool("fadein", true);

        deadReasonText.text = deadReason;
        yield return new WaitForSeconds(2f);
        deadPanel.SetActive(true);

        // StartCoroutine(DeadRecip());
    }
}
