using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Storytwo : MonoBehaviour
{
    private GameObject player;
    private GameObject mainCamera;
    public GasEffect gasEffect;
    public Transform camera_Offset;
    public float rotateSpeed = 1f;
    private bool isRotating = false;

    private float rotateViewTime = 1f; // 旋轉視角的時間

    #region 死亡畫面
    public Transform deadPosition;
    public Animator fade_animator;
    public GameObject deadPanel;
    #endregion

    #region 復活參數
    public int rebirth_index = 0;
    public Transform[] rebirthPos;

    [HideInInspector] public bool reStart = false;
    #endregion

    private void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        player = GameObject.Find("XR Origin (XR Rig)");

        gasEffect.showEffect();
    }

    public void Dead(int deadIndex)
    {
        StartCoroutine(RotateView(mainCamera.transform, deadPosition));
        rebirth_index = deadIndex;
    }

    //旋轉相機視角
    private IEnumerator RotateView(Transform initial, Transform target)
    {
        Quaternion initialRotation = initial.rotation;
        Vector3 direction = target.position - initial.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);


        float elapsedTime = 0f;
        while (elapsedTime < rotateViewTime)
        {
            camera_Offset.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / rotateViewTime);
            elapsedTime += Time.deltaTime * rotateSpeed;
            yield return null;
        }

        ShowDeadCanvas();

    }

    private void ShowDeadCanvas()
    {
        isRotating = true;

        fade_animator.SetBool("fadein", true);

        deadPanel.SetActive(true);

        StartCoroutine(DeadRecip());
    }

    IEnumerator DeadRecip()
    {
        fade_animator.SetBool("isDead", true);
        yield return new WaitForSeconds(5f);
        Rebirth();
    }

    public void Rebirth()
    {
        deadPanel.SetActive(false);
        fade_animator.SetBool("fadein", false);
        isRotating = false;
        reStart = true;

        camera_Offset.rotation = Quaternion.Euler(0, 0, 0);
        if (rebirth_index == 1)
        {
            string sceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(sceneName);
        }

        else if (rebirth_index == 2)
        {
            SceneManager.LoadScene("Story2");
        }

        //恢復時間
        // Time.timeScale = 1;

    }
}
