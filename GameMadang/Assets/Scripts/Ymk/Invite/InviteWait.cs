using UnityEngine;

public class InviteWait : MonoBehaviour
{
    public void SetUI()
    {
        gameObject.SetActive(true);
    }

    public void CancelBtn()
    {
        gameObject.SetActive(false);
        ServerMgr.instance.PlayerInviteCancel();
    }
}
