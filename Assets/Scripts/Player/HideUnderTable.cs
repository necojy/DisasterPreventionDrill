using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HideUnderTable : MonoBehaviour
{
    public Vector3 HidePos;
    public Vector3 OrigPos;
    public Quaternion OrigRot;
    private GameObject player;
    private GameObject table;
    private BoxCollider coll;
   
    public bool isHiding = false;


    void Start()
    {
        player = GameObject.Find("XR Origin (XR Rig)");
        table = GameObject.Find("FreeTable");
        coll = table.GetComponent<BoxCollider>();
        HidePos = table.transform.position;
        HidePos.y += 1f;
        
    }

    void Update()
    {
        if (!isHiding)
        {
            OrigPos = player.transform.position;
            OrigRot = player.transform.rotation;
        }
    }

    public void Hide()
    {
        isHiding = !isHiding;
        if (isHiding)
        {
            coll.isTrigger = true;
            player.transform.position = HidePos;
            Quaternion flippedRotation = Quaternion.Euler(OrigRot.x, -OrigRot.y, OrigRot.z);
            player.transform.rotation = flippedRotation;
        }
        else
        {
            player.transform.position = OrigPos;
            player.transform.rotation = OrigRot;
            coll.isTrigger = false;
        }
    }
}
