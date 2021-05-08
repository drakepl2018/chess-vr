// Author: Ridge diffine
// this script is used to handle player movements for mutiplayer, 
// keeps players current postions and rotations to send to network
// so players can see each other move around
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;


public class networkPlayer : MonoBehaviour
{
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;
    private PhotonView photonView;

    private Transform headRig;
    private Transform leftHandRig;
    private Transform rightHandRig;


    // this finds your active local player objects and deactivates them so you can't see them
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        GameObject rig = GameObject.Find("LibraryPlayer");
        headRig = rig.transform.Find("SteamVRObjects/VRCamera");
        leftHandRig = rig.transform.Find("SteamVRObjects/LeftHand/HoverPoint");
        rightHandRig = rig.transform.Find("SteamVRObjects/RightHand/HoverPoint");

        //this is used to deactivate player objects so you dont have two sets of objects
        if (photonView.IsMine)
        {
            rightHand.gameObject.SetActive(false);
            leftHand.gameObject.SetActive(false);
            head.gameObject.SetActive(false);
        }
    }

    // this is used for updating player postion to send to network (so players can see eachother move around)
    void Update()
    {
        if (photonView.IsMine)
        {
            MapPosition(head, headRig);
            MapPosition(leftHand, leftHandRig);
            MapPosition(rightHand, rightHandRig);
        }
    }

    //gets current map position
    void MapPosition(Transform Target, Transform rigTransform)
    {
        Target.position = rigTransform.position;
        Target.rotation = rigTransform.rotation;
    }
}
