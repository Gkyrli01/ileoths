using UnityEngine.Networking;

using UnityEngine;
using System.Collections;
using System;
//vazi lava se ena kouti pou otan ena exthriko teras 
//paei sto kouti xanei zoi kai i lava eksafanizete
public class ifestosspell1 : ActionSpell
{
 
    int owner = 1;

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
            spellCellHighlight( true, false);
            //CmdonlineHighlightMinions(false, true);
            step++;
            return;
        }
        if (step == 1)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {//deksi
                reset();
                return;
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {// aristero
                if (checktargetCell())
                {
                    callCommands();//Stelnei ta Command p prepei sto server gia energy kai remaining spells.
                    reset();
                }
                return;
            }
        }
       
    }


    [Command]
    private void CmdmakeTrapKnown(GameObject clickedCell)
    {
        clickedCell.GetComponent<Cell>().attack();
    }

    protected override void cellSpellEffect(Cell clickedCell)
    {
        CmdmakeTrapKnown(clickedCell.gameObject);
    }
    

    /*
     * Xreiazetai h reset giati t spell en ginisketai se ena step mono.Patas to spell,meta thkialeeis to monopati p theleis
     *ara kapoies fores mporei na theleis na akurwseis to spell.Enna m peis gt na men kalesw tin finalize?Gt i reset kaleitai p tin valid.Enna m peis
     * kaleitai je i finalize,alla i  diafora einai oti i finalize kaleitai mono ama o logos p en tha ginei to spell en epeidi leifketai mana(energy oti xreiazetai
     * telos pantwn).
     *   
     * */
    public override void reset()
    {
        spellCancelCellHighlight();
        base.finalize();
        //No need of anything in Reset because it's a one click thing.Not more than one steps needed.
    }

    /*
    private void checktarget1()
    {
        Ray ray = Client.Cameras.current.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject != null && hit.transform.gameObject.tag.Equals("Cell") && hit.transform.gameObject.GetComponent<Cell>().Owner == Client.Id1)
            {
                hit.transform.gameObject.GetComponent<Cell>().attack();//apla kanw true
                CmdReduceMyRemaining(SPELL, Client.Id1);
                CmdAddManaEnergy(0, -energyNeeds, gameObject);
            }
        }
    }**/
}
