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
    public float promptTime = 3f;
    public bool PromptEnd = false;

    void Start()
    {
        player = GameObject.Find("XR Origin (XR Rig)");
        charCtrlDriver = player.GetComponent<CharacterControllerDriver>();
        mainCamera = GameObject.Find("Main Camera");
        table = GameObject.Find("TableToHide");

        hidePos = table.transform.position;
        hidePos.z -= 0.5f;
        action = actionReference.action;
        //action.performed += ActivateBehavior;

        handGuide = GameObject.Find("HandGuide");
        handPrompt = handGuide.GetComponent<Animator>();
        handGuide.SetActive(false);
    }

    void Update()
    {

        if (!isHiding)
        {
            origPos = player.transform.position;
            origRot = player.transform.rotation;
            //flippedRotation = Quaternion.Euler(origRot.eulerAngles.x, origRot.eulerAngles.y + 180f, origRot.eulerAngles.z);
            //flippedRotation = Quaternion.Euler(0, 270f, 0);
        }
        else
        {

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
        StartCoroutine(MovePlayer(hidePos,  -Vector3.right));
        StartCoroutine(ShowHandGuide());
    }

    public void Leave()
    {
        //StartCoroutine(MovePlayer(origPos, origRot));
        if (Countdown.timeOut && canHide) StartCoroutine(Open_OptionCanva());
        isHiding = false;
        charCtrlDriver.enabled = true; // 重新啟用移動控制器
    }


    IEnumerator MovePlayer(Vector3 targetPos, Vector3 targetDirection)
    {
        float duration = 1.0f; // 移動時間（秒）
        float elapsedTime = 0f;
        Vector3 startPos = player.transform.position;
        Quaternion startRot = player.transform.rotation;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            player.transform.position = Vector3.Lerp(startPos, targetPos, t);

            // 目標旋轉
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

            // 當前頭顯（HMD）旋轉
            Quaternion hmdRotation = InputTracking.GetLocalRotation(XRNode.Head);

            // 調整XR Rig的旋轉，使頭顯（HMD）面向全局的目標方向
            Quaternion correctedRotation = targetRotation * Quaternion.Inverse(hmdRotation);
            player.transform.rotation = Quaternion.Lerp(startRot, correctedRotation, t);

            // 校正 X 軸和 Z 軸偏移
            Vector3 correctedPosition = new Vector3(player.transform.position.x, targetPos.y, player.transform.position.z);
            player.transform.position = correctedPosition;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 確保最後的位置和旋轉是準確的
        player.transform.position = targetPos;
        player.transform.rotation = Quaternion.LookRotation(targetDirection, Vector3.up);

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
        handPrompt.SetBool("showPrompt", true);
        yield return new WaitForSeconds(promptTime);
        handPrompt.SetBool("showPrompt", false);
        handGuide.SetActive(false);
        PromptEnd = true;
    }

}
