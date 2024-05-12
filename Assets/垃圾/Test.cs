using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Test : XRGrabInteractable
{
    // protected override void OnSelectEntered(SelectEnterEventArgs args)
    // {
    //     Debug.Log(args.interactor.name); // 輸出抓取物體的名字
    //     Debug.Log(args.interactor.transform.tag); // 輸出抓取物體的名字

    //     if(args.interactor.transform.CompareTag("aaa"))
    //     {
    //         Debug.Log("left");
    //     }

    //     if(args.interactor.transform.CompareTag("Right Hand"))
    //     {
    //         Debug.Log("Right");
    //     }
    //     base.OnSelectEntered(args);
    // }

    // public void tt()
    // {
    //     Debug.Log("123");
    // }
}
