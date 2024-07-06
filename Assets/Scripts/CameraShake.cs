using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    //地震開始時間
    public float startTime = 3f;

    #region 搖晃參數   
    public float shakeDuration_small = 3f;
    public float shakeDuration_max = 10f;
    public float shakeMagnitude = 0.25f;
    public bool isShaking = false;
    #endregion

    #region 搖晃後的掉落物
    public float waitForDown = 1f;
    public GameObject[] fallObjects;
    private bool isFalling = false;
    #endregion

    #region 顯示提示畫布
    private ShowCanvas showCanvas;
    private Camera option1Camera; //故事 1 提示
    #endregion

    public GameObject[] shakingItems;

    public Animator livingroomShakeAni;
    private void Start()
    {
        showCanvas = GameObject.Find("Camera Offset").GetComponent<ShowCanvas>();
        option1Camera = GameObject.Find("option1Camera").GetComponent<Camera>();
        option1Camera.enabled = false; // 選項 1 相機關閉

        Init();
    }

    //初始化
    private void Init()
    {
        // 控制物體先不掉落
        foreach (GameObject fallObject in fallObjects)
        {
            fallObject.GetComponent<Rigidbody>().useGravity = false;
        }

        StartCoroutine(Shake());
    }

    public IEnumerator Shake()
    {
        AudioManager.instance.PlayItemSound("alert_Sound");
        yield return new WaitForSeconds(startTime);

        //開始搖晃
        isShaking = true;

        Vector3 originalPosition = transform.localPosition;
        float elapsed = 0.0f;

        AudioManager.instance.PlayBackground("Earthquake");
        StartCoroutine(ItemShaking());


        livingroomShakeAni.SetBool("isShaking", true);

        while (elapsed < shakeDuration_small)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }


        //暫停音樂
        AudioManager.instance.PauseSound("BackgroundSource");

        //給於提示 : 設定提示參數
        int optionCanvas_index = 0;
        yield return StartCoroutine(showCanvas.StartHint(optionCanvas_index, 2, 0, 3, option1Camera));

        AudioManager.instance.ResumeSound("BackgroundSource");
        livingroomShakeAni.SetBool("maxShaking", true);

        elapsed = 0.0f;
        while (elapsed < shakeDuration_max)
        {
            if (elapsed < shakeDuration_max / 4 && !isFalling) StartCoroutine(FallObjects());
            elapsed += Time.deltaTime;
            yield return null;
        }

        livingroomShakeAni.SetBool("maxShaking", false);
        livingroomShakeAni.SetBool("isShaking", false);

        transform.localPosition = originalPosition;
        isShaking = false;


    }

    private IEnumerator FallObjects()
    {
        isFalling = true;
        yield return new WaitForSeconds(waitForDown);
        foreach (GameObject fallObject in fallObjects)
        {
            yield return new WaitForSeconds(Random.Range(0.8f, 1.25f));
            fallObject.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    private IEnumerator ItemShaking()
    {
        foreach (GameObject shakingItem in shakingItems)
        {
            yield return new WaitForSeconds(0.5f);
            Animator shakingItemAnimator = shakingItem.GetComponent<Animator>();
            if (shakingItemAnimator != null)
            {
                shakingItemAnimator.SetBool("objectShaking", true);
            }
        }
    }
}
