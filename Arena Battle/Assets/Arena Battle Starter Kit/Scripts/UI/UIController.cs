using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {
    public static UIController instance;

    /* prefabs */
    public GameOverlayWindowCtrl GameOverlayWindow;
    public LoadingWindowCtrl LoadingWindow;




    /* public vars */
    public GameObject Design;
    public GameObject WindowOverlay;

    public GameObject ScorePanel;
    public TMPro.TMP_Text ourScoreText;
    public TMPro.TMP_Text enemyScoreText;
    private int ourScore = 0;
    private int enemyScore = 0;
    public GameObject TimePanel;
    public GameObject GameOverPanel;
    public GameObject ReviveBtn;
    public TMPro.TMP_Text GameOverText;


    public void SetScore(int value,bool isOur)
    {
        if(isOur)
        {
            ourScore += value;
            ourScoreText.text=ourScore.ToString();
        }
        else
        {
            enemyScore += value;
            enemyScoreText.text=enemyScore.ToString();
        }
    }

    public void SetGameOver()
    {
        Time.timeScale = 0;
        TimePanel.SetActive(false);
        ScorePanel.SetActive(false);
        GameOverPanel.SetActive(true);
        
        if(ourScore>enemyScore)
        {
            GameOverText.text="  我方胜利！！";
        }
        else if(ourScore<enemyScore)
        {
            GameOverText.text= "  敌方胜利！！";
            ReviveBtn.SetActive(true);
        }
        else
        {
            GameOverText.text= "  平局！！";
            ReviveBtn.SetActive(true);
        }
        AdManager.ShowInterstitialAd("1lcaf5895d5l1293dc",
            () => {
                Debug.Log("--插屏广告完成--");

            },
            (it, str) => {
                Debug.LogError("Error->" + str);
            });
    }

    public void BackMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void Revive()
    {
        AdManager.ShowVideoAd("192if3b93qo6991ed0",
            (bol) => {
                if (bol)
                {
                    ScorePanel.SetActive(true);
                    TimePanel.SetActive(true);
                    GameOverPanel.SetActive(false);
                    TimePanel.GetComponent<CountDownTimer>().timerIsRunning = true;
                    TimePanel.GetComponent<CountDownTimer>().AddTime(15);
                    Time.timeScale = 1;

                    AdManager.clickid = "";
                    AdManager.getClickid();
                    AdManager.apiSend("game_addiction", AdManager.clickid);
                    AdManager.apiSend("lt_roi", AdManager.clickid);


                }
                else
                {
                    StarkSDKSpace.AndroidUIManager.ShowToast("观看完整视频才能获取奖励哦！");
                }
            },
            (it, str) => {
                Debug.LogError("Error->" + str);
                //AndroidUIManager.ShowToast("广告加载异常，请重新看广告！");
            });

    }

    void Awake () {
        instance = this;
        this.Design.SetActive(false);

        this.ShowLoadingWindow();
    }
	
	void Update () {
	    
	}

    public void ShowWindow(ClosableWindowCtrl windowPrefab)
    {
        GameObject inst = Utils.CreateInstance(windowPrefab.gameObject, this.WindowOverlay, true);
        ClosableWindowCtrl win = inst.GetComponent<ClosableWindowCtrl>();

        win.Init();
    }

    public void CloseWindow(ClosableWindowCtrl window)
    {
        Destroy(window.gameObject);
    }


    public void ShowLoadingWindow()
    {
        this.ShowWindow(this.LoadingWindow);
    }

    public void ShowGameOverlayWindow()
    {
        this.ShowWindow(this.GameOverlayWindow);
        TimePanel.SetActive(true);
        ScorePanel.SetActive(true);

    }
}
