using UnityEngine;
using System.Collections;
using System;

public abstract class ActionSpell : Action {
   
    public override bool valid()
    {


        if (Client.myState == Client.PLAYING && state == Client.SpellState && (myPlayer != null && !myPlayer.GetComponent<playerSetup>().freezed))
            if (gameObject.GetComponent<tess>().checkEnergy(energyNeeds) && checkMyRemaining(SPELL))
                return true;
            else {
                finalize();
            }
        else if(myPlayer.GetComponent<playerSetup>().freezed && step != 0){
            reset();
        } 
        return false;
    }
    /// <summary>
    /// In which step is the spell at(Use it in multiStepSpells.
    /// </summary>
    protected int step=0;
    protected int state;
    protected int energyNeeds;
    protected bool multiStepSpell = false;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
	}


   protected void callCommands()
    {
        CmdReduceMyRemaining(SPELL, Client.Id1);
        CmdAddManaEnergy(0, -energyNeeds, gameObject);
    }
    /// <summary>
    /// spellstate-1|||multistepUsed=false|||step=0
    /// </summary>
    public override void finalize()
    {

        Client.SpellState = -1;
        multiStepActionUsed = false;
        step = 0;

    }

    /// <summary>
    /// Checks whether a the player cell was clicked.
    /// </summary>
    /// <returns>true if the player clicked his own cell.false if clicked anywher else.</returns>
    protected bool checktargetCell()
    {
        Ray ray = Client.Cameras.current.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject != null && hit.transform.gameObject.tag.Equals("Cell") && hit.transform.gameObject.GetComponent<Cell>().Owner == Client.Id1)
            {
                cellSpellEffect(hit.transform.gameObject.GetComponent<Cell>());
                return true;
            }
            else
            {
                reset();
                return false;
            }
        }
        return false;
    }




    protected virtual void minionSpellEffect(minion clickedMinion)
    {
        //to idio me to katw apla gia minion...
    }

    protected  virtual void cellSpellEffect(Cell clickedCell)
    {
        ////jithkia sas touto,kammetai t override opws sto ifestosspell1,aman enna kamete spell p epilegei cell.Taxa grafeis ti na kamei pas to cell to spell p grafeis.
    }
    /// <summary>
    /// Checks whether a spellTargetable minion was clicked.
    /// </summary>
    /// <returns>1 if targetable minion was clicked.0 if not targetable minion or other object clicked.-1 if no click occured or click on blank space.</returns>
    protected int checkIfTargetableMinion()
    {
        Ray ray = Client.Cameras.current.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject != null && hit.transform.gameObject.tag.Equals("minion") && hit.transform.gameObject.GetComponent<minion>().getTargetable())
            {
                minionSpellEffect(hit.transform.gameObject.GetComponent<minion>());
                return 1;
            }
            else
            {
                reset();
                return 0;
            }
        }
        return -1;
    }

}
