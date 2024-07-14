using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineHint : MonoBehaviour
{
    public Transform playerHand; // 玩家手的位置
    public Transform target; // 目標的位置
    public float detectionRange; // 檢測範圍
    public LineRenderer lineRenderer;
    private bool isHintActive = true;


    public TaskManager taskManager;
    public string taskMessage;
    public char alignment;


    public bool isTaskAdd = false;

    void Awake()
    {

    }
    void Start()
    {
        target = this.transform;
        lineRenderer = this.GetComponent<LineRenderer>();
        taskManager = FindObjectOfType<TaskManager>();

        // 初始化 LineRenderer
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
        lineRenderer.positionCount = 2;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = new Color(1, 1, 1, 0.5f);
        lineRenderer.endColor = new Color(1, 1, 1, 0.5f);

        lineRenderer.enabled = false;
    }

    void Update()
    {
        if (isHintActive)
        {
            float distance = Vector3.Distance(playerHand.position, target.position);

            if (distance <= detectionRange)
            {
                // 顯示並更新 LineRenderer
                lineRenderer.enabled = true;
                lineRenderer.SetPosition(0, playerHand.position);
                lineRenderer.SetPosition(1, target.position);

                // 顯示任務
                if (!isTaskAdd)
                {
                    taskManager.AddTask(taskMessage, alignment);
                    isTaskAdd = true;
                }
            }
            else
            {
                // 隱藏 LineRenderer
                lineRenderer.enabled = false;
            }
        }
    }

    private void OnTriggerEnter(Collider coll)
    {
        // 檢測到玩家與道具接觸
        if (coll.CompareTag("Right Hand") || coll.CompareTag("Left Hand"))
        {
            isHintActive = false;
            lineRenderer.enabled = false; // 禁用 LineRenderer

            taskManager.CompleteTask(taskMessage);
        }
    }
}
