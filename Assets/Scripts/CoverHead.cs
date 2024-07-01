using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverHead : MonoBehaviour
{
    public Collider colliderA;
    public Collider colliderB;
    private bool isColliderAInside = false;
    private bool isColliderBInside = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other == colliderA)
        {
            isColliderAInside = true;
        }

        if (other == colliderB)
        {
            isColliderBInside = true;
        }

        if (isColliderAInside && isColliderBInside)
        {
            Debug.Log("開門");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == colliderA)
        {
            isColliderAInside = false;
        }

        if (other == colliderB)
        {
            isColliderBInside = false;
        }
    }

    public GameObject player;
    public Transform cube;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 newPosition = player.transform.position;  // 获取 player 的位置
        newPosition.y += 2;  // 修改 y 坐标
        cube.position = newPosition;  // 设置 cube 的位置
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = player.transform.position;  // 获取 player 的位置
        newPosition.y += 2;  // 修改 y 坐标
        cube.position = newPosition;  // 设置 cube 的位置
    }
}
