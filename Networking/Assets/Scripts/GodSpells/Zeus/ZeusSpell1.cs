using UnityEngine;
using System.Collections;
using System;

public class ZeusSpell1 : ActionSpell {

    int owner = 0;

    // Use this for initialization
    void Start () {
        state = Client.SPELL2;
        energyNeeds = 1000;
        owner = gameObject.GetComponent<tess>().Player;
        myPlayer = GameObject.FindGameObjectWithTag("player" + Client.Id1);
        //multiStepSpell = true;
    }
	
	// Update is called once per frame
	void Update () {

        if (owner == Client.Id1 && valid())
        {
            multiStepActionUsed = true;
            actionToTake();
        }
	}

        public override void actionToTake()
    {
        if (step == 0)
        {
            CmdonlineHighlightMinions(false, true);
            step++;
            return;
        }
        if (step == 1)
        {
            if(Input.GetKeyDown(KeyCode.Mouse1)){
                reset();
                return;
            }
            if(Input.GetKeyDown(KeyCode.Mouse0)){
                checkIfTargetableMinion1();
                return;
            }
        }

    }

    private void checkIfTargetableMinion1()
    {
        Ray ray = Client.Cameras.current.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject != null && hit.transform.gameObject.tag.Equals("minion") && hit.transform.gameObject.GetComponent<minion>().getTargetable())
            {
                CmddestroyAnObjectFromNetwork(hit.transform.gameObject);
                callCommands();
                reset();
            }
            else
            {
                reset();
            }
        }
    }
    public override void reset()
    {
        base.finalize();
        CmdonlineCancelHighlight();
    }

}
