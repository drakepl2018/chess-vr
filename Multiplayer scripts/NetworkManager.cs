// Author: Ridge Diffine
// This script controls the multiplayer networking, it is responsible for 
// connecting to server, and creating/joing rooms on network
//
//


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        ConnectToServer();
    }

    // attempts to connect client to Photon server
    void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("trying to connect to server...");
    }


    //if connected to server, will create/join room with the options below
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to server");
        base.OnConnectedToMaster();
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 3;
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;
        PhotonNetwork.JoinOrCreateRoom("Room1", roomOptions, TypedLobby.Default);
    }

    //if sucessfully joined to room
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined a Room");
        base.OnJoinedRoom();
    }

    //if another player joins the room you are in
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("new player joined room");
        base.OnPlayerEnteredRoom(newPlayer);
    }

}
