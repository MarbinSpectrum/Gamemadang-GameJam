using TMPro;
using UnityEngine;

public class CutSceneEvent : MonoBehaviour
{
    //���� ���� �ִ°� ȿ����, ������, ��纯ȯ ����?

    private TextMeshProUGUI text;

    private void Awake()
    {
        text=transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }
    public void StartEvent()
    {
        Time.timeScale = 0f;
        RandomText();
    }
    public void EndEvent()
    {
        gameObject.SetActive(false);
    }



    private void RandomText()
    {
        text.text = "������ ��� �����";
    }

    
}
