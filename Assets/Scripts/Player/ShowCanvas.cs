using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCanvas : MonoBehaviour
{

    #region 旋轉視角參數
    public Transform camera_Offset;
    public Transform rotatePosition;
    public float rotateSpeed = 1f;
    private float rotateViewTime = 1f; // 旋轉視角的時間
    public Camera mainCamera;
    public Camera rotate_Camera;
    #endregion

    #region 提示選項參數
    public GameObject optionCanvas;
    public float showCanvasTimes = 2f;
    #endregion


    private void Awake()
    {
        optionCanvas.SetActive(false);
        rotate_Camera.enabled = false;
    }

    // 更換攝影機以旋轉到提示畫面
    public IEnumerator StartHint()
    {
        mainCamera.enabled = false;
        rotate_Camera.enabled = true;
        yield return StartCoroutine(RotateView(camera_Offset, rotatePosition, true));
        yield return StartCoroutine(RotateView(rotatePosition, camera_Offset, false));
        mainCamera.enabled = true;
        rotate_Camera.enabled = false;
    }

    //旋轉相機視角
    private IEnumerator RotateView(Transform initial, Transform target, bool open)
    {
        Quaternion initialRotation = initial.rotation;
        Vector3 direction = target.position - initial.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        //更新下次旋轉位置(恢復視角);
        rotatePosition = camera_Offset;

        float elapsedTime = 0f;
        while (elapsedTime < rotateViewTime)
        {
            transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / rotateViewTime);
            elapsedTime += Time.deltaTime * rotateSpeed;
            yield return null;
        }

        // 確保最終旋轉到目標旋轉
        transform.rotation = targetRotation;



        if (open) yield return StartCoroutine(ShowOptionCanvas());
    }

    private IEnumerator ShowOptionCanvas()
    {
        optionCanvas.SetActive(true);
        yield return new WaitForSeconds(showCanvasTimes);
        optionCanvas.SetActive(false);
    }
}