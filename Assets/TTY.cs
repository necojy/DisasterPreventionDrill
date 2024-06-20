using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class TTY : MonoBehaviour
{
    private GameObject tty;
    void Start()
    {
        // int valueFromA = PlayerPrefs.GetInt("offsetValue");
        // Debug.Log("Value from SceneA: " + valueFromA); // 输出值

        tty = GameObject.Find("Main Camera");
        tty.GetComponent<TrackedPoseDriver>().trackingType = TrackedPoseDriver.TrackingType.PositionOnly;
        Debug.Log("132");
    }
}
