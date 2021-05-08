// Author: Ridge Diffine
// This script handles player movement 
//
//



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class MovePlayer : MonoBehaviour
{
    public SteamVR_Action_Vector2 moveValue;
    float maxSpeed = 1;
    float sensitivity = 1;

    private float speed = 0.0f;

    // Update is called once per frame
    void Update()
    {
            //if joy stick/ d-pad is pressed in certain direct will then update players trasnformation based on speed and sinsivity
            if (moveValue.axis.y > 0 || moveValue.axis.y < 0 || moveValue.axis.x > 0 || moveValue.axis.y < 0)
            {
                Vector3 direction = Player.instance.hmdTransform.TransformDirection(moveValue.axis.x , 0, moveValue.axis.y);
                speed = moveValue.axis.magnitude * sensitivity;
                speed = Mathf.Clamp(speed, 0, maxSpeed);
                transform.position += speed * Time.deltaTime * (direction);
                if(transform.position.y != .5f)
                {
                    transform.position = new Vector3(transform.position.x, .5f, transform.position.z);
                }
            }
        
    }
}
