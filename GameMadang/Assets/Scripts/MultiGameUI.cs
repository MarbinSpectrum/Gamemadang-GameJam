using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class MultiGameUI : MonoBehaviour
{
    [SerializeField] private MultiSpawn multiSpawn;

    [SerializeField]private GameObject[] masterUI;
    [SerializeField]private GameObject[] slaveUI;

    [SerializeField]private GameObject[]scoreUI;

    [Header("ResultUI")]
    [SerializeField]private GameObject winPanel;
    [SerializeField]private GameObject LosePanel;

    [Header("CountDown")]
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private GameObject countPanel;

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TimeUI timeCheck;
    int round;

    private void Awake()
    {
        round = InGameSync.instance.round;

        GameManager.Instance.OnLife += CheckLife;
        GameManager.Instance.OnScore += CheckScore;

        Init();
    }

    private void Init()
    {
        IEnumerator CreateUnitCor()
        {
            countPanel.SetActive(true);
            timeCheck.timeOver = false;
            timeCheck.InitTime();
            InGameSync.instance.masterHp = 3;
            InGameSync.instance.slaveHp = 3;
            InGameSync.instance.SetSeed();

            yield return new WaitUntil(() => InGameSync.instance.gameSeed != 0);

            GameManager.Instance.GameStart(countText);
            multiSpawn.CreateUnit(InGameSync.instance.gameSeed);
        }

        StartCoroutine(CreateUnitCor());
    }


    private void Update()
    {
        //Debug.Log($"현재라운드 {round}");
        //Debug.Log($"동기화라운드 {inGameSync.round}");
        text.text = $"{InGameSync.instance.res}";
        if (Time.timeScale!=0)
        {
            if (timeCheck.timeOver) InGameSync.instance.res = GameResult.Draw;
            UpdateUI();
        }
    }
    public void CheckLife()
    {
        if (InGameSync.instance.IsMasterClient())
        {
            InGameSync.instance.masterHp--;
            if (InGameSync.instance.masterHp == 0)
                InGameSync.instance.res = GameResult.SlaveWin;
        }
        else
        {
            InGameSync.instance.slaveHp--;
            if (InGameSync.instance.slaveHp == 0)
                InGameSync.instance.res = GameResult.MasterWin;
        }
    }

    
    public void CheckScore()//정답 클릭시 호출
    {
        if (InGameSync.instance.IsMasterClient())
        {
            InGameSync.instance.res = GameResult.MasterWin;
        }
        else
        {
            InGameSync.instance.res = GameResult.SlaveWin;
        }
    }
    
    private void UpdateUI() // 점수와 목숨 동기화
    {
        for(int i =0; i<masterUI.Length;i++)
        {
            if(i< InGameSync.instance.masterHp)
            {
                masterUI[i].SetActive(true);
            }
            else
            {
                masterUI[i].SetActive(false);
            }
        }

        for (int i = 0; i < slaveUI.Length; i++)
        {
            if (i < InGameSync.instance.slaveHp)
            {
                slaveUI[i].SetActive(true);
            }
            else
            {
                slaveUI[i].SetActive(false);
            }
        }

        CheckResult();

    }
    private void CheckResult()
    {
        //시간 오버시
        if(InGameSync.instance.res == GameResult.Draw)
        {
            Time.timeScale = 0;
            Init();
            StartCoroutine(ReturnResult());
            return;
        }

        if(InGameSync.instance.res == GameResult.SlaveWin)
        {
            InGameSync.instance.res = GameResult.SlaveWin;
            scoreUI[round].GetComponent<Image>().color = Color.blue;

            Time.timeScale = 0;

            InGameSync.instance.slaveWin++;
            round++;

            //게임끝
            if (round==5)
            {
                GameDecision();
                return;
            }

            Init();
            StartCoroutine(ReturnResult());

        }
        else if(InGameSync.instance.res == GameResult.MasterWin)
        {
            InGameSync.instance.res = GameResult.MasterWin;
            scoreUI[round].GetComponent<Image>().color = Color.red;

            Time.timeScale = 0;

            InGameSync.instance.masterWin++;
            round++;
            if (round == 5)//게임끝
            {
                GameDecision();
                return;
            }

            Init();
            StartCoroutine(ReturnResult());

        }

    }

    private void GameDecision()
    {
        if(InGameSync.instance.masterWin> InGameSync.instance.slaveWin)
        {
            if (InGameSync.instance.IsMasterClient()) winPanel.SetActive(true);
            else LosePanel.SetActive(true);
        }
        else
        {
            if (InGameSync.instance.IsMasterClient()) LosePanel.SetActive(true);
            else winPanel.SetActive(true);
        }
       
    }
    IEnumerator ReturnResult()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        InGameSync.instance.res = GameResult.None;

    }

    public void MainScene()
    {
        SceneManager.LoadScene("Title");
    }
   

}
