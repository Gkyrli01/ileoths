using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class NetworkedTimer : NetworkBehaviour {
    

    public Text counterText;
    bool didStartFinish = false;
    [SyncVar]
    public float seconds, minutes;
	// Use this for initialization
	void Start () {
       // while (GameObject.Find("Counter") == null) ;
        counterText = GameObject.Find("Counter").GetComponent<Text>();
        didStartFinish = true;
	}
	
	// Update is called once per frame
	void Update () {


        if(GameObject.Find("GameInstanceManager").GetComponent<playerTimer>().SecondPlayer)
        if (isServer) {
            
            //minutes = (int)(Time.time / 60f);
                seconds += Time.deltaTime;
                if (seconds > 59.99f)
                {
                    minutes++;
                    seconds = 0;
                }
                //seconds = (int)(Time.time % 60f);

                RpcTime();
        }

    }

    [ClientRpc]
    void RpcTime()
    {
        if(counterText!=null)
        counterText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
       
    }
}
