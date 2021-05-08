// Author: Ridge Diffine
// This script is used to switch sides of board (White -> black) (Black -> white),
// this script only works with hard coded buttons so it was just used to test, and 
// is now implemented inside MenuButton.cs
//


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSides : MonoBehaviour
{
    public bool pressed = false;
    public Board board;
    public View view;
    public bool player = false;
    public GameObject Board;
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y == 1.15f)
        {
            transform.position = new Vector3(transform.position.x, 1.2f, transform.position.z);
            pressed = true;
            Debug.Log(pressed);
        }
        if (pressed && player == false)
        {
            Board = GameObject.Find("board");
            Board.transform.eulerAngles = new Vector3(Board.transform.eulerAngles.x, 180, transform.eulerAngles.z);
            Board.transform.position = new Vector3(4.3f, 1.15f, 32.9f);
            view = Board.GetComponent<Chess>().view;
            view.clearPieces();
            //view.model.newGame();
            view.setPieces();
            pressed = false;
            player = true;
        }
        else if(pressed && player){
            Board = GameObject.Find("board");
            Board.transform.eulerAngles = new Vector3(Board.transform.eulerAngles.x, 0, transform.eulerAngles.z);
            Board.transform.position = new Vector3(3.836f, 1.161f, 32.427f);
            view = Board.GetComponent<Chess>().view;
            view.clearPieces();
            //view.model.newGame();
            view.setPieces();
            pressed = false;
            player = false;
        }
    }
}

