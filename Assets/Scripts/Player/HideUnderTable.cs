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
    private GameObject locomotion;
    //private BoxCollider coll;
    private CharacterControllerDriver charCtrlDriver;
    public bool isHiding = false;
    public bool inHidingArea = false;
    public bool inputable = true;
    public InputActionReference actionReference;
    public InputAction action;


    public Camera option2Camera;
    public ShowCanvas showCanvas;
    private bool isEnding = false;

    void Start()
    {
        player = GameObject.Find("XR Origin (XR Rig)");
        charCtrlDriver = player.GetComponent<CharacterControllerDriver>();
        locomotion = GameObject.Find("Locomotion Systeam");
        mainCamera = GameObject.Find("Main Camera");
        table = GameObject.Find("TableToHide");
        //coll = table.GetComponent<BoxCollider>();
        hidePos = table.transform.position;
        action = actionReference.action;
        action.performed += ActivateBehavior;

    }

    void Update()
    {
        hidePos = table.transform.position;
        if (!isHiding)
        {
            mainCamera.GetComponent<TrackedPoseDriver>().trackingType = TrackedPoseDriver.TrackingType.RotationAndPosition;
            origPos = player.transform.position;
            origRot = player.transform.rotation;
            flippedRotation = Quaternion.Euler(origRot.x, origRot.eulerAngles.y + 180f, origRot.z);
        }
        else
        {
            player.transform.position = hidePos;
            hidePos.y += 1;
            mainCamera.GetComponent<TrackedPoseDriver>().trackingType = TrackedPoseDriver.TrackingType.RotationOnly;
            mainCamera.transform.position = hidePos;
        }
    }

    private void OnTriggerStay(Collider coll)
    {
        if (coll.tag == "Left Hand" || coll.tag == "Right Hand")
        {
            inHidingArea = true;
        }
    }

    private void OnTriggerExit(Collider coll)
    {
        if (coll.tag == "Left Hand" || coll.tag == "Right Hand")
        {
            inHidingArea = false;
        }
    }

    public void Hide()
    {
        charCtrlDriver.enabled = false;
        StartCoroutine(MovePlayer(hidePos, flippedRotation));
    }
    public void Leave()
    {
        StartCoroutine(MovePlayer(origPos, origRot));
        StartCoroutine(Open_OptionCanva());
        charCtrlDriver.enabled = true;
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

        player.transform.position = targetPos;
        player.transform.rotation = targetRot;
        mainCamera.transform.position = new Vector3(0, 0, 0);
        inputable = true;
    }

    private void ActivateBehavior(InputAction.CallbackContext context)
    {

        if (isHiding && inputable)
        {
            Leave();
            isHiding = false;
            inputable = false;

        }
        else if (!isHiding && inHidingArea && inputable)
        {
            inputable = false;
            Hide();
            isHiding = true;
        }
    }

    private IEnumerator Open_OptionCanva()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(showCanvas.StartHint(1, 2, 2, 3, option2Camera));
        inputable = false; //確保只能躲一次
    }

}
