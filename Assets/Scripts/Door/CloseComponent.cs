using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

//當玩家靠近或遠離時關閉元件
public class CloseComponent : MonoBehaviour
{
    private Transform Player;
    public Transform GrabGameObject;
    private XRGrabInteractable xRGrabInteractable;
    public float dis;

    private void Start()
    {
        Player = GameObject.Find("XR Origin (XR Rig)").transform;
        xRGrabInteractable = GrabGameObject.GetComponent<XRGrabInteractable>();
    }

    private void Update()
    {
        if(math.distance(Player.position,GrabGameObject.position) > dis)
        {
            xRGrabInteractable.enabled = false;
        }
        else
        {
            xRGrabInteractable.enabled = true;
        }
    }
}
