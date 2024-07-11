using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    public GameObject taskPrefab; // 任務條目預製件
    public Transform taskListParent; // 任務列表的父物件，用於顯示任務條目
    private List<string> tasks = new List<string>(); // 任務列表
    private List<GameObject> taskItems = new List<GameObject>(); // 存儲當前顯示的任務條目

    void Start()
    {
        UpdateTaskUI(); // 初始化任務UI
    }

    void UpdateTaskUI()
    {
        // 清除當前顯示的所有任務條目
        foreach (var item in taskItems)
        {
            Destroy(item);
        }
        taskItems.Clear();

        // 創建並顯示新的任務條目
        if (tasks.Count > 0)
        {
            foreach (var task in tasks)
            {
                var taskItem = Instantiate(taskPrefab, taskListParent);
                taskItem.GetComponentInChildren<TextMeshProUGUI>().text = task;
                taskItems.Add(taskItem);
            }
        }
        else
        {
            // 如果沒有任務，顯示默認信息


        }
    }

    public void CompleteTask(string completedTask)
    {
        completedTask = " - " + completedTask + "\n";
        if (tasks.Contains(completedTask))
        {
            tasks.Remove(completedTask); // 移除已完成的任務
            UpdateTaskUI(); // 更新任務UI
        }
    }

    public void AddTask(string newTask)
    {
        tasks.Add(" - " + newTask + "\n");
        UpdateTaskUI();
    }
}

