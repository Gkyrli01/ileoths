using UnityEngine;
using System.Collections;
//katastrefi ola ta terata t tamplo
public class posidonasspell1 : ActionSpell {
    int owner = 1;
    public GameObject[] all;
    public int Owner
    {
        get
        {
            return owner;
        }

        set
        {
            owner = value;
        }
    }
    // Use this for initialization
    void Start () {
        state = Client.SPELL1;
        energyNeeds = 1000;
        owner = gameObject.GetComponent<tess>().Player;
        myPlayer = GameObject.FindGameObjectWithTag("player" + Client.Id1);

    }
	
	// Update is called once per frame
	void Update () {
        if (owner != Client.Id1)
            return;
        if (valid())
        {
            actionToTake();
        }

    }
    public override void actionToTake()
    {
        all = GameObject.FindGameObjectsWithTag("minion");
        foreach (GameObject x in all)
        {
            CmddestroyAnObjectFromNetwork(x);
        }
        callCommands();
 
        finalize();
    }
    public override void reset()
    {
        //No need of anything in Reset because it's a one click thing.Not more than one steps needed.
    }

    public override void finalize()
    {
        base.finalize();
        //An xreiastei kapoia alli idiaiteri leitourgia sto telos t spell mpainei p katw.
    }
}
