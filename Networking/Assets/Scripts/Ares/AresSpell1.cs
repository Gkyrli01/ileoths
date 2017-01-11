using UnityEngine;
using System.Collections;
using System;

public class AresSpell1 : ActionSpell
{

    const int BOOST = 10;
    int owner = 0;

    public override void actionToTake()
    {
        if (step == 0)
        {
            CmdonlineHighlightMinions(true, false);
            step++;
            return;
        }
        if (step == 1)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                checkIfTargetableMinion();
            }
        }
    }

    private void checkIfTargetableMinion()
    {
        Ray ray = Client.Cameras.current.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject != null && hit.transform.gameObject.tag.Equals("minion") && hit.transform.gameObject.GetComponent<minion>().getTargetable())
            {
                hit.transform.gameObject.GetComponent<health>().attack += BOOST;
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

    // Use this for initialization
    void Start()
    {
        state = Client.SPELL1;
        energyNeeds = 1000;
        owner = gameObject.GetComponent<tess>().Player;
        myPlayer = GameObject.FindGameObjectWithTag("player" + Client.Id1);

    }

    // Update is called once per frame
    void Update()
    {
        if (owner == Client.Id1 && valid())
        {
            multiStepActionUsed = true;
            actionToTake();
        }

    }
}