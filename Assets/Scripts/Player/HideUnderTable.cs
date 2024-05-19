using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class HideUnderTable : MonoBehaviour
{
    public Vector3 hidePos;
    public Vector3 origPos;
    public Quaternion origRot;
    public Quaternion flippedRotation;
    private GameObject player;
    private GameObject table;
    private GameObject locomotion;
    private BoxCollider coll;
   
    public bool isHiding = false;
    public bool inHidingArea = false;
    public bool inputable = true;
    public InputActionReference actionReference;
    public InputAction action;



    void Start()
    {
        player = GameObject.Find("XR Origin (XR Rig)");
        locomotion = GameObject.Find("Locomotion Systeam");
        table = GameObject.Find("TableToHide");
        coll = table.GetComponent<BoxCollider>();
        hidePos = table.transform.position;
        
        action = actionReference.action;
        action.performed += ActivateBehavior;
        
    }

    void Update()
    {
        if (!isHiding)
        {
            origPos = player.transform.position;
            origRot = player.transform.rotation;
            flippedRotation = Quaternion.Euler(origRot.x, origRot.eulerAngles.y + 180f, origRot.z);
        }else{
            player.transform.position = hidePos;
        }
    }

    private void OnTriggerStay(Collider coll)
    {
        if (coll.tag == "Player")
        {   
            inHidingArea=true;
        }
    }

    private void OnTriggerExit(Collider coll)
    {
        if (coll.tag == "Player")
        {   
            inHidingArea=false;
        }
    }

    public void Hide()
    {         
        StartCoroutine(MovePlayer(hidePos, flippedRotation));        
    }
    public void Leave()
    {         
        StartCoroutine(MovePlayer(origPos, origRot));        
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
        inputable=true;
    }

    private void ActivateBehavior(InputAction.CallbackContext context)
    {   
        
        if (isHiding && inputable){
            inputable=false;
            Leave();
            isHiding=false;
        }else if (!isHiding && inHidingArea && inputable){
            inputable=false;
            Hide();     
            isHiding=true;
        }       
    }

}
