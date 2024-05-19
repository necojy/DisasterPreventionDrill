using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeControl : MonoBehaviour
{
    [SerializeField] RectTransform uiFill;
    float nowtime, maxtime;
    float oriW, oriH;
    public int Duration;
    private int remaingDuration;
    public ShowCanvas showCanvas;

    public bool timeOut = false;

    public GameObject timer;

    public bool startGame = false;
    void Start()
    {
        nowtime = Duration * 100;
        maxtime = Duration * 100;
        oriW = uiFill.sizeDelta.x;
        oriH = uiFill.sizeDelta.y;
        remaingDuration = Duration;
        // StartCoroutine(UpdateTimer());
    }
    void countHp()
    {
        uiFill.sizeDelta = new UnityEngine.Vector2(oriW / maxtime * nowtime, oriH);
    }

    public IEnumerator UpdateTimer()
    {
        while (nowtime >= 0)
        {
            nowtime -= 1;
            countHp();
            yield return new WaitForSeconds(0.01f);
        }
        
        showCanvas.open_Timer = false;
        timeOut = true;
        
    }

    private void Update() 
    {
        if(showCanvas.open_Timer)
        {
            startGame = true;
            StartCoroutine(UpdateTimer());
        }    
    }
}
