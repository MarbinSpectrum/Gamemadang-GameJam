using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;


public class MultiGameUI : MonoBehaviourPun
{
    [SerializeField]private InGameSync inGameSync;
    [SerializeField]private Button mainButton;

    [SerializeField]private GameObject masterUI;
    [SerializeField]private GameObject slaveUI;
    int masterHP=3;
    int slaveHP=3;

    private void Awake()
    {
        //mainButton.onClick.
        GameManager.Instance.OnLife += CheckLife;
    }

    public void CheckLife()
    {
        if(inGameSync.IsMasterClient())
        {
            masterHP--;
            inGameSync.masterHp--;
            if (masterHP==0)
            {
                inGameSync.res = GameResult.SlaveWin;
            }
        }
        else
        {
            slaveHP--;
            inGameSync.slaveHp--;

            if (slaveHP == 0)
            {
                inGameSync.res = GameResult.MasterWin;
            }
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        masterUI.transform.GetChild(masterHP).gameObject.SetActive(false);
        slaveUI.transform.GetChild(slaveHP).gameObject.SetActive(false);

    }

    public void Test()
    {
        inGameSync.slaveHp--;
    }

}
