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

    //�ʴ��� ���������
    private string recipientID;

    //�ʴ����� ����
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

                    //UID ����
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
        Debug.LogWarningFormat("�������� ������ ������. ���� : {0}", cause);
    }

    public override void OnJoinedRoom()
    {
        switch(action)
        {
            case ServerAction.Invite:
                {
                    //����������� ���� ���� ��������� ǥ��
                    //�������â ȣ��
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
                        //�����ε� ������ ����
                        //���� ���â ����

                        senderID = newPlayer.UserId;

                        InviteSystem.instance.Show_InviteCheck(senderID, () =>
                        {
                            ExitGames.Client.Photon.Hashtable hashtable
                              = new ExitGames.Client.Photon.Hashtable();
                            hashtable.Add("goInGame", true);
                            newPlayer.SetCustomProperties(hashtable);

                            UnityEngine.SceneManagement.SceneManager.LoadScene("TestMain");
                            action = ServerAction.None;

                            //OnPlayerPropertiesUpdate�� ������ �ð���
                        }, 
                        () =>
                        {
                            ExitGames.Client.Photon.Hashtable hashtable
                                = new ExitGames.Client.Photon.Hashtable();
                            hashtable.Add("isKicked", true);
                            newPlayer.SetCustomProperties(hashtable);

                            InviteSystem.instance.Close_InviteCheck();
                            action = ServerAction.None;

                            //OnPlayerPropertiesUpdate�� ������ �ð���
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
                                //�÷��̾ �̹� ������� ���̶�� ǥ��
                                Debug.Log("�ٸ� �÷��̾��� �ʴ��� ������ ������̶�� ǥ��");
                            }
                            break;
                        case 32764:
                            //GameDoesNotExist   
                            {
                                //uid�� �߸��Ǿ��ٰ� ǥ��
                                Debug.Log("�ش� uid�� ����");
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
                        //���� ���� â �ݱ�
                        InviteSystem.instance.Close_InviteCheck();
                        action = ServerAction.None;
                    }
                }
                break;
            case ServerAction.Invite:
                {
                    if (PhotonNetwork.IsMasterClient == false)
                    {
                        //���� ���â �ݱ�
                        InviteSystem.instance.Close_InviteWait();
                    }
                }
                break;
            case ServerAction.InGame:
                {
                    //�ٸ� �÷��̾ ����

                    //OnConnectedToMaster�Լ��� ������ �ð���
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

                                //OnConnectedToMaster�Լ��� ������ �ð���
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

        //OnConnectedToMaster�Լ��� ������ �ð���
    }

    public void PlayerInviteCancel()
    {
        //�ʴ��� ������ ���
        if (PhotonNetwork.InLobby)
            return;
        action = ServerAction.None;
        PhotonNetwork.LeaveRoom();
        loadBG.gameObject.SetActive(true);

        //OnConnectedToMaster�Լ��� ������ �ð���
    }


    private void GotoMyRoom()
    {
        //���� ������
        RoomOptions rm = new RoomOptions();
        rm.IsVisible = true;
        rm.IsOpen = true;
        rm.MaxPlayers = 2;
        rm.PublishUserId = true;

        PhotonNetwork.JoinOrCreateRoom(userId, rm, TypedLobby.Default);

        //OnJoinedRoom or OnJoinRoomFailed�Լ��� ������ �ð���
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
            //OnConnectedToMaster�Լ��� ������ �ð���
        }


    }

    public void SetInGame()
    {
        action = ServerAction.InGame;
    }
}
