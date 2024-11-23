using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MultiStart : MonoBehaviour
{
    private void Start()
    {
        ServerMgr.instance.SetInGame();
        PhotonNetwork.Instantiate("Mouse", Vector3.zero, Quaternion.identity);

    }
}
