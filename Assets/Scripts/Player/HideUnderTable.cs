using System.Collections;
using UnityEngine;
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
    


    void Start()
    {
        player = GameObject.Find("XR Origin (XR Rig)");
        locomotion = GameObject.Find("Locomotion Systeam");
        table = GameObject.Find("FreeTable");
        coll = table.GetComponent<BoxCollider>();
        hidePos = table.transform.position;
        hidePos.y += 1f;
        
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

    public void Hide()
    {
       
        isHiding = !isHiding;
        if (isHiding)
        {
            StartCoroutine(MovePlayer(hidePos, Quaternion.Euler(0, 180, 0)));
        }
        else
        {
            StartCoroutine(MovePlayer(origPos, Quaternion.identity));
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

}
