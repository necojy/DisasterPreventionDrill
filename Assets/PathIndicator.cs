using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathIndicator : MonoBehaviour
{
    public Transform player; // 玩家对象
    public Transform target; // 目标对象
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        if (player != null && target != null)
        {
            lineRenderer.SetPosition(0, player.position); // 设置起点为玩家位置
            lineRenderer.SetPosition(1, target.position); // 设置终点为目标位置
        }
    }
}
