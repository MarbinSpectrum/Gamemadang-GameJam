using UnityEngine;
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
        inGameSync.masterHp = 3;
        inGameSync.slaveHp = 3;
        round = inGameSync.round;
        countPanel.SetActive(true);
        
        GameManager.Instance.OnLife += CheckLife;
        GameManager.Instance.OnScore += CheckScore;

        GameManager.Instance.GameStart(countText);
    }
    private void Update()
    {
        Debug.Log($"게임결과 {inGameSync.res}");
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
        Debug.Log($"라운드 {round}");
        if(inGameSync.res == GameResult.SlaveWin||inGameSync.masterHp== 0 )
        {
            inGameSync.res = GameResult.SlaveWin;
            inGameSync.masterWin++;

            scoreUI[round].GetComponent<Image>().color = Color.blue;

            Time.timeScale = 0;

            if (inGameSync.IsMasterClient()) LosePanel.SetActive(true);
            else winPanel.SetActive(true);

        }
        else if(inGameSync.res == GameResult.MasterWin||inGameSync.slaveHp == 0 )
        {
            inGameSync.res = GameResult.MasterWin;
            inGameSync.slaveWin++;

            scoreUI[round].GetComponent<Image>().color = Color.red;
            Time.timeScale = 0;

            if (inGameSync.IsMasterClient()) LosePanel.SetActive(true);
            else winPanel.SetActive(true);
        }

        if (inGameSync.round == 5)
        {
            //게임 결과 송출
            return;
        }
    }

}
