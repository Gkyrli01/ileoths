using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class initGame : NetworkBehaviour {
   

    public bool done = false;
    bool ready = false;

    public bool Ready
    {
        get
        {
            return ready;
        }

        set
        {
            ready = value;
        }
    }

    // Use this for initialization
    void Start () {

        StartCoroutine(inititializeStuff());
    }


    IEnumerator inititializeStuff()
    {
        Debug.Log("Mpika");
        yield return new WaitUntil(() => done==true);
        Debug.Log("Vgika");
        GetComponent<playerSetup>().enabled = true;
        GetComponent<ActionAttack>().enabled = true;
        GetComponent<ActionMove>().enabled = true;
        GetComponent<SealCase>().enabled = true;
        GetComponent<ActionMove>().enabled = true;
        GetComponent<PathCreation>().enabled = true;
        GetComponent<NetworkedTimer>().enabled = true;

    }

	// Update is called once per frame
	void Update () {
        /*
        if (!isServer)
            return;
            */

        if (!done&&GameObject.Find("Nodes") != null)
        {
            Debug.Log("An fanei touto gliora");

            done = true;
        }
            //first = false;

            //NetworkClient.Instance.Ready();
            //ClientScene.Ready(NetworkManager.singleton.client.connection);
           // CmduavOff();



        
        /*  if(first&&isServer&& GameObject.FindGameObjectsWithTag("player").Length == 2)
          {
              uav = false;
              NetworkManager.singleton.ServerChangeScene("test2");
              first = false;
              sceneChange = true;
          }

          if (isLocalPlayer)
          {


          }*/
        /*
        if (isLocalPlayer&&first)
            if (GameObject.FindGameObjectsWithTag("player").Length == 2) { 
                //CmdPlayerOnline();
                first = false;
                //soCallMeMaybe();
            }
        if (uav&&!done&&isLocalPlayer)
        {
         
            if(GameObject.Find("Counter")!=null&& GameObject.Find("Nodes") != null)
            {
                Ready = true;
                uav = false;
                done = true;
            }
            */

        /*
        NetworkManager.singleton.ServerChangeScene("test2");

        GetComponent<playerSetup>().enabled = true;
        GetComponent<ActionAttack>().enabled = true;
        GetComponent<ActionMove>().enabled = true;
        GetComponent<SealCase>().enabled = true;
        GetComponent<ActionMove>().enabled = true;
        GetComponent<PathCreation>().enabled = true;
        GetComponent<NetworkedTimer>().enabled = true;
    */
        //}
    }

    /*   public void OnServerSceneChanged(string sceneName)
       {
           GetComponent<playerSetup>().enabled = true;
           GetComponent<ActionAttack>().enabled = true;
           GetComponent<ActionMove>().enabled = true;
           GetComponent<SealCase>().enabled = true;
           GetComponent<ActionMove>().enabled = true;
           GetComponent<PathCreation>().enabled = true;
           GetComponent<NetworkedTimer>().enabled = true;
       }
       */

    [Command]
    void CmduavOff()
    {

        //uav = false;

    }
    [Command]
    void CmdPlayerOnline()
    {
        SceneManager.LoadScene("test2");
       // NetworkServer.SetAllClientsNotReady();
       // NetworkManager.singleton.ServerChangeScene("test2");

        //RpcWait();
       // uav = true;
       
    }
    [ClientRpc]
    void RpcWait()
    {
        //NetworkManager.singleton.ServerChangeScene("test2");

        
        Func<bool> myFunction = evalReady;
        /*
        yield return new WaitUntil(() => ready);
        GetComponent<playerSetup>().enabled = true;
        GetComponent<ActionAttack>().enabled = true;
        GetComponent<ActionMove>().enabled = true;
        GetComponent<SealCase>().enabled = true;
        GetComponent<ActionMove>().enabled = true;
        GetComponent<PathCreation>().enabled = true;
        GetComponent<NetworkedTimer>().enabled = true;*/

    }
    IEnumerator soCallMeMaybe()
    {
        Func<bool> myFunction = evalReady;
        yield return new WaitUntil(() => ready);
        GetComponent<playerSetup>().enabled = true;
        GetComponent<ActionAttack>().enabled = true;
        GetComponent<ActionMove>().enabled = true;
        GetComponent<SealCase>().enabled = true;
        GetComponent<ActionMove>().enabled = true;
        GetComponent<PathCreation>().enabled = true;
        GetComponent<NetworkedTimer>().enabled = true;
        done = true; 
    }
    bool evalReady()
    {
        return ready;
    }


}
