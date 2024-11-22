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
    public int          round;
    public int          masterWin;
    public int          slaveWin;

    public void Start()
    {
        if (photonView.IsMine)
        {
            gameSeed = (int)(Time.time * 100f);
            round = 1;
        }
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //값을 날려주는 부분
            stream.SendNext(gameSeed);
            stream.SendNext((int)res);
            stream.SendNext(masterHp);
            stream.SendNext(slaveHp);
            stream.SendNext(round);
            stream.SendNext(masterWin);
            stream.SendNext(slaveWin);
        }
        else
        {
            //값을 가져오는 부분
            this.gameSeed = (int)stream.ReceiveNext();
            this.res = (GameResult)stream.ReceiveNext();
            this.masterHp = (int)stream.ReceiveNext();
            this.slaveHp = (int)stream.ReceiveNext();
            this.masterHp = (int)stream.ReceiveNext();
            this.round = (int)stream.ReceiveNext();
            this.masterWin = (int)stream.ReceiveNext();
            this.slaveWin = (int)stream.ReceiveNext();
        }
    }

    public bool IsMasterClient()
    {
        //내가 마스터 클라이언트인가?
        return PhotonNetwork.MasterClient.UserId == ServerMgr.userId;
    }
}
