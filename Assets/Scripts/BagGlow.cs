using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagGlow : MonoBehaviour
{
    public GameObject[] hint_bag;
    public int hint_start, hint_end;
    public GameObject glowbox;

    private GameObject itemColumnHint;
    private Animator itemColumnHintAnimator;
    public bool isHint = false;


    private void Start()
    {
        itemColumnHint = GameObject.Find("itemColumnHint");
        itemColumnHint.SetActive(false);
    }

    public void CheckCanOpen()
    {
        if (isHint == false)
        {
            isHint = true;
            StartCoroutine(OpenHint());
        }
    }

    private IEnumerator OpenHint()
    {
        itemColumnHint.SetActive(true);
        itemColumnHintAnimator = itemColumnHint.GetComponent<Animator>();

        itemColumnHintAnimator.SetBool("isHint", true);

        yield return new WaitForSeconds(3.5f);
        itemColumnHint.SetActive(false);
        glowbox.SetActive(false);
    }


    public void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            StartCoroutine(StartHint());
        }
    }

    public IEnumerator StartHint()
    {
        Hint_Glow(true, hint_start, hint_end);
        yield return new WaitForSeconds(3f);
        Hint_Glow(false, hint_start, hint_end);
        CheckCanOpen();
    }

    private void Hint_Glow(bool is_open, int start, int end)
    {
        for (int i = start; i <= end; i++)
        {
            if (hint_bag[i] != null)
            {
                Skode_Glinting skode_Glinting = hint_bag[i].GetComponent<Skode_Glinting>();
                if (skode_Glinting != null)
                {
                    if (is_open) skode_Glinting.StartGlinting();

                    else skode_Glinting.StopGlinting();
                }
            }
        }
    }

}
