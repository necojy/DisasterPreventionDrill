using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Try : MonoBehaviour
{
    public Slider hpBar;
    float nowtime = 10f, maxtime = 10f;

    private void Start() 
    {
        hpBar.maxValue = maxtime;
        hpBar.value = nowtime;

        StartCoroutine(TT());
    }

    IEnumerator TT()
    {
        while (nowtime > 0)
        {
            nowtime -= Time.deltaTime;
            hpBar.value = nowtime;
            yield return null;
        }

        nowtime = 0;
        hpBar.value = nowtime;
    }

}
