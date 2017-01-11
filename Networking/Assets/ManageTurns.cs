using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class ManageTurns : NetworkBehaviour {

    [SyncVar]
    private GameObject player1,player2;
    private GameObject manager;
    [SyncVar]
    int i = 0;

    public GameObject Player1
    {
        get
        {
            return player1;
        }

        set
        {
            player1 = value;
        }
    }

    public GameObject Player2
    {
        get
        {
            return player2;
        }

        set
        {
            player2 = value;
        }
    }

    public int I
    {
        get
        {
            return i;
        }

        set
        {
            i = value;
        }
    }

    // Use this for initialization
    void Start () {
        manager = this.gameObject;
	}

    // Update is called once per frame
    void Update() {
        /*
        if (isServer) { 
        if (player1 == null || player2 == null)
            return;
            if (I == 0)
            {
                if (manager.GetComponent<CounterTimer>().Current == manager.GetComponent<CounterTimer>().roundTime) {
                    RpcwhenIequals1();
                    I++;
            }
        }
        else if (I == 1)
        {
                if (manager.GetComponent<CounterTimer>().Current == manager.GetComponent<CounterTimer>().roundTime)
                {
                    RpcwhenIequals1();
                    I++;
                }
        }
        else
        {
                if (manager.GetComponent<CounterTimer>().Current == manager.GetComponent<CounterTimer>().roundTime)
                {
                    RpcwhenIequals2();
                    I--;
                }
        }
        }*/
    }
    [ClientRpc]
    public void RpcwhenIequals1()
    {
        if (player1.GetComponent<playerSetup>().Id == 1)
        {
            GameObject.Find("WhoseTurn").GetComponent<Text>().text = "My Turn";
        }
        else
            GameObject.Find("WhoseTurn").GetComponent<Text>().text = "Op Turn";
    }
    [ClientRpc]
    public void RpcwhenIequals2()
    {
        if (player2.GetComponent<playerSetup>().Id == 2)
        {
            GameObject.Find("WhoseTurn").GetComponent<Text>().text = "My Turn";
        }
        else
            GameObject.Find("WhoseTurn").GetComponent<Text>().text = "Op Turn";
    }
}
