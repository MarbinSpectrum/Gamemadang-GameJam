using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;

public class ServerMgr : MonoBehaviourPunCallbacks
{
    public static ServerMgr instance;
    private static bool init = false;
    public static string userId = "";

    [SerializeField] private GameObject loadBG;

    public ServerAction action { private set; get; } = ServerAction.Init;

    //초대장 보냈을경우
    private string recipientID;

    //초대장을 받음
    private string senderID;

    private void Awake()
    {
        if(init == false)
        {
            init = true;

            instance = this;
            DontDestroyOnLoad(instance);

            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public override void OnConnectedToMaster()
    {
        switch(action)
        {
            case ServerAction.Init:
                {
                    action = ServerAction.None;

                    //UID 세팅
                    userId = PhotonNetwork.AuthValues.UserId;
                    GotoMyRoom();
                }
                break;
            case ServerAction.Invite:
                {
                    PhotonNetwork.JoinRoom(recipientID);
                    loadBG.gameObject.SetActive(true);
                }
                break;
            case ServerAction.None:
                {
                    GotoMyRoom();
                }
                break;
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("서버와의 연결이 끊어짐. 사유 : {0}", cause);
    }

    public override void OnJoinedRoom()
    {
        switch(action)
        {
            case ServerAction.Invite:
                {
                    //여기까지오면 게임 수락 대기중으로 표시
                    //수락대기창 호출
                    InviteSystem.instance.Show_InviteWait();
                    loadBG.gameObject.SetActive(false);
                }
                break;
            case ServerAction.None:
                {
                    loadBG.gameObject.SetActive(false);
                }
                break;
        }
    }


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        switch(action)
        {
            case ServerAction.None:
                {
                    action = ServerAction.InviteCheck;

                    if (PhotonNetwork.IsMasterClient)
                    {
                        //방장인데 누군가 들어옴
                        //수락 대기창 열기

                        senderID = newPlayer.UserId;

                        InviteSystem.instance.Show_InviteCheck(senderID, () =>
                        {
                            ExitGames.Client.Photon.Hashtable hashtable
                              = new ExitGames.Client.Photon.Hashtable();
                            hashtable.Add("goInGame", true);
                            newPlayer.SetCustomProperties(hashtable);

                            UnityEngine.SceneManagement.SceneManager.LoadScene("TestMain");
                            action = ServerAction.None;

                            //OnPlayerPropertiesUpdate로 응답이 올거임
                        }, 
                        () =>
                        {
                            ExitGames.Client.Photon.Hashtable hashtable
                                = new ExitGames.Client.Photon.Hashtable();
                            hashtable.Add("isKicked", true);
                            newPlayer.SetCustomProperties(hashtable);

                            InviteSystem.instance.Close_InviteCheck();
                            action = ServerAction.None;

                            //OnPlayerPropertiesUpdate로 응답이 올거임
                        });
                    }
                }
                break;
        }
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        switch(action)
        {
            case ServerAction.Invite:
                {
                    action = ServerAction.None;
                    switch (returnCode)
                    {
                        case 32765:
                            //GameFull 
                            {
                                //플레이어가 이미 수락대기 중이라고 표시
                                Debug.Log("다른 플레이어의 초대의 수락을 대기중이라고 표시");
                            }
                            break;
                        case 32764:
                            //GameDoesNotExist   
                            {
                                //uid가 잘못되었다고 표시
                                Debug.Log("해당 uid는 없다");
                            }
                            break;
                    }

                    loadBG.gameObject.SetActive(false);
                    GotoMyRoom();
                }
                break;
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        switch(action)
        {
            case ServerAction.InviteCheck:
                {
                    if (PhotonNetwork.IsMasterClient)
                    {
                        //수락 결정 창 닫기
                        InviteSystem.instance.Close_InviteCheck();
                        action = ServerAction.None;
                    }
                }
                break;
            case ServerAction.Invite:
                {
                    if (PhotonNetwork.IsMasterClient == false)
                    {
                        //수락 대기창 닫기
                        InviteSystem.instance.Close_InviteWait();
                    }
                }
                break;
            case ServerAction.InGame:
                {
                    //다른 플레이어가 나감

                    //OnConnectedToMaster함수로 응답이 올거임
                }
                break;
        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        switch(action)
        {
            case ServerAction.Invite:
                {
                    if (targetPlayer == PhotonNetwork.LocalPlayer)
                    {
                        if (changedProps["isKicked"] != null)
                        {
                            if ((bool)changedProps["isKicked"])
                            {
                                action = ServerAction.None;

                                string[] _removeProperties = new string[1];
                                _removeProperties[0] = "isKicked";
                                PhotonNetwork.RemovePlayerCustomProperties(_removeProperties);
                                PhotonNetwork.LeaveRoom();

                                InviteSystem.instance.Close_InviteWait();

                                //OnConnectedToMaster함수로 응답이 올거임
                            }
                        }
                        else if (changedProps["goInGame"] != null)
                        {
                            if ((bool)changedProps["goInGame"])
                            {
                                action = ServerAction.None;

                                string[] _removeProperties = new string[1];
                                _removeProperties[0] = "goInGame";
                                PhotonNetwork.RemovePlayerCustomProperties(_removeProperties);
                                InviteSystem.instance.Close_InviteWait();

                                UnityEngine.SceneManagement.SceneManager.LoadScene("TestMain");
                            }
                        }
                    }
                }
                break;
        }
    }

    public void PlayerInvite(string id)
    {
        if (PhotonNetwork.InLobby)
            return;
        action = ServerAction.Invite;
        recipientID = id;
        PhotonNetwork.LeaveRoom();
        loadBG.gameObject.SetActive(true);

        //OnConnectedToMaster함수로 응답이 올거임
    }

    public void PlayerInviteCancel()
    {
        //초대장 보내기 취소
        if (PhotonNetwork.InLobby)
            return;
        action = ServerAction.None;
        PhotonNetwork.LeaveRoom();
        loadBG.gameObject.SetActive(true);

        //OnConnectedToMaster함수로 응답이 올거임
    }


    private void GotoMyRoom()
    {
        //본인 방입장
        RoomOptions rm = new RoomOptions();
        rm.IsVisible = true;
        rm.IsOpen = true;
        rm.MaxPlayers = 2;
        rm.PublishUserId = true;

        PhotonNetwork.JoinOrCreateRoom(userId, rm, TypedLobby.Default);

        //OnJoinedRoom or OnJoinRoomFailed함수로 응답이 올거임
    }

    public void LeaveInGame()
    {
        action = ServerAction.None;
        if(PhotonNetwork.IsMasterClient)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Lobby");
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Lobby");
            PhotonNetwork.LeaveRoom();
            loadBG.gameObject.SetActive(true);
            //OnConnectedToMaster함수로 응답이 올거임
        }


    }

    public void SetInGame()
    {
        action = ServerAction.InGame;
    }
}
