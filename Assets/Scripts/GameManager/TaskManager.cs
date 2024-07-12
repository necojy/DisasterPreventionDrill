using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    public enum TaskAlignment
    {
        Left,
        Right
    }

    public GameObject taskPrefab; // 任務條目預製件
    public Transform leftTaskListParent; // 左側任務列表的父物件
    public Transform rightTaskListParent; // 右側任務列表的父物件
    private List<string> tasks = new List<string>(); // 任務列表
    private List<GameObject> taskItems = new List<GameObject>(); // 存儲當前顯示的任務條目
    private List<TaskAlignment> taskAlignments = new List<TaskAlignment>(); // 存儲每個任務的對齊方式

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
        for (int i = 0; i < tasks.Count; i++)
        {
            var task = tasks[i];
            var alignment = taskAlignments[i];
            Transform parent = alignment == TaskAlignment.Left ? leftTaskListParent : rightTaskListParent;
            var taskItem = Instantiate(taskPrefab, parent);
            var taskText = taskItem.GetComponentInChildren<TextMeshProUGUI>();
            taskText.text = task;
            taskItems.Add(taskItem);
        }
    }

    public void CompleteTask(string completedTask)
    {
        completedTask = " - " + completedTask + "\n";
        if (tasks.Contains(completedTask))
        {
            int index = tasks.IndexOf(completedTask);
            tasks.RemoveAt(index); // 移除已完成的任務
            taskAlignments.RemoveAt(index); // 移除對應的對齊方式
            UpdateTaskUI(); // 更新任務UI
        }
    }

    public void AddTask(string newTask, char alignment)
    {
        tasks.Add(" - " + newTask + "\n");
        if (alignment == 'l')
            taskAlignments.Add(TaskAlignment.Left);
        else if (alignment == 'r')
            taskAlignments.Add(TaskAlignment.Right);

        // 新增任務並淡入顯示
        Transform parent = alignment == 'l' ? leftTaskListParent : rightTaskListParent;
        var taskItem = Instantiate(taskPrefab, parent);
        var taskText = taskItem.GetComponentInChildren<TextMeshProUGUI>();
        taskText.text = " - " + newTask + "\n";
        taskItems.Add(taskItem);
        StartCoroutine(FadeIn(taskItem));
    }

    private IEnumerator FadeIn(GameObject taskItem)
    {
        CanvasGroup canvasGroup = taskItem.AddComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        float duration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 1f;
    }
}
