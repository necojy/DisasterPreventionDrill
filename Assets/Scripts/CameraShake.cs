using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float startTime = 3f;

    #region 搖晃參數   
    public float shakeDuration = 10f;
    public float shakeMagnitude = 0.25f;
    public bool isShaking = false;
    #endregion

    #region 搖晃後的掉落物
    public float waitForDown = 1f;
    public GameObject[] fallObjects;
    private bool isFalling = false;
    #endregion

    #region 顯示提示畫布
    public ShowCanvas showCanvas;
    #endregion
    
    public GasEffect gasEffect;

    public GameObject[] shakingItems;

    public Camera option1Camera;
    private void Start()
    {
        Init(); 
        option1Camera.enabled = false;
    }

    //初始化
    private void Init()
    {
        // 控制物體先不掉落
        foreach (GameObject fallObject in fallObjects)
        {
            fallObject.GetComponent<Rigidbody>().useGravity = false;
        }

        StartCoroutine(Shake(shakeDuration,shakeMagnitude));
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        yield return new WaitForSeconds(startTime);

        //開始搖晃
        isShaking = true;

        Vector3 originalPosition = transform.localPosition;
        float elapsed = 0.0f;

        AudioManager.instance.PlayBackground("Earthquake");
        StartCoroutine(ItemShaking());
        
        while (elapsed < duration / 4)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);
            elapsed += Time.deltaTime;
            yield return null;
            if(elapsed < duration / 4 && !isFalling) StartCoroutine(FallObjects());
        }


        //暫停音樂
        AudioManager.instance.PauseSound("BackgroundSource");

        //給於提示 : 設定提示參數
        int optionCanvas_index = 0;
        yield return StartCoroutine(showCanvas.StartHint(optionCanvas_index,2,0,1,option1Camera));

        gasEffect.showEffect();

        AudioManager.instance.ResumeSound("BackgroundSource");

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        isShaking = false;
        transform.localPosition = originalPosition;
    }

    private IEnumerator FallObjects()
    {
        isFalling = true;
        yield return new WaitForSeconds(waitForDown);
        foreach (GameObject fallObject in fallObjects)
        {
            fallObject.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    private IEnumerator ItemShaking()
    {
        foreach(GameObject shakingItem in shakingItems)
        {
            yield return new WaitForSeconds(0.5f);
            Animator shakingItemAnimator = shakingItem.GetComponent<Animator>();
            if(shakingItemAnimator != null)
            {
                shakingItemAnimator.SetBool("objectShaking",true);
            }
        }
    }
}
