using TMPro;
using UnityEngine;

public class CutSceneEvent : MonoBehaviour
{
    //넣을 수도 있는거 효과음, 움직임, 대사변환 정도?

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
        text.text = "지금은 대사 없어요";
    }

    
}
