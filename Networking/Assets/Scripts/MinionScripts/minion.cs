using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class minion : NetworkBehaviour {
    [SyncVar]
    public int owner;
    bool wasClicked;
    bool canGetAttacked = false;
    bool spellTargetable = false;
    Color targetColor = Color.magenta;
    Color defaultColor;
    Color myColor = Color.cyan;
    Color enemyColor = Color.blue;
    bool attacking=false;
    bool move = false;
    int moveManaNeeds=200;
    int attackManaNeeds=200;

    public bool WasClicked
    {
        get
        {
            return wasClicked;
        }

        set
        {
            wasClicked = value;
        }
    }

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

    public bool Attacking
    {
        get
        {
            return attacking;
        }

        set
        {
            attacking = value;
        }
    }

    public bool Move
    {
        get
        {
            return move;
        }

        set
        {
            move = value;
        }
    }

    public bool CanGetAttacked
    {
        get
        {
            return canGetAttacked;
        }

        set
        {
            canGetAttacked = value;
        }
    }

    public int MoveManaNeeds
    {
        get
        {
            return moveManaNeeds;
        }

        set
        {
            moveManaNeeds = value;
        }
    }

    public int AttackManaNeeds
    {
        get
        {
            return attackManaNeeds;
        }

        set
        {
            attackManaNeeds = value;
        }
    }

    public void OnMouseDown()
    {
        
        //Activate();
    }


    void OnMouseUp()
    {
        WasClicked = true;
    }


    public void Activate()
    {   
        GetComponent<Renderer>().material.color = Color.yellow;
        move = true;
    }


    public void Deactivate()
    {
        GetComponent<Renderer>().material.color = Color.red;
        move = false;


    }
    public void ActivateA()
    {
        GetComponent<Renderer>().material.color = Color.yellow;
        Attacking = true;
        
    }


    public void DeactivateA()
    {
        GetComponent<Renderer>().material.color = Color.red;
        Attacking = false;

    }


    public void target()
    {
        GetComponent<Renderer>().material.color = targetColor;
        CanGetAttacked = true;
    }
    public void cancel()
    {
        GetComponent<Renderer>().material.color = defaultColor;
        CanGetAttacked = false;
        Attacking = false;
        move = false;
    }
    /// <summary>
    /// Used for spells,setting a minion highlighting and targetable setting.
    /// </summary>
    /// <param name="pId">playerId</param>
    /// <param name="my">true if y want to set targetable your minions</param>
    /// <param name="enemy">true if y want to set targetable enemy minions</param>
    public void setTargetable(int pId,bool my,bool enemy)
    {
        if (pId == owner&& my)
        {
            GetComponent<Renderer>().material.color = myColor;
            spellTargetable = true;
        }
        if(pId!=owner&&enemy)
        {
            GetComponent<Renderer>().material.color = enemyColor;
            spellTargetable = true;
        }

       

    }

    public void resetTargetable()
    {
        spellTargetable = false;
        GetComponent<Renderer>().material.color = defaultColor;

    }
    public bool getTargetable()
    {
        return spellTargetable;
    }

    // Use this for initialization
    void Start () {
        defaultColor = GetComponent<Renderer>().material.color;

    }

}
