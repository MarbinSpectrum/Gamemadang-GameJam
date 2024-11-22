using UnityEngine;

public class UId_UI : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Text uidText;

    private bool flag = false;

    private void Update()
    {
        if (flag)
            return;
        if (ServerMgr.userId == string.Empty)
            return;

        flag = true;
        uidText.text = string.Format("UID : {0}", ServerMgr.userId);

    }

    public void CopyUID()
    {
        GUIUtility.systemCopyBuffer = ServerMgr.userId;
    }
}
