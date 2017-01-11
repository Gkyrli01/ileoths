using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class playerTimer : NetworkBehaviour {


    [SyncVar]
    private bool secondPlayer = false;
    public GameObject endButton;
    [SyncVar(hook = "OnTimeChange")]
    float timeLeft;
     Text Turn;
     Text TurnTime;
    [SyncVar]
    private int start;
    [SyncVar]
    bool player;
    bool doItOnce = true;
    bool notInit = false;
    public bool Player
    {
        get
        {
            return player;
        }

        set
        {
            player = value;
        }
    }

    public bool SecondPlayer
    {
        get
        {
            return secondPlayer;
        }

        set
        {
            secondPlayer = value;
        }
    }

    // Use this for initialization
    void Start () {

        if (GameObject.Find("WhoseTurn") == null || GameObject.Find("TurnTimer") == null) {
            notInit = false; 
            return;
        }
        Turn = GameObject.Find("WhoseTurn").GetComponent<Text>();
        TurnTime= GameObject.Find("TurnTimer").GetComponent<Text>();
        if (isServer)
        {
            timeLeft = 30f;
            Player = true;
        }
        if (start == 0)
        {

            Turn.text = "MyTurn";
            start = 1;
            endButton.GetComponent<Button>().enabled = true;
        }
        else
        {
            Turn.text = "OpTurn";
            endButton.GetComponent<Button>().enabled = false;
        }

    }
	
	// Update is called once per frame
	void Update () {
        if (!notInit)
        {
            Turn = GameObject.Find("WhoseTurn").GetComponent<Text>();
            TurnTime = GameObject.Find("TurnTimer").GetComponent<Text>();
            notInit = true;

            if (isServer)
            {

                Turn.text = "MyTurn";
                start = 1;
                endButton.GetComponent<Button>().enabled = true;
            }
            else
            {
                Turn.text = "OpTurn";
                endButton.GetComponent<Button>().enabled = false;
            }
        }
        if (isServer) {

            if (secondPlayer)
            {
                timeLeft -= Time.deltaTime;
                if (doItOnce&&Client.Id1==1)
                {
                    GameObject.FindGameObjectWithTag("player1").GetComponent<playerSetup>().freeze();
                    doItOnce = !doItOnce;
                }
            }
        if (timeLeft < 0)
        {
                CmdendTurn();
                
            }
        }

    }
    
     public void CmdendTurn()
    {
        timeLeft = 30f;
        RpconEndTurn();
        player = !player;
    }
    [ClientRpc]
    void RpconEndTurn()
    {   
        if (Turn.text.Equals("MyTurn"))
        {
            Turn.text = "OpTurn";
            endButton.GetComponent<Button>().enabled = false;
        }
        else { 
            Turn.text = "MyTurn";
            endButton.GetComponent<Button>().enabled = true;

        }

       foreach(GameObject ok in GameObject.FindGameObjectsWithTag("player1")){
            ok.GetComponent<playerSetup>().justChanged=true;
        }
        foreach (GameObject ok in GameObject.FindGameObjectsWithTag("player2"))
        {
            ok.GetComponent<playerSetup>().justChanged = true;
        }


    }

    void OnTimeChange(float timeLeft)
    {
        if(TurnTime!=null)
        TurnTime.text = (int)timeLeft + "s";
    }

}
