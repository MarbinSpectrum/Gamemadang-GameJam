using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;

public class InGameSync : MonoBehaviourPunCallbacks
{
    private Dictionary<string, object> gameState = new Dictionary<string, object>();

    public int gameSeed
    {
        get => GetState<int>("gameSeed");
        set => UpdateState("gameSeed", value);
    }

    public int round
    {
        get => GetState<int>("gameSeed");
        set => UpdateState("round", value);
    }

    public GameResult res
    {
        get => GetState<GameResult>("res");
        set => UpdateState("res", value);
    }

    public int masterHp
    {
        get => GetState<int>("masterHp");
        set => UpdateState("masterHp", value);
    }

    public int slaveHp
    {
        get => GetState<int>("slaveHp");
        set => UpdateState("slaveHp", value);
    }

    public int masterWin
    {
        get => GetState<int>("masterWin");
        set => UpdateState("masterWin", value);
    }

    public int slaveWin
    {
        get => GetState<int>("slaveWin");
        set => UpdateState("slaveWin", value);
    }

    public void Start()
    {
        if (IsMasterClient())
        {
            gameSeed = (int)(Time.time * 100f);
            round = 1;
        }
    }

    private T GetState<T>(string key)
    {
        if (gameState.TryGetValue(key, out var value) && value is T castValue)
            return castValue;
        return default;
    }

    private void UpdateState(string key, object value)
    {
        if (gameState.ContainsKey(key))
            gameState[key] = value;
        else
            gameState.Add(key, value);

        var hashtable = new ExitGames.Client.Photon.Hashtable { { key, value } };
        Player[] player = PhotonNetwork.PlayerListOthers;
        foreach (Player p in player)
            p.SetCustomProperties(hashtable);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        foreach (var key in changedProps.Keys)
        {
            if (key is string propertyName && changedProps[propertyName] != null)
            {
                //키값 업데이트
                gameState[propertyName] = changedProps[propertyName];
            }
        }
    }

    public bool IsMasterClient()
    {
        //내가 마스터 클라이언트인가?
        return PhotonNetwork.MasterClient.UserId == ServerMgr.userId;
    }
}
