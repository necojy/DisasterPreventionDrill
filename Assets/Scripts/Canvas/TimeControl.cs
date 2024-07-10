using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeControl : MonoBehaviour
{
    #region 時間條參數
    // [SerializeField] RectTransform uiFill;
    public Slider timeBar;
    public Image fillImage;
    float nowtime, maxtime;
    float oriW, oriH;
    #endregion
    public CameraShake cameraShake;
    public ShowCanvas showCanvas;
    public float diff = 0f;
    public bool timeOut = false;

    // 顏色參數
    public Color startColor = Color.green;
    public Color endColor = Color.red;

    // 粒子系統
    public ParticleSystem sparkParticleSystem;
    public RectTransform timeBarFillArea; // Slider 的填充區域
    private bool sparkStarted = false;

    void Awake()
    {
        nowtime = cameraShake.shakeDuration_max - diff;
        maxtime = cameraShake.shakeDuration_max - diff;
        timeBar.maxValue = maxtime;
        timeBar.value = nowtime;

        fillImage.color = startColor;
    }
    public IEnumerator UpdateTimer()
    {
        timeOut = false;

        while (nowtime > 0)
        {
            nowtime -= Time.deltaTime;
            timeBar.value = nowtime;

            // 根據剩餘時間計算顏色
            float t = nowtime / maxtime;
            fillImage.color = Color.Lerp(endColor, startColor, t);

            // 剩餘時間少於一半時啟動
            if (nowtime <= maxtime / 2 && !sparkStarted)
            {
                sparkStarted = true;
                if (sparkParticleSystem != null)
                {
                    sparkParticleSystem.Play();
                }
            }

            // 更新效果的位置
            if (sparkParticleSystem != null && sparkParticleSystem.isPlaying)
            {
                Vector3[] corners = new Vector3[4];
                timeBarFillArea.GetWorldCorners(corners);

                // 計算位置，設定在填充區域的右側
                Vector3 sparkPosition = corners[2]; // 右上角的位置
                sparkParticleSystem.transform.position = sparkPosition;
            }

            yield return null;
        }

        nowtime = 0;
        timeBar.value = nowtime;
        timeOut = true;

        // 停止火花效果
        if (sparkParticleSystem != null && sparkParticleSystem.isPlaying)
        {
            sparkParticleSystem.Stop();
        }
    }

}
