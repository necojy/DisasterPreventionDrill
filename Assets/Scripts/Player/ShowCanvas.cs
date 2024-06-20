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
    public Transform camera_Offset;
    public Transform rotatePosition;
    public float rotateSpeed = 1f;
    private float rotateViewTime = 1f; // 旋轉視角的時間
    public Camera mainCamera;
    private GameObject uu;
    public Camera rotate_Camera;
    public Camera Animator_Camera;
    public Animator Animator_Control;    //用動畫控制旋轉視野

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


    private void Awake()
    {
        timer.SetActive(false);
        Caption.SetActive(false);
        rotate_Camera.enabled = false;
        isRotating = false;

        foreach (GameObject optionCanva in optionCanvas)
        {
            optionCanva.SetActive(false);
        }

        player = GameObject.Find("XR Origin (XR Rig)");
       
    }
    void Start()
    {
         uu=GameObject.Find("Main Camera");
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



    // 更換攝影機以旋轉到提示畫面
    public IEnumerator StartHint(int optionCanvas_index, int usingWhat, int hint_start, int hint_end)
    {

        isRotating = true;
        //mainCamera.enabled = false;
        rotate_Camera.enabled = true;
        Hint_Glow(true, hint_start, hint_end);

        //使用什麼工具旋轉視角
        
        Transform TT = mainCamera.transform;
        //uu.GetComponent<TrackedPoseDriver>().trackingType = TrackedPoseDriver.TrackingType.RotationOnly;
        mainCamera.transform.position = new Vector3(TT.position.x,TT.position.y,TT.position.z); 

        if (usingWhat == 1) yield return StartCoroutine(RotateView(mainCamera.transform, rotatePosition, false, optionCanvas_index));
        else if (usingWhat == 2) yield return StartCoroutine(RotateView_Animator(optionCanvas_index));

        yield return new WaitForSeconds(2f);
        Hint_Glow(false, hint_start, hint_end);

        timer.SetActive(true);
        StartCoroutine(timeControl.UpdateTimer());

        OpenScreenText(optionCanvas_index);

        mainCamera.enabled = true;
        rotate_Camera.enabled = false;
        Animator_Camera.enabled = false;

        isRotating = false;
    }
    //旋轉相機視角
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
    private IEnumerator RotateView_Animator(int optionCanvas_index)
    {
        mainCamera.enabled = false;
        Animator_Camera.enabled = true;
        Animator_Control.SetBool("canRotate", true);

        yield return StartCoroutine(ShowOptionCanvas(optionCanvas_index));
        yield return new WaitForSeconds(2f);

    }

    //開啟提示選項canvas
    private IEnumerator ShowOptionCanvas(int index)
    {
        optionCanvas[index].SetActive(true);
        yield return new WaitForSeconds(showCanvasTimes);
        optionCanvas[index].SetActive(false);
    }

    // 呼叫提示物體發光
    private void Hint_Glow(bool is_open, int start, int end)
    {
        for (int i = start; i <= end; i++)
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
        if(optionCanvas_index == 0) showCaption.ChangeCaptionContent("");
        else if(optionCanvas_index == 1) showCaption.ChangeCaptionContent("好像有奇怪的味道");
        // screenText.SetActive(true);

        StartCoroutine(showCaption.ShowWords());

        // yield return new WaitForSeconds(showCaption.duration);

        // screenText.SetActive(false);

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
    private void ShowDeadCanvas()
    {
        isRotating = true;

        fade_animator.SetBool("fadein", true);

        deadPanel.SetActive(true);

        // StartCoroutine(TT());
        // Time.timeScale = 0;
    }

    IEnumerator TT()
    {
        yield return new WaitForSeconds(3f);
        Rebirth();
    }


    public void Rebirth()
    {
        deadPanel.SetActive(false);
        fade_animator.SetBool("fadein",false);
        isRotating = false;
        reStart = true;

        camera_Offset.rotation = Quaternion.Euler(0,0,0);
        if(rebirth_index == 1)
        {
            string sceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(sceneName);
        }

        else if(rebirth_index == 2)
        {
            player.transform.position =  rebirthPos[0].position;
        }

        //恢復時間
        // Time.timeScale = 1;
        
    }
}
