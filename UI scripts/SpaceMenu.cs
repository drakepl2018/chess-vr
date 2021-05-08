// Author: Ridge Diffine
// This script is used to control opening and closing menus screens while in the space map 
// when playing aginst AI.
//
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using System;
using UnityEngine.SceneManagement;

public class SpaceMenu : MonoBehaviour
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
        //gets buttons state (true or false)
        if (SteamVR_Actions.default_menu.GetState(handSource) == true)
        {
            //checks if menu is open or not
            if (GameObject.Find("InGameMenu(Clone)") == null)
            {
                currentMenu = Instantiate(menu, new Vector3(player.transform.position.x + 4, player.transform.position.y, player.transform.position.z), Quaternion.Euler(0,90,0));
                currentMenu.transform.parent = player.transform;
                currentMenu.GetComponent<Canvas>().worldCamera = camera1;
                currentPointer = Instantiate(Pointer, new Vector3(hand.transform.position.x, hand.transform.position.y, hand.transform.position.z), Quaternion.Euler(0,90,0));
                currentPointer.transform.parent = hand.transform;
            }

        }
        else
        {
            if (GameObject.Find("Pointer(Clone)") != null)
            {
                Destroy(currentPointer);
            }
            if (GameObject.Find("InGameMenu(Clone)") != null)
            {
                Destroy(currentMenu);
            }
        }

    }
}
