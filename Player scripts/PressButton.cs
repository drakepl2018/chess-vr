// Author: Ridge Diffine
// script used for pressing physcial buttons to test the switch sides and reset game mechanics,
// ends up being replaced by the MenuButton.cs
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using System;

public class PressButton : MonoBehaviour
{

    public float PickupDistance = .3f;
    bool pressed = false;
    public LayerMask press;

    public SteamVR_Input_Sources handSource = SteamVR_Input_Sources.RightHand;

    Rigidbody button;
    

    IEnumerator pressAfterDelay(float time)
    {
        yield return new WaitForSeconds(time);
        button.transform.position = new Vector3(button.position.x, 1.2f, button.position.z);
    }
    void Update()
    {
        if (SteamVR_Actions.default_press.GetState(handSource))
            pressed = true;
        else
            pressed = false;


        if (!pressed)
        {
            
            Collider[] colliders = Physics.OverlapSphere(transform.position, PickupDistance, press);
            if (colliders.Length > 0)
            {
                button = colliders[0].transform.root.GetComponent<Rigidbody>();
            }
            else
                button = null;
        }
        else
        {
            if (button)
            {
                button.transform.position = new Vector3(button.position.x, 1.15f, button.position.z);
                //StartCoroutine(pressAfterDelay(.5f));
            }
        }
    }
}

