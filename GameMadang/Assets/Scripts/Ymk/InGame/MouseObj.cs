using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MouseObj : MonoBehaviourPunCallbacks
{
    [SerializeField] private SpriteRenderer sp;

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
                sp.color = Color.red;
            else
                sp.color = Color.blue;
        }
        else
        {
            if (photonView.IsMine)
                sp.color = Color.blue;
            else
                sp.color = Color.red;
        }
    }
}
