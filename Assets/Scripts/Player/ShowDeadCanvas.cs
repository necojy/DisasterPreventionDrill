using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShowDeadCanvas : MonoBehaviour
{
    private Animator fade_animator;
    private GameObject locomotion;
    private GameObject deadPanel;
    private Text deadReasonText;
    public string deadReason;
    public bool isDead = false;

    private void Start()
    {
        fade_animator = GameObject.Find("Fade Canvas").GetComponent<Animator>();
        locomotion = GameObject.Find("Locomotion System");
        deadPanel = GameObject.Find("DeadPanel");
        deadReasonText = GameObject.Find("deadReason").GetComponent<Text>();

        deadPanel.SetActive(false);
    }

    public IEnumerator ShowDeadCanva()
    {
        isDead = true;

        fade_animator.SetBool("fadein", true);
        locomotion.SetActive(false);

        deadReasonText.text = deadReason;
        yield return new WaitForSeconds(1f);
        deadPanel.SetActive(true);

        StartCoroutine(DeadRecip());
    }

    IEnumerator DeadRecip()
    {
        fade_animator.SetBool("isDead", true);
        yield return new WaitForSeconds(4f);
        Rebirth();
    }

    public void Rebirth()
    {
        deadPanel.SetActive(false);
        fade_animator.SetBool("fadein", false);
        isDead = false;

        SceneManager.LoadScene("Streets");
    }
}
