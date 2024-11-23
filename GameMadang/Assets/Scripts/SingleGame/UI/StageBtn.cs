using UnityEngine;
using UnityEngine.UI;

public class StageBtn : MonoBehaviour
{
    public int num;

    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() =>GameManager.Instance.EnterStage(num));
    }

}
