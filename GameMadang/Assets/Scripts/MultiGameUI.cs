using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;


public class MultiGameUI : MonoBehaviourPun
{
    [SerializeField]private InGameSync inGameSync;
    [SerializeField]private Button mainButton;

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
    int round;

    private void Awake()
    {
        round = inGameSync.round;
        GameManager.Instance.OnLife += CheckLife;
        GameManager.Instance.OnScore += CheckScore;
        Init();
    }
    private void Init()
    {
        inGameSync.masterHp = 3;
        inGameSync.slaveHp = 3;
        countPanel.SetActive(true);

        GameManager.Instance.GameStart(countText);
    }
    private void Update()
    {
        Debug.Log($"현재라운드 {round}");
        //Debug.Log($"동기화라운드 {inGameSync.round}");
        text.text = $"{inGameSync.res}";
        if (Time.timeScale!=0)
        {
            UpdateUI();
        }
        
    }

    public void CheckLife()
    {
        if (inGameSync.IsMasterClient())
        {
            inGameSync.masterHp--;
        }
        else
        {
            inGameSync.slaveHp--;
        }
    }

    
    public void CheckScore()//정답 클릭시 호출
    {
        if (inGameSync.IsMasterClient())
        {
            inGameSync.res = GameResult.MasterWin;
        }
        else
        {
            inGameSync.res = GameResult.SlaveWin;
        }
    }
    
    private void UpdateUI() // 점수와 목숨 동기화
    {
        for(int i =0; i<masterUI.Length;i++)
        {
            if(i< inGameSync.masterHp)
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
            if (i < inGameSync.slaveHp)
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
        if(inGameSync.res == GameResult.SlaveWin||inGameSync.masterHp== 0 )
        {
            inGameSync.res = GameResult.SlaveWin;
            scoreUI[round].GetComponent<Image>().color = Color.blue;

            Init();

            inGameSync.slaveWin++;
            round++;
            StartCoroutine(ReturnResult());

        }
        else if(inGameSync.res == GameResult.MasterWin || inGameSync.slaveHp == 0 )
        {
            inGameSync.res = GameResult.MasterWin;
            scoreUI[round].GetComponent<Image>().color = Color.red;

            Init();

            round++;
            inGameSync.masterWin++;
            StartCoroutine(ReturnResult());

        }

        if (inGameSync.round == 5)
        {
            //게임 결과 송출
            //if (inGameSync.IsMasterClient()) LosePanel.SetActive(true);
            //else winPanel.SetActive(true);
            return;
        }
    }
    IEnumerator ReturnResult()
    {
        Debug.Log(1);
        yield return new WaitForSecondsRealtime(1f);
        inGameSync.res = GameResult.None;

    }
   

}
