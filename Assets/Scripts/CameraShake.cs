using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float startTime = 3f;

    #region 搖晃參數   
    public float shakeDuration = 10f;
    public float shakeMagnitude = 0.25f;
    [HideInInspector] public bool isShaking = false;
    #endregion

    #region 搖晃後的掉落物
    public float waitForDown = 1f;
    public GameObject[] fallObjects;
    private bool isFalling = false;
    #endregion
    public ShowCanvas showCanvas;
    private void Start()
    {
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

        StartCoroutine(Shake(shakeDuration,shakeMagnitude));
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        yield return new WaitForSeconds(startTime);

        //開始搖晃
        isShaking = true;

        Vector3 originalPosition = transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < duration / 2)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);
            elapsed += Time.deltaTime;
            yield return null;
            if(elapsed < duration / 4 && !isFalling) StartCoroutine(FallObjects());
        }


        //給於提示
        yield return StartCoroutine(showCanvas.StartHint());


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

    
}
