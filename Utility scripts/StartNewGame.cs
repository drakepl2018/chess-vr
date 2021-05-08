// Author: Ridge Diffine
// This script is used to reset game, and only works with hard coded buttons so it was just used to test, and 
// is now implemented inside MenuButton.cs
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartNewGame : MonoBehaviour
{
    public bool pressed = false;
    public Board board;
    public View view;
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
        if (pressed)
        {
            Board = GameObject.Find("board");
            view = Board.GetComponent<Chess>().view;
            view.model.newGame();
            view.clearPieces();
            view.setPieces();
            pressed = false;
        }
    }
}
