using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCaption : MonoBehaviour
{
    public GameObject Caption;

    public GameObject Rebirth_recip;
    public float starttime;
    public int duration = 3;
    private Text captionTextComponent;
    private Text rebirth_recipComponent;

    void Awake()
    {
        captionTextComponent = Caption.GetComponent<Text>();
        rebirth_recipComponent = Rebirth_recip.GetComponent<Text>();
    }
    void Start()
    {
        // StartCoroutine(ShowWords());
    }
    public IEnumerator ShowWords()
    {
        // yield return new WaitForSeconds(starttime);
        OpenCaption();
        int duration_temp = duration;
        while (duration_temp >= 0)
        {
            duration_temp -= 1;
            if (duration_temp < 0)
            {
                CloseCaption();
            }
            yield return new WaitForSeconds(1f);
        }

    }
    public void OpenCaption()
    {
        Caption.SetActive(true);
    }
    public void CloseCaption()
    {
        Caption.SetActive(false);
    }

    public void ChangeCaptionContent(string changeContent)
    {
        captionTextComponent.text = changeContent;
    }

    public void ChangeRebirthContent(string changeContent)
    {
        rebirth_recipComponent.text = changeContent;
    }
}
