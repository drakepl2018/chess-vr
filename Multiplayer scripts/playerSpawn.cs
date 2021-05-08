// Author: Ridge Diffine
// this script handles spawning players into game in multiplayer


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class playerSpawn : MonoBehaviourPunCallbacks
{
    public GameObject playerPreFab;
    private GameObject Player;

    //when player joins room, it spawns that player a player object to represent the player physcially
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Player = PhotonNetwork.Instantiate(playerPreFab.name, transform.position, transform.rotation);
    }

    //deletes player when player leaves room
    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(Player);
    }
}
