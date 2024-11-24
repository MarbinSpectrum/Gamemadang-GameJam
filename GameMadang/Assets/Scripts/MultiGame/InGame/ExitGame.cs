using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ExitGame : MonoBehaviourPunCallbacks
{
    public void GameExit()
    {
        ServerMgr.instance.LeaveInGame();
    }
}
