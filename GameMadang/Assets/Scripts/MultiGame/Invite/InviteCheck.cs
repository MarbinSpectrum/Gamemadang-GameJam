using UnityEngine;

public class InviteCheck : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI label;

    private NoParaDel noFun;
    private NoParaDel yesFun;

    public void SetUI(string otherUid, NoParaDel pYesFun, NoParaDel pNoFun)
    {
        gameObject.SetActive(true);
        label.text = "UID '" + otherUid + "'����\n����� ��û �ϼ̽��ϴ�.";
        noFun = pNoFun;
        yesFun = pYesFun;
    }

    public void NoBtn()
    {
        gameObject.SetActive(false);
        noFun?.Invoke();
        noFun = null;
    }

    public void yesBtn()
    {
        gameObject.SetActive(false);
        yesFun?.Invoke();
        yesFun = null;
    }
}
