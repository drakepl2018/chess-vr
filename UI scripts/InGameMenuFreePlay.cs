// Author: Ridge Diffine
// This script is used to control opening and closing menus screens while in the library map 
// when playing in free mode.
//
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using System;
using UnityEngine.SceneManagement;

public class InGameMenuFreePlay : MonoBehaviour
{
    public SteamVR_Input_Sources handSource = SteamVR_Input_Sources.RightHand;
    public GameObject hand;
    public GameObject menu;
    public GameObject player;
    public GameObject Pointer;
    public Camera camera1;
    GameObject currentMenu;
    GameObject currentPointer;


    void FixedUpdate()
    {
        //checks for if menu button is pressed
        if (SteamVR_Actions.default_menu.GetState(handSource) == true)
        {
            //creates menu and pointer
            if (GameObject.Find("InGameMenuFreePlay(Clone)") == null)
            {
                currentMenu = Instantiate(menu, new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z + 4), Quaternion.identity);
                currentMenu.transform.parent = player.transform;
                currentMenu.GetComponent<Canvas>().worldCamera = camera1;
                currentPointer = Instantiate(Pointer, new Vector3(hand.transform.position.x, hand.transform.position.y, hand.transform.position.z), Quaternion.identity);
                currentPointer.transform.parent = hand.transform;
            }

        }
        //if not pressed deletes menu and pointer
        else
        {
            if (GameObject.Find("Pointer(Clone)") != null)
            {
                Destroy(currentPointer);
            }
            if (GameObject.Find("InGameMenuFreePlay(Clone)") != null)
            {
                Destroy(currentMenu);
            }
        }

    }
}
