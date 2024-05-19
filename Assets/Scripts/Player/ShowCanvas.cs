using System.Collections;
using System.Threading;
using UnityEngine;

public class ShowCanvas : MonoBehaviour
{
    #region 旋轉視角參數
    public Transform camera_Offset;
    // public Transform originalPosition;
    public Transform rotatePosition;
    public float rotateSpeed = 1f;
    private float rotateViewTime = 1f; // 旋轉視角的時間
    public Camera mainCamera;
    public Camera rotate_Camera;
    #endregion

    #region 提示選項參數
    [HideInInspector] public bool open_Timer = false;
    public GameObject timer;
    public GameObject optionCanvas;
    public float showCanvasTimes = 2f;
    #endregion

    #region 物品提示發光
    public GameObject[] hint_Objs;
    #endregion

    #region 死亡畫面
    public Transform deadPosition;
    public Animator fade_animator;
    public GameObject deadPanel;
    #endregion
    private bool isRotating = false;

    private void Awake()
    {
        timer.SetActive(false);
        optionCanvas.SetActive(false);
        rotate_Camera.enabled = false;
    }

    // 更換攝影機以旋轉到提示畫面
    public IEnumerator StartHint()
    {
        mainCamera.enabled = false;
        rotate_Camera.enabled = true;
        Hint_Glow(true);
        yield return StartCoroutine(RotateView(mainCamera.transform, rotatePosition, false));
        yield return new WaitForSeconds(2f);
        Hint_Glow(false);

        timer.SetActive(true);
        open_Timer = true;

        mainCamera.enabled = true;
        rotate_Camera.enabled = false;
    }
    //旋轉相機視角
    private IEnumerator RotateView(Transform initial, Transform target, bool is_dead)
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

        if (!is_dead)
        {
            yield return StartCoroutine(ShowOptionCanvas());
        }
        else
        {
            ShowDeadCanvas();
        }

    }

    //開啟提示選項canvas
    private IEnumerator ShowOptionCanvas()
    {
        optionCanvas.SetActive(true);
        yield return new WaitForSeconds(showCanvasTimes);
        optionCanvas.SetActive(false);
    }

    private void ShowDeadCanvas()
    {
        fade_animator.SetBool("fadein", true);

        isRotating = true;

        deadPanel.SetActive(true);
    }

    // 呼叫提示物體發光
    private void Hint_Glow(bool is_open)
    {
        foreach (GameObject hint_obj in hint_Objs)
        {
            if (hint_obj != null)
            {
                Skode_Glinting skode_Glinting = hint_obj.GetComponent<Skode_Glinting>();
                if (skode_Glinting != null)
                {
                    if (is_open) skode_Glinting.StartGlinting();
                    else skode_Glinting.StopGlinting();
                }
            }
        }
    }

    //碰到觸發死亡
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Faling") && !isRotating)
        {
            StartCoroutine(RotateView(camera_Offset, deadPosition, true));
            Destroy(other.gameObject);
        }
    }

    public void Dead()
    {
        StartCoroutine(RotateView(mainCamera.transform, deadPosition, true));
    }
}
