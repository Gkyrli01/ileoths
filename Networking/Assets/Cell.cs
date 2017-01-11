using UnityEngine.Networking;
using UnityEngine;

public class Cell : NetworkBehaviour
{

    GameObject manager;
    [SyncVar]
    int owner;
    static bool pause = true;
    bool first = true;
    public Material ownerMaterial;
    public Material enemyMaterial;
    [SyncVar]
    public bool cattack = false;
    public int aattackpower = 100;
    bool spellTargetable = false;
    bool itsaTrap = false;
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

    public Material OwnerMaterial
    {
        get
        {
            return ownerMaterial;
        }

        set
        {
            ownerMaterial = value;
        }
    }

    public Material EnemyMaterial
    {
        get
        {
            return enemyMaterial;
        }

        set
        {
            enemyMaterial = value;
        }
    }

    public static bool Pause
    {
        get
        {
            return pause;
        }

        set
        {
            pause = value;
        }
    }

    public bool SpellTargetable
    {
        get
        {
            return spellTargetable;
        }

        set
        {
            spellTargetable = value;
        }
    }

    public bool ItsaTrap
    {
        get
        {
            return itsaTrap;
        }

        set
        {
            itsaTrap = value;
        }
    }


    // Use this for initialization
    void Start()
    {




    }

    // Update is called once per frame
    void Update()
    {
        /*if (first) {
            first = false;
        manager = GameObject.Find("GameInstanceManager");
        if (owner == manager.GetComponent<Client>().Id)
            this.gameObject.GetComponent<Renderer>().material = OwnerMaterial;
        else
            this.gameObject.GetComponent<Renderer>().material = EnemyMaterial;
        }

        */
    }
    void OnMouseOver()
    {
        if (pause)
            return;
        if (Client.Id1 == 1)
        {
            GameObject.FindWithTag("player1").GetComponent<playerSetup>().pathPreview(transform, false);
        }
        else
        {
            GameObject.FindWithTag("player2").GetComponent<playerSetup>().pathPreview(transform, false);

        }
    }
    public void chantaje() //attack to minions
    {
        /* if (MinionObjects[i, j].GetComponent<minion>() != null)
         {

         }*/
    }
    public void attack()//enable attack lavaaaaa :D
    {
        cattack = true;
    }

    public void setTargetable(int pId, bool my, bool enemy)
    {
        if (pId == owner && my)
        {
            GetComponent<Renderer>().material.color = Color.red;
            spellTargetable = true;
        }
        if (pId != owner && enemy)
        {
            GetComponent<Renderer>().material.color = Color.red;
            spellTargetable = true;
        }
    }

    public void resetTargetable()
    {
        spellTargetable = false;
    }
    public bool getTargetable()
    {
        return spellTargetable;
    }



}
