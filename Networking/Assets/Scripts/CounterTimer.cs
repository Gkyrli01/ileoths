using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class CounterTimer : NetworkBehaviour
{

    public readonly int roundTime = 10;

    static bool firstTime = true;
    public Text counterText;
    [SyncVar]
    public float seconds, minutes;
    [SyncVar]
    float temp;
    [SyncVar]
    private int current;

    public int Current
    {
        get
        {
            return current;
        }

        set
        {
            current = value;
        }
    }

    // Use this for initialization
    void Start()
    {   




        if (isServer && firstTime) {
            firstTime = false;
            Current = roundTime;
        }
        counterText = GameObject.Find("TurnTimer").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isServer)
        {
            minutes = (int)(Time.time / 60f);
            if (temp != seconds) { 
                Current--;
                if (Current == 0)
                {
                    Current = roundTime;
                    if (this.gameObject.GetComponent<ManageTurns>().I == 2)
                    {
                        this.gameObject.GetComponent<ManageTurns>().RpcwhenIequals2();
                        this.gameObject.GetComponent<ManageTurns>().I = 1;
                    }
                    else
                    {
                        this.gameObject.GetComponent<ManageTurns>().RpcwhenIequals1();
                        this.gameObject.GetComponent<ManageTurns>().I = 2;
                    }
                    //kaleitai to end turn
                }
           
            seconds = (int)(Time.time % 60f);
            temp = seconds;
            }
            else
                seconds = (int)(Time.time % 60f);
            RpcTime();
        }

    }

    [ClientRpc]
    void RpcTime()
    {
        if (counterText != null)
            counterText.text = Current+"s";

    }
}
