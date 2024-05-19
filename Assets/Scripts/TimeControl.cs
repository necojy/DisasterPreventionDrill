using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeControl : MonoBehaviour
{
    #region 時間條參數
    [SerializeField] RectTransform uiFill;
    float nowtime, maxtime;
    float oriW, oriH;
    #endregion
    public CameraShake cameraShake;
    public ShowCanvas showCanvas;
    public float diff = 9f;
    public bool timeOut = false;
    void Awake()
    {
        nowtime = (cameraShake.shakeDuration - diff) * 100;
        maxtime = (cameraShake.shakeDuration - diff) * 100;
        oriW = uiFill.sizeDelta.x;
        oriH = uiFill.sizeDelta.y;
    }
    public IEnumerator UpdateTimer()
    {
        timeOut = false;

        while (nowtime >= 0)
        {
            nowtime -= Time.deltaTime * 100;
            uiFill.sizeDelta = new Vector2(oriW / maxtime * nowtime, oriH);
            yield return null;
        }

        timeOut = true;
    }
    // private IEnumerator TT()
    // {
    //     yield return new WaitForSeconds(Duration-12);
    //     showCanvas.open_Timer = false;
    //     timeOut = true;
    // }
    // private void Update()
    // {
    //     if (showCanvas.open_Timer)
    //     {
    //         startGame = true;
    //         StartCoroutine(UpdateTimer());
    //         StartCoroutine(TT());
    //     }
    // }
}
