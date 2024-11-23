using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int ClearStage=1;
    public int curStage;//현재 들어온 스테이지

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
        Time.timeScale = 0f;
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
        return false;
    }

}
