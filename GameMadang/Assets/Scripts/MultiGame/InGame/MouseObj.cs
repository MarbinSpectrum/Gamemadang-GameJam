using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MouseObj : MonoBehaviourPunCallbacks
{
    [SerializeField] private SpriteRenderer sp;
    [SerializeField] private Sprite master;
    [SerializeField] private Sprite slave;
    [SerializeField] private Sprite myMouse;

    private void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            transform.position =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }

        if (PhotonNetwork.MasterClient.UserId == ServerMgr.userId)
        {
            if (photonView.IsMine)
                sp.sprite = master;
            else
                sp.sprite = slave;
        }
        else
        {
            if (photonView.IsMine)
                sp.sprite = slave;
            else
                sp.sprite = master;
        }
    }
}
