// Author: Ridge Diffine
// This script is responsible for handling players interactions with chess pieces in game
//  (picking them up and setting them down on board), this script also communicates to chess engine
//  by changing names of pieces picked up to label them with appropriate square
//
//
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Photon.Pun;

public class PickUpHand : MonoBehaviour
{
    public float PickupDistance = .3f;
    bool handClosed = false;
    public LayerMask pickupLayer;
    public List<GameObject> indicators = new List<GameObject>();
    public int count = 0;
    
    public SteamVR_Input_Sources handSource = SteamVR_Input_Sources.RightHand;

    Rigidbody holdingTarget;
    Rigidbody lastTarget;
    GameObject temp;
    public bool target = false;

    private PhotonView photonView;

    // Update is called once per frame
   
    void FixedUpdate()
    {
        //checks if trigger is being held down
        if (SteamVR_Actions.default_Grab.GetState(handSource))
            handClosed = true;
        else
            handClosed = false;
        
        //if hand is opened (no piece is being held)
        if (!handClosed)
        {

            //checks if a peice was being held and is not now
            if(target == true)
            {
                count = 0;
                //destroys all highlighter squares
                foreach(GameObject Obj in indicators){
                    Destroy(Obj);
                }
                //gets a hold of active bord and view
                Board board = GameObject.Find("board").GetComponent<Chess>().board;
                View view = GameObject.Find("board").GetComponent<Chess>().view;
                Vector3 pos = lastTarget.transform.position;
                Square closest = new Square();
                float closestDis = 99999;
                float distance = 0;
                Vector3 dist;
                //finds closest square to piece
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        string col = "" + (char)(j + 65);
                        string ro = "" + (8 - i);
                        dist = GameObject.Find(col + ro).transform.position; //square
                        distance = Vector3.Distance(pos, dist);
                        if (closestDis > distance)
                        {
                            closestDis = distance;
                            closest = board.get("" + col + ro);
                        
                        }
                    }
                }
                //checks if move is legal
                string command = "" + lastTarget.gameObject.name[1] + lastTarget.gameObject.name[2];
                Square comingfrom = board.get(command);
                closest.print();
                comingfrom.piece.makeMove(closest, board);
                view.setPieces();
                board.print();
                Debug.Log("release");
                board = GameObject.Find("board").GetComponent<Chess>().board;
                if (board.promotion == true)
                {
                    Debug.Log("hey");
                    if(board.playerTurn() == "black")
                    {
                        //GameObject.Find("board").GetComponent<Chess>().RL.SetActive(true);
                        //GameObject.Find("board").GetComponent<Chess>().QL.SetActive(true);
                        //GameObject.Find("board").GetComponent<Chess>().BL.SetActive(true);
                        //GameObject.Find("board").GetComponent<Chess>().KL.SetActive(true);
                        
                    }
                    else if(board.playerTurn() == "white")
                    {
                        //GameObject.Find("board").GetComponent<Chess>().RB.SetActive(true);
                       //GameObject.Find("board").GetComponent<Chess>().QB.SetActive(true);
                        //GameObject.Find("board").GetComponent<Chess>().BB.SetActive(true);
                        //GameObject.Find("board").GetComponent<Chess>().KB.SetActive(true);
                    }
                   
                }
                target = false;
            }
            //checks for collider close enough to be picked up from hand
            Collider[] colliders = Physics.OverlapSphere(transform.position, PickupDistance, pickupLayer);
            if (colliders.Length > 0)
            {
                holdingTarget = colliders[0].transform.root.GetComponent<Rigidbody>();
                holdingTarget.constraints = ~RigidbodyConstraints.FreezePosition;
            }
            else
                holdingTarget = null;
        }
        //if hand is closed
        else
        {
            //if hand is closed and there is a target available
            if (holdingTarget)
            {
                target = true;
                lastTarget = holdingTarget;
                //ask for permission in multiplayer
                if(holdingTarget.GetComponent<PhotonView>().IsMine == false)
                {
                    photonView = holdingTarget.GetComponent<PhotonView>();
                    photonView.RequestOwnership();
                }
                //moves object to hand
                holdingTarget.velocity = (transform.position - holdingTarget.transform.position) / Time.fixedDeltaTime;
                if(count == 0){
                    Board board = GameObject.Find("board").GetComponent<Chess>().board;
                    string command = "" + lastTarget.gameObject.name[1] + lastTarget.gameObject.name[2];
                    Square comingfrom = board.get(command);
                    List<Square> moves = comingfrom.piece.legalMoves(board);
                    foreach(Square square in moves){
                        Vector3 pos = GameObject.Find("" + square.column + square.row).transform.position;
                        GameObject model = Instantiate(GameObject.Find("Indicator"), pos, Quaternion.identity);
                        indicators.Add(model);
                    }
                }
                count++;

                //Controls rotation of object in hand
                holdingTarget.maxAngularVelocity = 20;
                Quaternion deltaRot = transform.rotation * Quaternion.Inverse(holdingTarget.transform.rotation);
                Vector3 eulerRot = new Vector3(Mathf.DeltaAngle(0, deltaRot.eulerAngles.x), Mathf.DeltaAngle(0, deltaRot.eulerAngles.y), Mathf.DeltaAngle(0, deltaRot.eulerAngles.z));
                eulerRot *= .95f;
                eulerRot *= Mathf.Deg2Rad;
                holdingTarget.angularVelocity = eulerRot / Time.fixedDeltaTime;

            }
        }
    }


    


}
