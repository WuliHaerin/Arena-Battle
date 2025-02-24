using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour
{
    public float timeRemaining = 120; // ���õ���ʱʱ��Ϊ2���ӣ�120�룩
    public bool timerIsRunning = false; // ���Ƽ�ʱ���Ƿ�����
    public TMPro.TMP_Text timeText; // ������ʾ����ʱ��UI�ı�


    private void Start()
    {
        // ������ʱ��
        timerIsRunning = true;
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime; // ÿ֡����ʱ��
                DisplayTime(timeRemaining); // ����UI��ʾ
            }
            else
            {
                // ����ʱ����
                timeRemaining = 0;
                timerIsRunning = false;
                Debug.Log("����ʱ������");
                UIController.instance.SetGameOver();
                timerIsRunning = false;
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1; // ��ֹ��ʾ0��ʱֱ������59��

        float minutes = Mathf.FloorToInt(timeToDisplay / 60); // �������
        float seconds = Mathf.FloorToInt(timeToDisplay % 60); // ��������

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds); // ��ʽ����ʾʱ��
    }

    public void AddTime(float extraTime)
    {   if (timerIsRunning)
        {
            if (timeRemaining < 0)
                timeRemaining = 0;
            timeRemaining += extraTime; // ���Ӷ���ʱ��
        }
    }
}
