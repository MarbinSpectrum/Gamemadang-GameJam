using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Intro : MonoBehaviour
{
    [SerializeField] private List<Transform> pageObj = new List<Transform>();
    [SerializeField] private Transform basePos;
    [SerializeField] private List<Transform> spawnPos;
    [SerializeField] private CanvasGroup titleImg;
    [SerializeField] private Animation titleAni;

    private IEnumerator pageAni = null;
    private int nowPage = 0;

    private void Start()
    {
        SoundMgr.Instance.PlayBGM(Sound.BGM_DarkFight);
        SetPage(nowPage);
    }

    private void SetPage(int n)
    {
        if (n > 4)
            return;

        if (pageAni != null)
        {
            StopCoroutine(pageAni);
            pageAni = null;
        }


        IEnumerator Run()
        {
            if (n == 0)
            {
                for (int i = 0; i < pageObj.Count; i++)
                    pageObj[i].gameObject.SetActive(false);

                pageObj[n].transform.position = basePos.transform.position;
                pageObj[n].gameObject.SetActive(true);
            }
            else if (n <= 3)
            {
                for (int i = 0; i < pageObj.Count; i++)
                    pageObj[i].gameObject.SetActive(false);

                pageObj[n].gameObject.SetActive(true);
                pageObj[n].transform.position = spawnPos[n - 1].transform.position;
                pageObj[n - 1].gameObject.SetActive(true);
                pageObj[n - 1].transform.position = basePos.transform.position;

                float dis = 0;
                do
                {
                    pageObj[n].transform.position = Vector3.Lerp(pageObj[n].transform.position, pageObj[n - 1].transform.position, 0.1f);
                    dis = Vector3.Distance(pageObj[n - 1].transform.position, pageObj[n].transform.position);
                    yield return null;
                } 
                while (dis > 0.1f);
            }
            else if (n == 4)
            {
                pageObj[n - 1].gameObject.SetActive(true);
                pageObj[n - 1].transform.position = basePos.transform.position;
                titleImg.transform.position = basePos.transform.position;

                float alphaValue = 0;
                do
                {
                    alphaValue = Mathf.Lerp(alphaValue, 1, 0.05f);
                    titleImg.alpha = alphaValue;
                    yield return null;
                } while (0.99f > alphaValue);
                alphaValue = 1f;
                titleImg.alpha = alphaValue;

                titleAni.Play();

                yield return new WaitForSeconds(1f);

                UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
            }
        }
        pageAni = Run();
        StartCoroutine(pageAni);

    }

    public void NextPage()
    {
        nowPage++;
        SetPage(nowPage);
    }
}
