using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveObject : MonoBehaviour
{
  void Awake()
    {
        DontDestroyOnLoad(gameObject); // 保留此物件
    }
}
