using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int ClearStage=1;
    public int curStage;//현재 들어온 스테이지
    public GameObject curMap=null;


    public Action OnLife;
    public Action OnScore;


    Coroutine coroutine;
    protected override void Awake()
    {
        base.Awake();
    }
    public void UpdateLife()
    {
        OnLife?.Invoke();
    }
    public void UpdateScore()
    {
        OnScore?.Invoke();
    }

    public void GameStart(TextMeshProUGUI text)
    {
        curMap = MapManager.Instance.GetMap(curStage);//시작전 맵 활성화

        Time.timeScale = 0f;
        curMap.SetActive(true);
        StartCoroutine(CountDown(text));
    }

    IEnumerator CountDown(TextMeshProUGUI text)
    {
        for(int i= 3; i>0; i--)
        {
            text.text = $"{i}";
            yield return new WaitForSecondsRealtime(1f);
        }

        text.transform.parent.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void EnterStage(int num)
    {
        curStage = num;
    }

    public bool CompareStage()
    {
        if(curStage==ClearStage) return true;
        return false;
    }

}
