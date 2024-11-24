using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StageBtn : MonoBehaviour
{
    public int num;

    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() =>GameManager.Instance.EnterStage(num));
        transform.transform.localScale = Vector3.one * 1.21f;
        transform.DOScale(1f, 0.3f).SetEase(Ease.OutQuad);
    }
 

}
