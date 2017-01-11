using UnityEngine;
using System.Collections;
//katastrefw tixea ena diko mou  minion k ena extriko.perimenw ton mario gia leptomeries an ine 2 terata px
//an tha iparxoun elegxoi px na min mpori na ektelesti an den iparxoun minions klp
public class zeusspell2 : ActionSpell
{
    int number;
    int owner = 0;
    // Transform lala;
    GameObject[] all;
     GameObject[] player=new GameObject[40];
     GameObject[] enemy= new GameObject[40];
    int e = 0;
    int p = 0;
    // Use this for initialization
    void Start () {
        state = Client.SPELL2;
        energyNeeds = 1000;
        owner = gameObject.GetComponent<tess>().Player;
        myPlayer = GameObject.FindGameObjectWithTag("player" + Client.Id1);
        
    }

    
    public override void actionToTake()
    {
        all = GameObject.FindGameObjectsWithTag("minion");

        foreach (GameObject x in all)
        {
            if (x.GetComponent<minion>().owner== Client.Id1)
            {
                player[p] = x;
                p++;
            }else
            {
                enemy[e] = x;
                e++;
            }
        }
        if (p == 0 || e == 0)
        {
            GameObject.Find("Warning").GetComponent<Animate>().go("Not enough minions.");
            reset();
            return;
        }
        number = Random.Range(0, p);
        CmddestroyAnObjectFromNetwork(player[number]);
        number = Random.Range(0, e);
        CmddestroyAnObjectFromNetwork(enemy[number]);
        callCommands();
        reset();
    }
    public override void reset()
    {
        p = 0;
        e = 0;
        finalize();
    }
    // Update is called once per frame
    void Update () {

        if (owner == Client.Id1 && valid())
        {
            actionToTake();
        }
    }
}
