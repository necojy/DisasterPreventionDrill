using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class HideUnderTable : MonoBehaviour
{
    public bool isHiding = false;
    public bool canHide = true;

    public Camera option2Camera;
    public ShowCanvas showCanvas;

    public GasEffect gasEffect;

    public TimeControl Countdown;
    private GameObject trackedObject;

    public GameObject handGuide;
    public Animator handPrompt;
    public float promptTime = 3f;
    public bool PromptEnd = false;

    public Transform head;
    public Transform original;
    public Transform target;


    private GameObject player;
    void Start()
    {
        player = GameObject.Find("Camera Offset");
        handGuide = GameObject.Find("HandGuide");
        handPrompt = handGuide.GetComponent<Animator>();
        handGuide.SetActive(false);
    }


    private void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Player") && canHide)
        {
            StartCoroutine(Hide());
        }
    }

    private void OnTriggerExit(Collider coll)
    {
        if (coll.CompareTag("Player"))
        {
            Leave();
        }
    }

    public IEnumerator Hide()
    {
        isHiding = true;
        yield return StartCoroutine(MovePlayer(target.position, Quaternion.Euler(0, 270f, 0)));
        Recenter();
        StartCoroutine(ShowHandGuide());
    }

    public void Leave()
    {
        if (Countdown.timeOut && canHide) StartCoroutine(Open_OptionCanva());
        isHiding = false;

    }

    public void Recenter()
    {
        // 計算頭部（頭顯）位置和原始位置之間的偏移量
        Vector3 offset = head.position - original.position;
        offset.y = 0; // 忽略 y 軸上的偏移量，僅考慮水平平面的偏移

        // 將原始位置設置為目標位置減去偏移量
        original.position = target.position - offset;

        // 獲取目標的前向向量，並將 y 軸設置為 0，僅考慮水平平面的方向
        Vector3 targetFoward = target.forward;
        targetFoward.y = 0;

        // 獲取頭部（頭顯）的前向向量，並將 y 軸設置為 0，僅考慮水平平面的方向
        Vector3 cameraFoward = head.forward;
        cameraFoward.y = 0;

        // 計算頭部（頭顯）前向向量與目標前向向量之間的夾角
        float angle = Vector3.SignedAngle(cameraFoward, targetFoward, Vector3.up);

        // 以頭部（頭顯）位置為中心，繞 y 軸旋轉原始位置，使其對準目標方向
        original.RotateAround(head.position, Vector3.up, angle);
    }

    IEnumerator MovePlayer(Vector3 targetPos, Quaternion targetRot)
    {
        float duration = 0.5f; // 移動時間（秒）
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
