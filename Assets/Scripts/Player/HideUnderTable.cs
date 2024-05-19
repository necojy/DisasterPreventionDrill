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
       
        if (isHiding)
        {
            StartCoroutine(MovePlayer(hidePos, flippedRotation));
        }
    
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
    }

    private void ActivateBehavior(InputAction.CallbackContext context)
    {   
        
        if (isHiding){
            StartCoroutine(MovePlayer(origPos, origRot));
            isHiding=false;
        }else if (inHidingArea){
            isHiding=true;
            Hide();     
        }       
    }

}
