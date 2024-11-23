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

 

    private void Awake()
    {
        inGameSync.masterHp = 3;
        inGameSync.slaveHp = 3;
        GameManager.Instance.OnLife += CheckLife;
    }
    private void Update()
    {
        UpdateUI();
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

        CheckResult();

    }
    private void CheckResult()
    {
        Debug.Log(inGameSync.round);
        if(inGameSync.masterHp== 0)
        {
            inGameSync.res = GameResult.MasterWin;
            inGameSync.masterWin++;
            inGameSync.round++;

            scoreUI[inGameSync.round+1].GetComponent<Image>().color = Color.red;

            Time.timeScale = 0;
        }
        else if(inGameSync.slaveHp == 0)
        {
            inGameSync.res = GameResult.SlaveWin;
            inGameSync.slaveWin++;
            inGameSync.round++;

            scoreUI[inGameSync.round+1].GetComponent<Image>().color = Color.blue;
            Time.timeScale = 0;

        }

        if (inGameSync.round == 5)
        {
            //게임 결과 송출
            return;
        }
    }

}
