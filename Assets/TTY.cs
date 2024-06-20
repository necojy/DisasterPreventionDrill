using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTY : MonoBehaviour
{
    void Start()
    {
        int valueFromA = PlayerPrefs.GetInt("offsetValue");
        Debug.Log("Value from SceneA: " + valueFromA); // 输出值
    }
}
