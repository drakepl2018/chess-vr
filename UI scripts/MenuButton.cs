// Author: Ridge Diffine
// This script is the event handler system, it handles all MenuScreens and buttons.
// 
//
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using System;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{

    public float PickupDistance = .1f;
    bool pressed = false;
    public LayerMask press;
    public AudioSource click; 
    public SteamVR_Input_Sources handSource = SteamVR_Input_Sources.RightHand;

    public Material HoverColor;
    public Material DefaultColor;
    public Material PressedColor;
    public GameObject button;
    public GameObject currentMenu;
    public Camera camera1;
    public string difficulty;
    public GameObject sound;
    public string mode;

    public Board board;
    public View view;
    public GameObject Board;
    public bool player = false;
    public string color;
   

    void FixedUpdate()
    {
        //finds audio for button clicks and checks if player presses click on a menu screen
        click = GameObject.Find("Click").GetComponent<AudioSource>();
        if (SteamVR_Actions.default_press.GetState(handSource))
            pressed = true;
        else
            pressed = false;

        //if button is not clicked
        if (!pressed)
        {
            //finds button the pointer is hovering on and changes color
            Collider[] colliders = Physics.OverlapSphere(transform.position, PickupDistance, press);
            if (colliders.Length > 0)
            {
                //Debug.Log(colliders.Length);
                button = colliders[0].gameObject;
                //Debug.Log(button.name);
                button.GetComponent<Renderer>().material = HoverColor;
            }
            //if not pointing at button then sets all buttons back to oriningal color
            else
            {
                if (button)
                {
                    button.GetComponent<Renderer>().material = DefaultColor;
                }
                button = null;
            }
        }
        else
        {
            //if a button is pressed, changes color and plays click sound, and also performs whatever the button is suppose to do
            if (button)
            {
                click.Play();
                button.GetComponent<Renderer>().material = PressedColor;
                //if button is quickplay, pulls up correct next menu
                if(button.name == "QuickPlay")
                {
                    button = null;
                    mode = "QuickPlay";
                    if(GameObject.Find("Main Menu") != null)
                    {
                        currentMenu = GameObject.Find("Main Menu");
                    }
                    else if(GameObject.Find("Main Menu(Clone)") != null)
                    {
                        currentMenu = GameObject.Find("Main Menu(Clone)");
                    }
                    Destroy(currentMenu);
                    currentMenu = Resources.Load("SelectColor") as GameObject;
                    Instantiate(currentMenu, new Vector3(15,0,47), Quaternion.identity);
                    currentMenu.GetComponent<Canvas>().worldCamera = camera1;
                    currentMenu = null;
                    //Debug.Log("clicked quickPlay");
                }
                //if button is freeplay, pulls up correct next menu
                else if (button.name == "FreePlay")
                {
                    button = null;
                    mode = "FreePlay";
                    if (GameObject.Find("Main Menu") != null)
                    {
                        currentMenu = GameObject.Find("Main Menu");
                    }
                    else if (GameObject.Find("Main Menu(Clone)") != null)
                    {
                        currentMenu = GameObject.Find("Main Menu(Clone)");
                    }
                    Destroy(currentMenu);
                    currentMenu = Resources.Load("Map") as GameObject;
                    Instantiate(currentMenu, new Vector3(15, 0, 47), Quaternion.identity);
                    currentMenu.GetComponent<Canvas>().worldCamera = camera1;
                }
                //if button is multiplayer, pulls up correct next menu
                else if (button.name == "Multiplayer")
                {
                    button = null;
                    if (GameObject.Find("Main Menu") != null)
                    {
                        currentMenu = GameObject.Find("Main Menu");
                    }
                    else if (GameObject.Find("Main Menu(Clone)") != null)
                    {
                        currentMenu = GameObject.Find("Main Menu(Clone)");
                    }
                    Destroy(currentMenu);
                    currentMenu = Resources.Load("create_host") as GameObject;
                    Instantiate(currentMenu, new Vector3(15, 0, 47), Quaternion.identity);
                    currentMenu.GetComponent<Canvas>().worldCamera = camera1;
                    //Debug.Log("clicked quickPlay");
                }
                //if button is settings, pulls up correct next menu
                else if (button.name == "settings")
                {
                    button = null;
                    if (GameObject.Find("Main Menu") != null)
                    {
                        currentMenu = GameObject.Find("Main Menu");
                    }
                    else if (GameObject.Find("Main Menu(Clone)") != null)
                    {
                        currentMenu = GameObject.Find("Main Menu(Clone)");
                    }
                    Destroy(currentMenu);
                    currentMenu = Resources.Load("settings") as GameObject;
                    Instantiate(currentMenu, new Vector3(15, 0, 47), Quaternion.identity);
                    currentMenu.GetComponent<Canvas>().worldCamera = camera1;
                    //Debug.Log("clicked quickPlay");
                }
                //place holder
                else if(button.name == "Create")
                {
                    button = null;
                }
                else if (button.name == "Join")
                {
                    button = null;
                }
                //sets difficulty
                else if(button.name == "Easy")
                {
                    difficulty = "Easy";
                    button = null;
                    currentMenu = GameObject.Find("AI(Clone)");
                    Debug.Log(currentMenu.name);
                    Destroy(currentMenu);
                    currentMenu = Resources.Load("Map") as GameObject;
                    Instantiate(currentMenu, new Vector3(15, 0, 47), Quaternion.identity);
                    currentMenu.GetComponent<Canvas>().worldCamera = camera1;
                }
                else if(button.name == "Hard")
                {
                    difficulty = "Hard";
                    currentMenu = GameObject.Find("AI(Clone)");
                    button = null;
                    Destroy(currentMenu);
                    currentMenu = Resources.Load("Map") as GameObject;
                    Instantiate(currentMenu, new Vector3(15, 0, 47), Quaternion.identity);
                    currentMenu.GetComponent<Canvas>().worldCamera = camera1;
                }
                //loads correct scene based on what mode you are in
                else if(button.name == "Library")
                {
                    
                    button = null;
                    
                    //Debug.Log(difficulty);
                    if(mode == "FreePlay")
                    {
                        SceneManager.LoadScene("LibraryFreePlay", LoadSceneMode.Single);
                    }
                    else
                    {
                        SceneManager.LoadScene("LibraryWH", LoadSceneMode.Single);
                    }
                    
                    
                }
                //loads correct scene based on what mode you are in
                else if(button.name == "SpaceShip")
                {
                    
                    button = null;
                    
                    if (mode == "FreePlay")
                    {
                        SceneManager.LoadScene("SpaceShipFreePlay", LoadSceneMode.Single);
                    }
                    else
                    {
                        SceneManager.LoadScene("SpaceWH", LoadSceneMode.Single);
                    }
                }
                //used to return you to main menu
                else if(button.name == "Back")
                {
                    button = null;
                    if (GameObject.Find("AI(Clone)") != null)
                    {
                        currentMenu = GameObject.Find("AI(Clone)");
                    }
                    else if (GameObject.Find("Map(Clone)") != null)
                    {
                        currentMenu = GameObject.Find("Map(Clone)");
                    }
                    else if (GameObject.Find("settings(Clone)") != null)
                    {
                        currentMenu = GameObject.Find("settings(Clone)");
                    }
                    else if (GameObject.Find("create_host(Clone)") != null)
                    {
                        currentMenu = GameObject.Find("create_host(Clone)");
                    }
                    else if(GameObject.Find("SelectColor(Clone)") != null)
                    {
                        currentMenu = GameObject.Find("SelectColor(Clone)");
                    }
                    Destroy(currentMenu);
                    currentMenu = Resources.Load("Main Menu") as GameObject;
                    Instantiate(currentMenu, new Vector3(15, 0, 47), Quaternion.identity);
                    currentMenu.GetComponent<Canvas>().worldCamera = camera1;
                }
                //used to return you back to spawn
                else if(button.name == "BackToSpawn")
                {
                    button = null;
                    

                    SceneManager.LoadScene("SpawnRoom", LoadSceneMode.Single);
                    //Debug.Log("clicked BackToSpawn");


                }
                //controls music
                else if(button.name == "MusicOn")
                {
                    button = null;
                    if (GameObject.Find("music") != null)
                    {
                        sound = GameObject.Find("music");
                        Destroy(sound);
                    }
                    else if(GameObject.Find("music(Clone)") != null)
                    {
                        sound = GameObject.Find("music(Clone)");
                        Destroy(sound);
                    }
                    else if(GameObject.Find("musicSpace") != null)
                    {
                        sound = GameObject.Find("musicSpace");
                        Destroy(sound);
                    }
                    else if (GameObject.Find("musicSpace(Clone)") != null)
                    {
                        sound = GameObject.Find("musicSpace(Clone)");
                        Destroy(sound);
                    }
                    else
                    {
                        if(GameObject.Find("Library") != null)
                        {
                            sound = Instantiate(Resources.Load("music") as GameObject);
                        }
                        else
                        {
                            sound = Instantiate(Resources.Load("musicSpace") as GameObject);
                        }
                        
                    }
                }
                //also controls music for different menu
                else if(button.name == "MusicOn/Off")
                {
                    button = null;
                    if (GameObject.Find("GameObject") != null)
                    {
                        sound = GameObject.Find("GameObject");
                        Destroy(sound);
                    }
                    else if (GameObject.Find("GameObject(Clone)") != null)
                    {
                        sound = GameObject.Find("GameObject(Clone)");
                        Destroy(sound);
                    }
                    else
                    {
                        sound = Instantiate(Resources.Load("GameObject") as GameObject);
                    }
                    

                }
                //resets board ( replaced reset script)
                else if(button.name == "ResetBoard")
                {
                    button = null;
                    Board = GameObject.Find("board");
                    view = Board.GetComponent<Chess>().view;
                    view.model.newGame();
                    view.clearPieces();
                    view.setPieces();
                    view.model.turn = 1;
                }
                //switches sides( replaces older script)
                else if (button.name == "SwitchSides")
                {
                    if (player == false)
                    {
                        button = null;
                        Board = GameObject.Find("board");
                        if (GameObject.Find("Library") != null)
                        {
                            Board.transform.eulerAngles = new Vector3(Board.transform.eulerAngles.x, 180, 0);
                            Board.transform.position = new Vector3(4.3f, 1.15f, 32.9f);
                        }
                        else
                        {
                            Board.transform.eulerAngles = new Vector3(0, 270, 0);
                            Board.transform.position = new Vector3(-4.6f, 1.05f, -1.05f);
                        }  
                        view = Board.GetComponent<Chess>().view;
                        view.clearPieces();
                        //view.model.newGame();
                        view.setPieces();
                        player = true;
                    }
                    else if (player)
                    {
                        button = null;
                        Board = GameObject.Find("board");
                        if (GameObject.Find("Library") != null)
                        {
                            Board.transform.eulerAngles = new Vector3(Board.transform.eulerAngles.x, 0, 0);
                            Board.transform.position = new Vector3(3.836f, 1.161f, 32.427f);
                        }
                        else
                        {
                            Board.transform.eulerAngles = new Vector3(0, 90, 0);
                            Board.transform.position = new Vector3(-5.1f, 1.05f, -.55f);

                        }
                        view = Board.GetComponent<Chess>().view;
                        view.clearPieces();
                        //view.model.newGame();
                        view.setPieces();
                        player = false;
                    }
                }
                //place holder
                else if (button.name == "GameLogic")
                {
                    button = null;
                }
                //sets the color you want to play as
                else if (button.name == "White")
                {
                    button = null;
                    color = "White";
                    currentMenu = GameObject.Find("SelectColor(Clone)");
                    Debug.Log(currentMenu.name);
                    Destroy(currentMenu);
                    currentMenu = Resources.Load("AI") as GameObject;
                    Instantiate(currentMenu, new Vector3(15, 0, 47), Quaternion.identity);
                    currentMenu.GetComponent<Canvas>().worldCamera = camera1;
                }
                //sets the color you want to play as
                else if (button.name == "Black")
                {
                    button = null;
                    color = "Black";
                    currentMenu = GameObject.Find("SelectColor(Clone)");
                    Debug.Log(currentMenu.name);
                    Destroy(currentMenu);
                    currentMenu = Resources.Load("AI") as GameObject;
                    Instantiate(currentMenu, new Vector3(15, 0, 47), Quaternion.identity);
                    currentMenu.GetComponent<Canvas>().worldCamera = camera1;
                }
                //loads multiplayer maps
                else if (button.name == "MultiLibrary")
                {
                    SceneManager.LoadScene("LibraryMultiplayer", LoadSceneMode.Single);
                }
                else if (button.name == "MulitSpaceShip")
                {
                    SceneManager.LoadScene("SpaceMultiplayer", LoadSceneMode.Single);
                }
            }
        }
    }
}
