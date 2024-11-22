using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeUI : MonoBehaviour
{
    public float time =0;
    private float curtime;
    [SerializeField] private Image timeGage;
    [SerializeField] private TextMeshProUGUI timeTxt;

    private void Awake()
    {
        curtime = time;
    }
    private void Update()
    {
        if (curtime <= 0) return;
        
        if (time > 0f)
        {
            timeTxt.text=CalTime(curtime);
            timeGage.fillAmount = (int)curtime/time;

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
