using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;

public class InGameSync : MonoBehaviourPun, IPunObservable
{
    public int          gameSeed;
    public GameResult   res;
    public int          masterHp;
    public int          slaveHp;

    public void Start()
    {
        if (photonView.IsMine)
        {
            gameSeed = (int)(Time.time * 100f);
        }
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //���� �����ִ� �κ�
            stream.SendNext(gameSeed);
            stream.SendNext((int)res);
            stream.SendNext(masterHp);
            stream.SendNext(slaveHp);
        }
        else
        {
            //���� �������� �κ�
            this.gameSeed = (int)stream.ReceiveNext();
            this.res = (GameResult)stream.ReceiveNext();
            this.masterHp = (int)stream.ReceiveNext();
            this.slaveHp = (int)stream.ReceiveNext();

        }
    }

    public bool IsMasterClient()
    {
        //���� ������ Ŭ���̾�Ʈ�ΰ�?
        return PhotonNetwork.MasterClient.UserId == ServerMgr.userId;
    }
}
