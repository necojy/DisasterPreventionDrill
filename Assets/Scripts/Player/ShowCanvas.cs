using System.Collections;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ShowCanvas : MonoBehaviour
{
    private GameObject player;

    #region 旋轉視角參數
    private Vector3 origPos;
    private Quaternion origRot; //紀錄玩家位置跟方向

    private bool isRotating = false;
    private Transform camera_Offset;
    private float rotateSpeed = 1f;
    private float rotateViewTime = 1f; // 旋轉視角的時間
    private Camera mainCamera; 
    private Transform rotatePosition;

    #endregion

    #region 提示選項參數
    public GameObject timer;
    public TimeControl timeControl;

    public GameObject[] optionCanvas;
    public float showCanvasTimes = 2f;
    public GameObject Caption;
    public ShowCaption showCaption;
    #endregion

    #region 物品提示發光
    public GameObject[] hint_Objs;
    #endregion

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

    private float recipNum = 5f;

    private void Awake()
    {
        timer.SetActive(false);
        Caption.SetActive(false);
        isRotating = false;

        foreach (GameObject optionCanva in optionCanvas)
        {
            optionCanva.SetActive(false);
        }

        player = GameObject.Find("XR Origin (XR Rig)");
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        camera_Offset = GameObject.Find("Camera Offset").transform;


    }
    void Update()
    {
        if (!isRotating)
        {
            origPos = player.transform.position;
            origRot = player.transform.rotation;
        }
        else
        {
            player.transform.position = origPos;
        }
    }



    //更換攝影機以旋轉到提示畫面(第幾個提示,提示index_start,提示index_end,物理旋轉到的位置,動畫旋轉使用的相機)
    public IEnumerator StartHint(int optionCanvas_index, int usingWhat, int hint_start, int hint_end, Camera rotateCamera)
    {

        isRotating = true;
        
        //物品發光提示
        Hint_Glow(true, hint_start, hint_end);

        //使用什麼工具旋轉視角
        if (usingWhat == 1) yield return StartCoroutine(RotateView(mainCamera.transform, rotatePosition, false, optionCanvas_index));
        else if (usingWhat == 2) yield return StartCoroutine(RotateView_Animator(optionCanvas_index, rotateCamera));

        yield return new WaitForSeconds(1.25f);

        Hint_Glow(false, hint_start, hint_end);

        //開啟計時器
        timer.SetActive(true);
        StartCoroutine(timeControl.UpdateTimer());

        //開啟字幕
        OpenScreenText(optionCanvas_index);

        isRotating = false;

    }
    //旋轉相機視角(物理旋轉)
    private IEnumerator RotateView(Transform initial, Transform target, bool is_dead, int optionCanvas_index)
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
            yield return StartCoroutine(ShowOptionCanvas(optionCanvas_index));
        }
        else
        {
            ShowDeadCanvas();
        }
    }

    //使用動畫旋轉相機視角
    private IEnumerator RotateView_Animator(int optionCanvas_index, Camera rotateCamera)
    {
        //切換提示相機
        mainCamera.enabled = false;
        rotateCamera.enabled = true;

        Animator animatorCamera = rotateCamera.GetComponent<Animator>();
        animatorCamera.SetBool("canRotate", true);

        // 開啟提示選項canvas
        yield return StartCoroutine(ShowOptionCanvas(optionCanvas_index));

        //關閉提示相機
        mainCamera.enabled = true;
        rotateCamera.enabled = false;
    }

    //開啟提示選項canvas
    private IEnumerator ShowOptionCanvas(int index)
    {
        optionCanvas[index].SetActive(true);
        yield return new WaitForSeconds(showCanvasTimes);
        optionCanvas[index].SetActive(false);
    }

    // 呼叫提示物體發光 (開關，編號幾到幾發光)
    private void Hint_Glow(bool is_open, int start_index, int end_index)
    {
        for (int i = start_index; i <= end_index; i++)
        {
            if (hint_Objs[i] != null)
            {
                Skode_Glinting skode_Glinting = hint_Objs[i].GetComponent<Skode_Glinting>();
                if (skode_Glinting != null)
                {
                    if (is_open) skode_Glinting.StartGlinting();
                    else skode_Glinting.StopGlinting();
                }
            }
        }
    }

    //開啟字幕
    public void OpenScreenText(int optionCanvas_index)
    {
        // 設定字幕內容
        if (optionCanvas_index == -1) showCaption.ChangeCaptionContent(recipNum.ToString());
        else if (optionCanvas_index == 0) showCaption.ChangeCaptionContent("");
        else if (optionCanvas_index == 1) showCaption.ChangeCaptionContent("好像有奇怪的味道");

        // 開啟字幕
        StartCoroutine(showCaption.ShowWords());
    }


    //碰到觸發死亡
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Faling"))
        {
            StartCoroutine(RotateView(camera_Offset, deadPosition, true, -1));
            Destroy(other.gameObject);
        }
    }

    public void Dead(int deadIndex)
    {
        StartCoroutine(RotateView(mainCamera.transform, deadPosition, true, -1));
        rebirth_index = deadIndex;
    }
    public void ShowDeadCanvas()
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
            // player.transform.position = rebirthPos[0].position;
        }

        //恢復時間
        // Time.timeScale = 1;

    }
}
