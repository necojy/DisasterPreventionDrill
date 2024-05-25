using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Try : MonoBehaviour
{

    // public Vector3 hidePos;
    public Vector3 rotatePos;
    public Vector3 origPos;
    public Quaternion origRot;
    public Quaternion flippedRotation;
    private GameObject player;
    private GameObject mainCamera;
    // private GameObject table;
    public Transform rotatePosition;

    // public bool isHiding = false;
    public bool isRotating = false;
    public bool inHidingArea = false;


    public bool hide = false;
    void Start()
    {
        player = GameObject.Find("XR Origin (XR Rig)");
        mainCamera = GameObject.Find("Main Camera");
        // table = GameObject.Find("TableToHide");
        // hidePos = table.transform.position;
        rotatePos = rotatePosition.position;

        rotatePos.y += 1;
    }

    void Update()
    {
        test();

        if (!isRotating)
        {
            origPos = player.transform.position;
            origRot = player.transform.rotation;
            flippedRotation = Quaternion.Euler(origRot.x, origRot.eulerAngles.y -90f, origRot.z);
        }
        else
        {
            player.transform.position = rotatePos;
        }
    }

    private void test()
    {
        if(hide && !isRotating)
        {
            Hide();     
            isRotating=true;
        }

        else if(!hide && isRotating)
        {
            Leave();
            isRotating=false;
        }
    }

        public void Hide()
    {         
        StartCoroutine(MovePlayer(rotatePos, flippedRotation));        
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
        mainCamera.transform.position = new Vector3(0,0,0);
    }
}
