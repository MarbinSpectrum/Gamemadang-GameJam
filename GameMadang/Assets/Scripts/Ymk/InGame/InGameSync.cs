using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;

public class InGameSync : MonoBehaviourPunCallbacks
{
    public static InGameSync instance;
    private static bool init = false;

    private void Awake()
    {
        if (init == false)
        {
            init = true;

            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private Dictionary<string, int> gameState = new Dictionary<string, int>();

    public int gameSeed
    {
        get => GetState("gameSeed");
        set => UpdateState("gameSeed", value);
    }

    public GameResult res
    {
        get => (GameResult)GetState("res");
        set => UpdateState("res", value);
    }

    public int masterHp
    {
        get => GetState("masterHp");
        set => UpdateState("masterHp", value);
    }

    public int slaveHp
    {
        get => GetState("slaveHp");
        set => UpdateState("slaveHp", value);
    }

    public int masterWin
    {
        get => GetState("masterWin");
        set => UpdateState("masterWin", value);
    }

    public int slaveWin
    {
        get => GetState("slaveWin");
        set => UpdateState("slaveWin", value);
    }

    public void SetSeed()
    {
        if (IsMasterClient() && gameSeed == 0)
            gameSeed = (int)(System.DateTime.Now.Ticks);
    }

    private void Update()
    {
        Debug.Log(gameSeed);
    }

    private int GetState(string key)
    {
        if (gameState.ContainsKey(key))
            return gameState[key];
        return 0;
    }

    private void UpdateState(string key, object value)
    {
        if (gameState.ContainsKey(key))
            gameState[key] = (int)value;
        else
            gameState.Add(key, (int)value);

        var hashtable = new ExitGames.Client.Photon.Hashtable { { key, (int)value } };
        Player[] player = PhotonNetwork.PlayerListOthers;
        foreach (Player p in player)
            p.SetCustomProperties(hashtable);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        foreach (var key in changedProps.Keys)
        {
            string propertyName = key.ToString();
            if(changedProps[propertyName] != null && changedProps[propertyName] is int keyValue)
            {
                //키값 업데이트
                gameState[propertyName] = keyValue;
            }
        }
    }

    public bool IsMasterClient()
    {
        //내가 마스터 클라이언트인가?
        return PhotonNetwork.MasterClient.UserId == ServerMgr.userId;
    }
}
