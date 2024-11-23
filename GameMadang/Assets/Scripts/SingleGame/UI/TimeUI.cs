using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TimeUI : MonoBehaviour
{
    public float time =0;
    private float curtime;
    public bool timeOver;
    [SerializeField] private Image timeGage;
    [SerializeField] private TextMeshProUGUI timeTxt;

    WaitForSeconds wait;

    private void Start()
    {
        curtime = time;
        wait= new WaitForSeconds(1f);
    }

    public void InitTime()
    {
        curtime = time;
    }


    private void Update()
    {
        if (curtime <= 0)
        {
            if (!timeOver) timeOver = true;
            return;
        }

        if (time > 0f)
        {
            timeTxt.text = CalTime(curtime);
            timeGage.fillAmount = (int)curtime / time;
            curtime -= Time.deltaTime;
        }
    }

    private string CalTime(float _curTime)
    {
        string str ="";
        int minute;
        int sec;

        minute = (int)curtime / 60;
        sec = (int)curtime % 60;

        str = string.Format("{0:D2} : {1:D2}",minute , sec);

        return str;
    }

}
