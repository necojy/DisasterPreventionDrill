using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellphoneOrLighteerHint : MonoBehaviour
{
    public bool isHint = false;

    private GameObject item1Hint;
    private Animator item1Animator;

    private void Start()
    {
        item1Hint = GameObject.Find("item_1_Hint");
        item1Hint.SetActive(false);
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
        item1Hint.SetActive(true);
        item1Animator = item1Hint.GetComponent<Animator>();

        item1Animator.SetBool("isHint", true);

        yield return new WaitForSeconds(4f);
        item1Hint.SetActive(false);
    }
}
