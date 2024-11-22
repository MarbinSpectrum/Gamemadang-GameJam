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

    [SerializeField]private GameObject scoreUI;

    public TextMeshProUGUI text;
 

    private void Awake()
    {
        //mainButton.onClick.
        if (inGameSync.IsMasterClient())
        {
            inGameSync.masterHp = 3;
        }
        else
        {
            inGameSync.slaveHp = 3;
        }
        GameManager.Instance.OnLife += CheckLife;
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
        UpdateUI();
        
    }
    private void Update()
    {
        text.text = $"{inGameSync.slaveHp}";
        UpdateUI();
        if (inGameSync.masterHp == 0 || inGameSync.slaveHp == 0) 
        CheckResult();
    }

    private void UpdateUI()
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
       
    }
    private void CheckResult()
    {
        if(inGameSync.masterHp== 0)
        {
            inGameSync.res = GameResult.MasterWin;
            inGameSync.masterWin++;
            scoreUI.transform.GetChild(inGameSync.round - 1).GetComponent<Image>().color = Color.red;
        }
        else if(inGameSync.slaveHp == 0)
        {
            inGameSync.res = GameResult.SlaveWin;
            inGameSync.slaveWin++;
            scoreUI.transform.GetChild(inGameSync.round - 1).GetComponent<Image>().color = Color.blue;
        }

        if(inGameSync.round == 5)
        {
            //게임 결과 송출
            return;
        }
        inGameSync.round++;
    }

    public void Test()
    {
        inGameSync.slaveHp--;

    }

}
