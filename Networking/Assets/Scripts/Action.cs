using UnityEngine;
using UnityEngine.Networking;
public abstract class Action : NetworkBehaviour {


    /// <summary>
    /// Attack const of Action used in CheckMyRemaining function.
    /// </summary>
    public const int ATTACK=1;

    /// <summary>
    /// Summon const of Action used in CheckMyRemaining function.
    /// </summary>
    public const int SUMMON =2;

    /// <summary>
    /// Move const of Action used in CheckMyRemaining function.
    /// </summary>
    public const int MOVE = 3;

    /// <summary>
    /// Spell const of Action used in CheckMyRemaining function.
    /// </summary>
    public const int SPELL = 4;

    /// <summary>
    /// Some actions are have multiple steps.Set this true so other spell dont interfere with your action.
    /// </summary>
    public static bool multiStepActionUsed = false;

    public static Transform[,] pathTransforms = playerSetup.pathTransforms;

    public static bool[,] isPath = playerSetup.isPath;

    public static int[,] isPathP = playerSetup.isPathP;

    public static bool[,] isMinion = playerSetup.isMinion;

    public static GameObject[,] MinionObjects = playerSetup.MinionObjects;

    public static readonly int COLLUMNS = 12;
    public static readonly int ROWS = 18;
    public Color ChildColor;
    Color myColor = Color.green;
    Color enemyColor = Color.cyan;
    public KeyCode keycode;
    protected GameObject myPlayer;
    bool actionCompleted;

    public bool ActionCompleted
    {
        get
        {
            return actionCompleted;
        }

        set
        {
            actionCompleted = value;
        }
    }

    void Start()
    {
            myPlayer = GameObject.FindGameObjectWithTag("player" + Client.Id1);
    }

    /// <summary>
    /// Here you have to determine the state that the game must be in so the action you are creating should be used.
    /// </summary>
    public abstract bool valid();

    /**
     Every action needs to have a behaviour,this one is the function you should use from update
        when the action is valid.Valid is the result of the function above. 
     * */
    public abstract void actionToTake();


    /**
     A must implement function of any action child,resets not finished work,when
        the user wants to do another action.
     * */
    public abstract void reset();

    /**
     Resets to normal cell colors that may change due to highlights during some action happening
     * */

    public abstract void finalize();
    public bool resetArenaColors()
    {    
        if(GameObject.FindWithTag("player1")!=null)
         ChildColor= GameObject.FindWithTag("player1").GetComponent<playerSetup>().ChildColor;
        else
            ChildColor = GameObject.FindWithTag("player2").GetComponent<playerSetup>().ChildColor;

        for (int i = 0; i < ROWS; i++)
        {
            for (int j = 0; j < COLLUMNS; j++)
            {
                if (isPathP[i, j] == 0)
                    pathTransforms[i, j].GetComponent<Renderer>().material.color = ChildColor;
                else if(pathTransforms[i, j].GetComponent<Cell>().getTargetable()|| pathTransforms[i, j].GetComponent<Cell>().cattack)
                {

                }
                else if (isPathP[i, j] == Client.Id1)
                {
                    pathTransforms[i, j].GetComponent<Renderer>().material.color = myColor;
                }
                else
                {
                    pathTransforms[i, j].GetComponent<Renderer>().material.color = enemyColor;

                }

            }
        }
        return false;

    }
   
    /**
     This function has as goal to maintanin current Action inUse from user when something happens that
        may change current action.If changes happen then it resets all the unfinished work the action was
        doing.
     * */
    public bool updatePlayerAction(Action use)
    {
        if(gameObject.GetComponent<playerSetup>().MyAction == null)
            gameObject.GetComponent<playerSetup>().MyAction = use;
        if (use != gameObject.GetComponent<playerSetup>().MyAction && gameObject.GetComponent<playerSetup>().MyAction != null)
        {
            gameObject.GetComponent<playerSetup>().MyAction.reset();
            gameObject.GetComponent<playerSetup>().MyAction = use;
            use.reset();
            return true;
        }
        return false;
    }
    /**

    This function has as goal to reset attack-move status of minions.
    Calls cancel  that has exactly this functionality.
     * */
    protected void resetMinions()
    {
        for (int i = 0; i < ROWS; i++)
        {
            for (int j = 0; j < COLLUMNS; j++)
            {
                if (MinionObjects[i, j] != null)
                {
                    MinionObjects[i, j].GetComponent<minion>().cancel();
                }
            }
        }
    }

    /**
     * A Command to the server to add or substract mana or energy
     * */
    [Command]
    protected void CmdAddManaEnergy(int mana, int energy,GameObject myGod)
    {
        myGod.GetComponent<tess>().add(mana, energy);
    }

    /// <summary>
    /// Depending on the value of whatActionIam checks whether the player can use the action requested.
    /// </summary>
    /// <param name="whatActionIam"></param>
    /// <returns></returns>
    public bool checkMyRemaining(int whatActionIam)
    {
        bool toret = false;
        if(myPlayer==null)
            myPlayer= GameObject.FindGameObjectWithTag("player" + Client.Id1);
        switch (whatActionIam)
        {
            case ATTACK:
                if(!(toret= myPlayer.GetComponent<playerSetup>().AttacksRemaining != 0))
                    GameObject.Find("Warning").GetComponent<Animate>().go(Animate.ATTACKLESS);
                break;
            case SPELL:
                if(!(toret = myPlayer.GetComponent<playerSetup>().SpellsRemaining != 0))
                    GameObject.Find("Warning").GetComponent<Animate>().go(Animate.SPELLESS);
                break;
            case MOVE:
                if(!(toret = myPlayer.GetComponent<playerSetup>().MovesRemaining != 0))
                    GameObject.Find("Warning").GetComponent<Animate>().go(Animate.MOVELESS);
                break;
            case SUMMON:
                if(!(toret = myPlayer.GetComponent<playerSetup>().SummonsRemaining != 0))
                    GameObject.Find("Warning").GetComponent<Animate>().go(Animate.SUMMONLESS);
                break;
            default:return false;
        }
        return toret;
    }

    [Command]
    public void CmdReduceMyRemaining(int whatActionIam,int id)
    {
        if (myPlayer == null)
            myPlayer=GameObject.FindGameObjectWithTag("player" +id);
        switch (whatActionIam)
        {
            case ATTACK:
                 myPlayer.GetComponent<playerSetup>().AttacksRemaining--;
                break;
            case SPELL:
                 myPlayer.GetComponent<playerSetup>().SpellsRemaining--;
                break;
            case MOVE:
                 myPlayer.GetComponent<playerSetup>().MovesRemaining--;
                break;
            case SUMMON:
                 myPlayer.GetComponent<playerSetup>().SummonsRemaining--;
                break;
            default: return ;
        }
    }

    /// <summary>
    ///Caution:Not checked yet. Destroy an object online
    /// </summary>
    /// <param name="toDestroy"></param>
    [Command]
    protected void CmddestroyAnObjectFromNetwork(GameObject toDestroy)
    {
        Destroy(toDestroy);
    }

    /// <summary>
    /// Call this funtion if you want to make minion targetable for a spell implementation.
    /// <minionScript>funtion getTargetable().Use this to determine whether a minion is targetable after a click occurs.</minionScript>        
    /// </summary>
    /// <Note>Possible to set both true.</Note>
    /// <param name="myMinions">Set targetable your minions</param>
    /// <param name="enemyMinions">Set targetable enemy minions</param>
    [Command]
    protected void CmdonlineHighlightMinions(bool myMinions, bool enemyMinions)
    {
        RpconlineHighlightMinionsCall(myMinions, enemyMinions);
    }

    [ClientRpc]
    private void RpconlineHighlightMinionsCall(bool myMinions, bool enemyMinions)
    {
        highlightMinions( myMinions,  enemyMinions);
    }
    /// <summary>
    /// Call this function if you want to set as targetable minions.
    /// </summary>
    /// <param name="myMinions">Set targetable your minions</param>
    /// <param name="enemyMinions">Set targetable enemy minions</param>
    /// <Note>Possible to set both true.</Note>
    protected void highlightMinions(bool myMinions,bool enemyMinions)
    {
        foreach(GameObject aMinion in MinionObjects)
        {
            if (aMinion == null) ;
            else if (aMinion.tag.Equals("god")) ;
            else
            {
                
                aMinion.GetComponent<minion>().setTargetable(gameObject.GetComponent<tess>().Player,myMinions,enemyMinions);
            }
        }
    }

    /// <summary>
    /// Call this if you want to cancel the highlight of an online highlight that occured before.
    /// </summary>
    [Command]
    protected void CmdonlineCancelHighlight()
    {
        RpconlineCancelHighlightCall();
    }
    [ClientRpc]
    private void RpconlineCancelHighlightCall()
    {
        cancelSpellHighlight();
    }
    /// <summary>
    /// Call this after a spell was cancelled or finished.
    /// </summary>
    protected void cancelSpellHighlight()
    {
        foreach (GameObject aMinion in MinionObjects)
        {
            if (aMinion == null) ;
            else if (aMinion.tag.Equals("god"));
            else
            {
                aMinion.GetComponent<minion>().resetTargetable();
            }
        }
    }
    
    protected void spellCellHighlight(bool myCells, bool enemyCells)
    {
        foreach(Transform aCell in pathTransforms)
        {
            if (aCell == null);
            else
            {
                aCell.gameObject.GetComponent<Cell>().setTargetable(gameObject.GetComponent<tess>().Player, myCells, enemyCells);
            }
        }
        return;
    }

    protected void spellCancelCellHighlight()
    {
        foreach (Transform aCell in pathTransforms)
        {
            if (aCell == null);
            else
            {
                aCell.gameObject.GetComponent<Cell>().resetTargetable();
            }
        }
        resetArenaColors();
    }

    [Command]
    protected void CmdMinionTakeDamage(GameObject minion,int damage)
    {
        RpcMinionTakeDamage(minion, damage);
    }

    [ClientRpc]
    private void RpcMinionTakeDamage(GameObject minion, int damage)
    {

        Vector3 a = minion.transform.position;
        if (minion.GetComponent<health>().TakeDamage(damage))
        {
            playerSetup.isMinion[(int)(a.x + 8.6), (int)(a.z + 5.6)] = false;
            playerSetup.MinionObjects[(int)(a.x + 8.6), (int)(a.z + 5.6)] = null;

        }


    }

}
