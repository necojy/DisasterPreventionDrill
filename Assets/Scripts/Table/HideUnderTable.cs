using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class HideUnderTable : MonoBehaviour
{
    public Vector3 hidePos;
    public Vector3 origPos;
    public Quaternion origRot;
    public Quaternion flippedRotation;
    private GameObject player;
    private GameObject mainCamera;
    private GameObject table;
    private CharacterControllerDriver charCtrlDriver;
    public bool isHiding = false;
    //public bool inHidingArea = false;
    public bool canHide = true;
    public InputActionReference actionReference;
    public InputAction action;

    public Camera option2Camera;
    public ShowCanvas showCanvas;

    public GasEffect gasEffect;

    public TimeControl Countdown;
    private GameObject trackedObject;

    public GameObject handGuide;
    public Animator handPrompt;
    public bool PromptEnd = false;

    void Start()
    {
        player = GameObject.Find("XR Origin (XR Rig)");
        charCtrlDriver = player.GetComponent<CharacterControllerDriver>();
        mainCamera = GameObject.Find("Main Camera");
        table = GameObject.Find("TableToHide");

        hidePos = table.transform.position;
        action = actionReference.action;
        //action.performed += ActivateBehavior;

        // 創建一個GameObject來追蹤實際位置
        trackedObject = new GameObject("TrackedObject");
        trackedObject.transform.SetParent(player.transform);

        handGuide = GameObject.Find("HandGuide");
        handPrompt = handGuide.GetComponent<Animator>();
        handGuide.SetActive(false);
    }

    void Update()
    {
        hidePos = table.transform.position;
        hidePos.z += 0.5f;
        if (!isHiding)
        {
            origPos = player.transform.position;
            origRot = player.transform.rotation;
            //flippedRotation = Quaternion.Euler(origRot.eulerAngles.x, origRot.eulerAngles.y + 180f, origRot.eulerAngles.z);
            flippedRotation = Quaternion.Euler(0, 270f, 0);
        }
        else
        {
            // 在躲藏狀態下，確保玩家位置保持在 hidePos
            //player.transform.position = hidePos;
            trackedObject.transform.position = mainCamera.transform.position; // 保持追蹤實際位置
            trackedObject.transform.rotation = mainCamera.transform.rotation;
        }
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Player") && canHide)
        {
            Hide();
            //inHidingArea = true;
        }
    }

    private void OnTriggerExit(Collider coll)
    {
        if (coll.CompareTag("Player"))
        {
            Leave();
            //inHidingArea = false;
        }
    }

    public void Hide()
    {
        charCtrlDriver.enabled = false; // 禁用移動控制器
        isHiding = true;
        StartCoroutine(MovePlayer(hidePos, flippedRotation));
        StartCoroutine(ShowHandGuide());
    }

    public void Leave()
    {
        //StartCoroutine(MovePlayer(origPos, origRot));
        if (Countdown.timeOut && canHide) StartCoroutine(Open_OptionCanva());
        isHiding = false;
        charCtrlDriver.enabled = true; // 重新啟用移動控制器
    }

    IEnumerator MovePlayer(Vector3 targetPos, Quaternion targetRot)
    {
        float duration = 1.0f; // 移動時間（秒）
        float elapsedTime = 0f;
        Vector3 startPos = player.transform.position;
        Quaternion startRot = player.transform.rotation;

        while (elapsedTime < duration)
        {
            player.transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / duration);
            player.transform.rotation = Quaternion.Lerp(startRot, targetRot, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 確保最後的位置和旋轉是準確的
        player.transform.position = targetPos;
        player.transform.rotation = targetRot;
        canHide = true;
    }

    /* private void ActivateBehavior(InputAction.CallbackContext context)
    {
        if (isHiding && canHide)
        {
            Leave();
            isHiding = false;
            canHide = false;
        }
        else if (!isHiding && inHidingArea && canHide)
        {
            canHide = false;
            Hide();
            isHiding = true;
        }
    } */

    private IEnumerator Open_OptionCanva()
    {
        gasEffect.ShowEffect();
        yield return new WaitForSeconds(1f);
        StartCoroutine(showCanvas.StartHint(1, 2, 4, 5, option2Camera));
        canHide = false; // 確保只能躲一次
    }
    private IEnumerator ShowHandGuide()
    {
        handGuide.SetActive(true);
        handPrompt.SetBool("showPrompt",true);
        yield return new WaitForSeconds(2.5f);
        handPrompt.SetBool("showPrompt",false);
        handGuide.SetActive(false);
        PromptEnd = true;
    }

}
