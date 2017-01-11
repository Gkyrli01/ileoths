using UnityEngine;
using UnityEngine.Networking;

using System.Collections;

public class Patenr : NetworkBehaviour {

	public static bool metrue=false;
	// Update is called once per frame
	void Update () {
        
        if (metrue)
        {
            CmdStart();
        }

    }


    [Command]
    void CmdStart()
    {
        GameObject.FindGameObjectWithTag("op").GetComponent<NetworkManager>().onlineScene = "test2";
    }
    /*
    [ClientRpc]
    void RpcStartClient()
    {
        if (GetComponent<network>().started)
            return;
        GameObject.FindGameObjectWithTag("op").GetComponent<NetworkManager>().StartClient(GetComponent<network>().matchInfo);
    }*/
}
