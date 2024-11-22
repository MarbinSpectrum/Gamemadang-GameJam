using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;

public class InGameSync : MonoBehaviourPunCallbacks
{
    private int GameSeed;
    public int gameSeed
    {
        get
        {
            return GameSeed;
        }
        set
        {
            GameSeed = value;

            ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
            hashtable.Add("gameSeed", gameSeed);

            Player[] player = PhotonNetwork.PlayerListOthers;
            foreach (Player p in player)
                p.SetCustomProperties(hashtable);
        }
    }
    private int Round;
    public int round
    {
        get
        {
            return Round;
        }
        set
        {
            Round = value;

            ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
            hashtable.Add("round", round);

            Player[] player = PhotonNetwork.PlayerListOthers;
            foreach (Player p in player)
                p.SetCustomProperties(hashtable);
        }
    }

    private GameResult Res;
    public GameResult res
    {
        get
        {
            return Res;
        }
        set
        {
            Res = value;

            ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
            hashtable.Add("res", res);

            Player[] player = PhotonNetwork.PlayerListOthers;
            foreach (Player p in player)
                p.SetCustomProperties(hashtable);
        }
    }

    private int MasterHp;
    public int masterHp
    {
        get
        {
            return MasterHp;
        }
        set
        {
            MasterHp = value;

            ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
            hashtable.Add("masterHp", masterHp);

            Player[] player = PhotonNetwork.PlayerListOthers;
            foreach (Player p in player)
                p.SetCustomProperties(hashtable);
        }
    }

    private int SlaveHp;
    public int slaveHp
    {
        get
        {
            return SlaveHp;
        }
        set
        {
            SlaveHp = value;

            ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
            hashtable.Add("slaveHp", slaveHp);

            Player[] player = PhotonNetwork.PlayerListOthers;
            foreach (Player p in player)
                p.SetCustomProperties(hashtable);
        }
    }

    private int MasterWin;
    public int masterWin
    {
        get
        {
            return MasterWin;
        }
        set
        {
            MasterWin = value;

            ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
            hashtable.Add("masterWin", masterWin);

            Player[] player = PhotonNetwork.PlayerListOthers;
            foreach (Player p in player)
                p.SetCustomProperties(hashtable);
        }
    }

    private int SlaveWin;
    public int slaveWin
    {
        get
        {
            return SlaveWin;
        }
        set
        {
            SlaveWin = value;

            ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
            hashtable.Add("slaveWin", slaveWin);

            Player[] player = PhotonNetwork.PlayerListOthers;
            foreach (Player p in player)
                p.SetCustomProperties(hashtable);
        }
    }

    public void Start()
    {
        if (photonView.IsMine)
        {
            gameSeed = (int)(Time.time * 100f);
            round = 1;
        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (targetPlayer == PhotonNetwork.LocalPlayer)
        {
            if (changedProps["gameSeed"] != null)
            {
                GameSeed = (int)changedProps["gameSeed"];

                string[] checkProperties = new string[] { "gameSeed" };
                PhotonNetwork.RemovePlayerCustomProperties(checkProperties);
            }
            else if (changedProps["round"] != null)
            {
                Round = (int)changedProps["round"];

                string[] checkProperties = new string[] { "round" };
                PhotonNetwork.RemovePlayerCustomProperties(checkProperties);
            }
            else if (changedProps["res"] != null)
            {
                Res = (GameResult)changedProps["res"];

                string[] checkProperties = new string[] { "res" };
                PhotonNetwork.RemovePlayerCustomProperties(checkProperties);
            }
            else if (changedProps["masterHp"] != null)
            {
                MasterHp = (int)changedProps["masterHp"];

                string[] checkProperties = new string[] { "masterHp" };
                PhotonNetwork.RemovePlayerCustomProperties(checkProperties);
            }
            else if (changedProps["slaveHp"] != null)
            {
                SlaveHp = (int)changedProps["slaveHp"];

                string[] checkProperties = new string[] { "slaveHp" };
                PhotonNetwork.RemovePlayerCustomProperties(checkProperties);
            }
            else if (changedProps["masterWin"] != null)
            {
                MasterWin = (int)changedProps["masterWin"];

                string[] checkProperties = new string[] { "masterWin" };
                PhotonNetwork.RemovePlayerCustomProperties(checkProperties);
            }
            else if (changedProps["slaveWin"] != null)
            {
                SlaveWin = (int)changedProps["slaveWin"];

                string[] checkProperties = new string[] { "slaveWin" };
                PhotonNetwork.RemovePlayerCustomProperties(checkProperties);
            }
        }
    }

    public bool IsMasterClient()
    {
        //내가 마스터 클라이언트인가?
        return PhotonNetwork.MasterClient.UserId == ServerMgr.userId;
    }
}
