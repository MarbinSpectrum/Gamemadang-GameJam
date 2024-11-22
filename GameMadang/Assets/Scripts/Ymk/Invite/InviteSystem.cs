using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class InviteSystem : MonoBehaviour
{
    public static InviteSystem instance;

    [SerializeField] private InputField     friendUID;
    [SerializeField] private InviteWait     inviteWait;
    [SerializeField] private InviteCheck    inviteCheck;
    [SerializeField] private Button         btn;

    private void Awake() => instance = this;

    public void InviteBtn()
    {
        if (friendUID.text == string.Empty)
            return;
        string fUID = friendUID.text;

        ServerMgr.instance.PlayerInvite(fUID);

        btn.enabled = false;
        Invoke("ActBtn", 3);
    }

    public void ActBtn() => btn.enabled = true;

    public void Show_InviteWait()
    {
        inviteWait.SetUI();
    }

    public void Close_InviteWait()
    {
        inviteWait.gameObject.SetActive(false);
    }

    public void Show_InviteCheck(string otherUid, NoParaDel pYesFun, NoParaDel pNoFun)
    {
        inviteCheck.SetUI(otherUid,pYesFun,pNoFun);
    }

    public void Close_InviteCheck()
    {
        inviteCheck.gameObject.SetActive(false);
    }
}
