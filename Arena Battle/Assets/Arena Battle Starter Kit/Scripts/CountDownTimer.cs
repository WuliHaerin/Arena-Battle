using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour
{
    public float timeRemaining = 120; // 设置倒计时时间为2分钟（120秒）
    public bool timerIsRunning = false; // 控制计时器是否运行
    public TMPro.TMP_Text timeText; // 用于显示倒计时的UI文本


    private void Start()
    {
        // 启动计时器
        timerIsRunning = true;
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime; // 每帧减少时间
                DisplayTime(timeRemaining); // 更新UI显示
            }
            else
            {
                // 倒计时结束
                timeRemaining = 0;
                timerIsRunning = false;
                Debug.Log("倒计时结束！");
                UIController.instance.SetGameOver();
                timerIsRunning = false;
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1; // 防止显示0秒时直接跳到59秒

        float minutes = Mathf.FloorToInt(timeToDisplay / 60); // 计算分钟
        float seconds = Mathf.FloorToInt(timeToDisplay % 60); // 计算秒数

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds); // 格式化显示时间
    }

    public void AddTime(float extraTime)
    {   if (timerIsRunning)
        {
            if (timeRemaining < 0)
                timeRemaining = 0;
            timeRemaining += extraTime; // 增加额外时间
        }
    }
}
