using UnityEngine;
using UnityEngine.Networking;

using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class LobbyPlayer : NetworkBehaviour {

    bool done = false;
    public static bool matched = false;
    bool waitingAnswer = false;
    [SyncVar]
    bool accept = false;
    [SyncVar]
    bool refuse = false;
    // Use this for initialization
    GameObject holdsAnswer;
    string print1;
    float timeLeft;

    void Start () {
        print1 = "p1";
        holdsAnswer = GameObject.Find("PLAY");
        if (holdsAnswer==null)
        {
            Debug.Log("NoPlay");
        }
        if (GameObject.FindGameObjectsWithTag("lobby").Length == 2)
        {
            print1 = "p2";
            if(isLocalPlayer)
            CmdMatched();
        }
        Debug.Log(print1);

    }

    [Command]
    void CmdAskToEnable()
    {
        Debug.Log("Ws dame");

        RpcEnable();
    }

    [ClientRpc]
    void RpcEnable()
    {
        foreach (GameObject ok in GameObject.FindGameObjectsWithTag("player"))
        {
            Debug.Log("EnablesTaxa");

            ok.GetComponent<playerSetup>().enabled = true;
            ok.GetComponent<ActionAttack>().enabled = true;
            ok.GetComponent<ActionMove>().enabled = true;
            ok.GetComponent<SealCase>().enabled = true;
            ok.GetComponent<ActionMove>().enabled = true;
            ok.GetComponent<PathCreation>().enabled = true;
            ok.GetComponent<NetworkedTimer>().enabled = true;
        }
        Debug.Log("TelosTaxa");

    }
    IEnumerator inititializeStuff()
    {
        Debug.Log("Mpika");
        yield return new WaitUntil(() => done == true);
        Debug.Log("Vgika");
        CmdAskToEnable();
        //GetComponent<playerSetup>().enabled = true;
        //GetComponent<ActionAttack>().enabled = true;
        //GetComponent<ActionMove>().enabled = true;
        //GetComponent<SealCase>().enabled = true;
        //GetComponent<ActionMove>().enabled = true;
        //GetComponent<PathCreation>().enabled = true;
        //GetComponent<NetworkedTimer>().enabled = true;

    }
    // Update is called once per frame
    void Update () {
        if (!isLocalPlayer)
        {
            //Debug.Log(print);
            //waitingAnswer = false;
            return;
        }
        /*
        if (print1.Equals("p2ready"))
        {
            print1 = "not";
            StartCoroutine(inititializeStuff());

        }
        
        if (GameObject.Find("Nodes") != null) //&& GameObject.FindGameObjectsWithTag("player").Length == 2)
        {
            Debug.Log("An fanei touto gliora");

            done = true;
        }
        if (print1.Equals("not"))
            return;*/
        if (waitingAnswer&&timeLeft>0)
        {
            Debug.Log(print1);
            timeLeft -= Time.deltaTime;
            if (GameObject.Find("PLAY").GetComponent<indirect>().Accept)
            {
                Debug.Log(print1);
                timeLeft = 0;
                waitingAnswer = false;
                GetComponent<NetworkLobbyPlayer>().SendReadyToBeginMessage();
                    if(print1.Equals("p2"))
                        print1 = "p2ready";

                //CmdAccept(true);
                return;
            }
            if (GameObject.Find("PLAY").GetComponent<indirect>().Decline)
            {
                timeLeft = 0;
                waitingAnswer = false;
                //CmdAccept(false);
                GetComponent<NetworkLobbyPlayer>().RemovePlayer();

                return;
            }
            // waitingAnswer = false;
        }
        if (waitingAnswer&&timeLeft < 0)
        {
            timeLeft = 0;
            waitingAnswer = false;
            CmdAccept(false);
        }
	}



    [Command]
    void CmdAccept(bool accept)
    {
        this.accept = accept;
        refuse = !accept;
        bool nampoumeGame = true;
        foreach(GameObject ok in GameObject.FindGameObjectsWithTag("lobby"))
        {
            if (!ok.GetComponent<LobbyPlayer>().accept)
                nampoumeGame = false;
        }
        if (nampoumeGame)
        {
            foreach (GameObject ok in GameObject.FindGameObjectsWithTag("lobby"))
            {
                //ok.GetComponent<LobbyPlayer>().readyToBegin=true;
            }
        }

    }


    [Command]
    void CmdMatched()
    {
        foreach (GameObject ok in GameObject.FindGameObjectsWithTag("lobby"))
        {
            if (ok.GetComponent<LobbyPlayer>().print1.Equals("p1")) {
                ok.GetComponent<LobbyPlayer>().timeLeft = 15f;
                ok.GetComponent<LobbyPlayer>().waitingAnswer = true;
                Debug.Log("Mpika");

            }
            Debug.Log(print1);

        }
        RpcMatched();
    }

    [ClientRpc]
    void RpcIamReady()
    {
        
    }
    [ClientRpc]
    void RpcMatched()
    {
        matched = true;
        Debug.Log(print1);
        indirect.FindObject(GameObject.Find("Image"), "Accept").SetActive(true);
        indirect.FindObject(GameObject.Find("Image"), "Decline").SetActive(true);
        indirect.FindObject(GameObject.Find("Image"), "Searching").SetActive(false);
        indirect.FindObject(GameObject.Find("Image"), "SearchingText").SetActive(false);
        waitingAnswer = true;
        timeLeft = 15f;
    }
}
