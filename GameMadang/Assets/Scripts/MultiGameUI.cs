using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] private CutSceneEvent masterCutScene;
    [SerializeField] private CutSceneEvent slaveCutScene;

    private bool runGame = false;
    private int gameCnt = 0;
    private GameObject myMouse = null;
    private List<int> stageNum = null;

    private void Awake()
    {
        GameManager.Instance.OnLife = CheckLife;
        GameManager.Instance.OnScore = CheckScore;
        if (PhotonNetwork.IsMasterClient)
            InGameSync.instance.gameSeed = 0;
        InGameSync.instance.masterWin = 0;
        InGameSync.instance.slaveWin = 0;

        ServerMgr.instance.SetInGame();

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

            masterCutScene.gameObject.SetActive(false);
            slaveCutScene.gameObject.SetActive(false);

            if (stageNum == null)
                stageNum = FenwickTreeRandomPicker.GetRandomNumbers(6, 5, InGameSync.instance.gameSeed);

            GameManager.Instance.GameStart(countText);
            int nowRound = InGameSync.instance.masterWin + InGameSync.instance.slaveWin;

            multiSpawn.CreateUnit(nowRound,InGameSync.instance.gameSeed + gameCnt);

            gameCnt += 200000;
            yield return new WaitUntil(() => Time.timeScale != 0);
            if (myMouse == null)
                myMouse = PhotonNetwork.Instantiate("Mouse", Vector3.zero, Quaternion.identity);
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
        SoundMgr.Instance.PlaySE(Sound.SE_WrongExplosion);
        if (InGameSync.instance.IsMasterClient())
        {
            InGameSync.instance.masterHp--;
            if (InGameSync.instance.masterHp == 0)
            {
                InGameSync.instance.res = GameResult.SlaveWin;
                InGameSync.instance.slaveWin++;
            }
        }
        else
        {
            InGameSync.instance.slaveHp--;
            if (InGameSync.instance.slaveHp == 0)
            {
                InGameSync.instance.res = GameResult.MasterWin;
                InGameSync.instance.masterWin++;
            }
        }
    }

    public void CheckScore()//정답 클릭시 호출
    {
        SoundMgr.Instance.PlaySE(Sound.SE_Explosion);
        if (InGameSync.instance.IsMasterClient())
        {
            InGameSync.instance.res = GameResult.MasterWin;
            InGameSync.instance.masterWin++;
        }
        else
        {
            InGameSync.instance.res = GameResult.SlaveWin;
            InGameSync.instance.slaveWin++;
        }
    }
    
    private void UpdateUI() // 점수와 목숨 동기화
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
        //시간 오버시
        if (timeCheck.timeOver)
        {
            InGameSync.instance.res = GameResult.Draw;
            if (InGameSync.instance.res == GameResult.Draw)
            {
                Time.timeScale = 0;
                runGame = false;

                StartCoroutine(ReturnResult());
                return;
            }
        }

        if (InGameSync.instance.res == GameResult.SlaveWin)
        {
            Time.timeScale = 0;
            runGame = false;

            if (InGameSync.instance.slaveWin >= 3)
            {
                InGameSync.instance.gameSeed = 0;
                GameDecision();
                return;
            }

            StartCoroutine(ReturnResult());

        }
        else if(InGameSync.instance.res == GameResult.MasterWin)
        {
            Time.timeScale = 0;
            runGame = false;

            if (InGameSync.instance.masterWin >= 3)//게임끝
            {
                InGameSync.instance.gameSeed = 0;
                GameDecision();
                return;
            }

            StartCoroutine(ReturnResult());
        }
    }

    private void GameDecision()
    {
        IEnumerator ReturnResult()
        {
            SoundMgr.Instance.PlaySE(Sound.SE_CutScene);
            if (InGameSync.instance.res == GameResult.MasterWin)
                masterCutScene.gameObject.SetActive(true);
            else if (InGameSync.instance.res == GameResult.SlaveWin)
                slaveCutScene.gameObject.SetActive(true);

            yield return new WaitForSecondsRealtime(5f);

            if (InGameSync.instance.masterWin > InGameSync.instance.slaveWin)
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
            SoundMgr.Instance.PlaySE(Sound.SE_GameEndPopup);
        }
        StartCoroutine(ReturnResult());
    }

    IEnumerator ReturnResult()
    {
        if(InGameSync.instance.res == GameResult.Draw)
        {
            yield return new WaitForSecondsRealtime(1f);
        }
        else
        {
            if (InGameSync.instance.res == GameResult.MasterWin)
                masterCutScene.gameObject.SetActive(true);
            else if (InGameSync.instance.res == GameResult.SlaveWin)
                slaveCutScene.gameObject.SetActive(true);

            yield return new WaitForSecondsRealtime(5f);
        }

        Init();
    }

    public void MultiShootSFX_Master()
    {
        MultiUnit targetUnit = multiSpawn.TargetUnit();
        Vector2 pos = Camera.main.WorldToScreenPoint(targetUnit.transform.position);
        targetUnit.gameObject.SetActive(false);

        UnitObj unitObj = ObjectPool.Instance.SpawnFromPool(targetUnit.unitTag);
        unitObj.transform.position = targetUnit.transform.position;
        unitObj.transform.localScale = targetUnit.transform.localScale;
        masterCutScene.ShootSFX(pos, unitObj.gameObject);
    }

    public void MultiShootSFX_Slave()
    {
        MultiUnit targetUnit = multiSpawn.TargetUnit();
        Vector2 pos = Camera.main.WorldToScreenPoint(targetUnit.transform.position);
        targetUnit.gameObject.SetActive(false);

        UnitObj unitObj = ObjectPool.Instance.SpawnFromPool(targetUnit.unitTag);
        unitObj.transform.position = targetUnit.transform.position;
        unitObj.transform.localScale = targetUnit.transform.localScale;
        slaveCutScene.ShootSFX(pos, unitObj.gameObject);
    }

    public void MainScene()
    {
        ServerMgr.instance.LeaveInGame();
        ObjectPool.Instance.ClearObj();
        multiSpawn.ClearMultiObj();
        MapManager.Instance.CloseMap();
        Time.timeScale = 1;
    }
}
