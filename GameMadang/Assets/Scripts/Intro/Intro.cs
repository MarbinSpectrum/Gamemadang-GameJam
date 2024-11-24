using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Intro : MonoBehaviour
{
    [SerializeField] private List<Transform> pageObj = new List<Transform>();
    [SerializeField] private Transform basePos;
    [SerializeField] private Transform spawnPos;

    private IEnumerator pageAni = null;
    private int nowPage = 0;

    private void Start()
    {
        SetPage(nowPage);
    }

    private void SetPage(int n)
    {
        if(pageAni != null)
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
                for (int i = 0; i < pageObj.Count; i++)
                    pageObj[i].transform.position = spawnPos.transform.position;

                pageObj[n].transform.position = basePos.transform.position;
                pageObj[n].gameObject.SetActive(true);
            }
            else if (n <= 3)
            {
                for (int i = 0; i < pageObj.Count; i++)
                    pageObj[i].gameObject.SetActive(false);
                for (int i = 0; i < pageObj.Count; i++)
                    pageObj[i].transform.position = spawnPos.transform.position;

                pageObj[n].gameObject.SetActive(true);
                pageObj[n - 1].gameObject.SetActive(true);

                pageObj[n - 1].transform.position = basePos.transform.position;
                float dis = 0;
                do
                {
                    pageObj[n].transform.position = Vector3.Lerp(pageObj[n].transform.position, pageObj[n - 1].transform.position, 0.03f);
                    dis = Vector3.Distance(pageObj[n - 1].transform.position, pageObj[n].transform.position);
                    yield return null;
                } while (dis > 0.1f);
            }
            else if (n == 4)
            {
                pageObj[n - 1].gameObject.SetActive(true);
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
