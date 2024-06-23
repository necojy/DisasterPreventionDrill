using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeControl : MonoBehaviour
{
    #region 時間條參數
    // [SerializeField] RectTransform uiFill;
    public Slider timeBar;
    float nowtime, maxtime;
    float oriW, oriH;
    #endregion
    public CameraShake cameraShake;
    public ShowCanvas showCanvas;
    public float diff = 0f;
    public bool timeOut = false;
    void Awake()
    {
        nowtime = cameraShake.shakeDuration_max - diff;
        maxtime = cameraShake.shakeDuration_max - diff;
        timeBar.maxValue = maxtime;
        timeBar.value = nowtime;
    }
    public IEnumerator UpdateTimer()
    {
        timeOut = false;

        while (nowtime > 0)
        {
            nowtime -= Time.deltaTime;
            timeBar.value = nowtime;
            yield return null;
        }

        nowtime = 0;
        timeBar.value = nowtime;

        timeOut = true;
    }

}
