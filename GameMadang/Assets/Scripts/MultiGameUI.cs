using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class MultiGameUI : MonoBehaviour
{
    [SerializeField] private MultiSpawn multiSpawn;

    [SerializeField]private GameObject[] masterUI;
    [SerializeField]private GameObject[] slaveUI;

    [SerializeField]private Image[] scoreUI;

    [Header("ResultUI")]
    [SerializeField]private GameObject winPanel;
    [SerializeField]private GameObject LosePanel;

    [Header("CountDown")]
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private GameObject countPanel;

    [SerializeField] private TimeUI timeCheck;

    private bool runGame = false;

    private void Awake()
    {
        GameManager.Instance.OnLife += CheckLife;
        GameManager.Instance.OnScore += CheckScore;

        ServerMgr.instance.SetInGame();
        PhotonNetwork.Instantiate("Mouse", Vector3.zero, Quaternion.identity);

        Init();
    }

    private void Init()
    {
        InGameSync.instance.res = GameResult.None;

        runGame = false;

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

            yield return new WaitUntil(() => Time.timeScale != 0);

            runGame = true;
        }

        StartCoroutine(CreateUnitCor());
    }

    private void Update()
    {
        if (runGame)
        {
            UpdateUI();
            CheckResult();
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

    public void CheckScore()//���� Ŭ���� ȣ��
    {
        if (InGameSync.instance.IsMasterClient())
            InGameSync.instance.res = GameResult.MasterWin;
        else
            InGameSync.instance.res = GameResult.SlaveWin;
    }
    
    private void UpdateUI() // ������ ��� ����ȭ
    {
        for(int i =0; i<masterUI.Length;i++)
            masterUI[i].SetActive(i < InGameSync.instance.masterHp);
        for (int i = 0; i < slaveUI.Length; i++)
            slaveUI[i].SetActive(i < InGameSync.instance.slaveHp);

        for (int i = 0; i < InGameSync.instance.masterWin; i++)
            scoreUI[i].color = Color.red;
        for (int i = 0; i < InGameSync.instance.slaveWin; i++)
            scoreUI[scoreUI.Length - i - 1].color = Color.blue;

    }

    private void CheckResult()
    {
        //�ð� ������
        if (timeCheck.timeOver)
        {
            InGameSync.instance.res = GameResult.Draw;
            InGameSync.instance.gameSeed = 0;

            if (InGameSync.instance.res == GameResult.Draw)
            {
                Time.timeScale = 0;
                runGame = false;

                StartCoroutine(ReturnResult());
                return;
            }
        }

        int round = InGameSync.instance.slaveWin + InGameSync.instance.masterWin;

        if (InGameSync.instance.res == GameResult.SlaveWin)
        {
            Time.timeScale = 0;
            runGame = false;

            InGameSync.instance.slaveWin++;
            InGameSync.instance.gameSeed = 0;
            if (InGameSync.instance.slaveWin >= 3)
            {
                GameDecision();
                return;
            }

            StartCoroutine(ReturnResult());

        }
        else if(InGameSync.instance.res == GameResult.MasterWin)
        {
            Time.timeScale = 0;
            runGame = false;

            InGameSync.instance.masterWin++;
            InGameSync.instance.gameSeed = 0;
            if (InGameSync.instance.masterWin >= 3)//���ӳ�
            {
                GameDecision();
                return;
            }

            StartCoroutine(ReturnResult());
        }
    }

    private void GameDecision()
    {
        if(InGameSync.instance.masterWin > InGameSync.instance.slaveWin)
        {
            if (InGameSync.instance.IsMasterClient()) 
                winPanel.SetActive(true);
            else
                LosePanel.SetActive(true);
        }
        else
        {
            if (InGameSync.instance.IsMasterClient()) 
                LosePanel.SetActive(true);
            else 
                winPanel.SetActive(true);
        }   
    }

    IEnumerator ReturnResult()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        Init();
    }

    public void MainScene()
    {
        ServerMgr.instance.LeaveInGame();
        ObjectPool.Instance.ClearObj();
        Time.timeScale = 1;
    }
}
