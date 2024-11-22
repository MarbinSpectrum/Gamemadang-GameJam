using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TimeUI : MonoBehaviour
{
    public float time =0;
    private float curtime;

    [SerializeField] private Image timeGage;
    [SerializeField] private TextMeshProUGUI timeTxt;

    WaitForSeconds wait;

    private void Start()
    {
        curtime = time;
        wait= new WaitForSeconds(1f);
       // StartCountDown();
    }

    //public void StartCountDown()
    //{
    //    StartCoroutine(CountDown());
    //}
    //private IEnumerator CountDown()
    //{
    //    while (true)
    //    {
    //        curtime -= 1f;
    //        timeTxt.text = CalTime(curtime);

    //        timeGage.fillAmount = (int)curtime / time;

    //        if (curtime<=0)
    //        {
    //            break;
    //        }
    //        yield return wait;
    //    }
    //}

    private void Update()
    {
        if (curtime <= 0) return;

        if (time > 0f)
        {
            timeGage.fillAmount = curtime / time;
            timeTxt.text = CalTime(curtime);
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
