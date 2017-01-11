using UnityEngine;
using UnityEngine.Networking;

public class ActionMove : Action {

    public static readonly int MAXMOVE = 3;
    RaycastHit hit;
    GameObject moving;
    Vector3 newPosition;
    int spellAffectMove = 0;
    public RaycastHit Hit
    {
        get
        {
            return hit;
        }

        set
        {
            hit = value;
        }
    }

    public int SpellAffectMove
    {
        get
        {
            return spellAffectMove;
        }

        set
        {
            spellAffectMove = value;
        }
    }

    void Start()
    {
        keycode= KeyCode.Mouse0;
    }
    void Update()
    {
        if (!isLocalPlayer||gameObject.GetComponent<playerSetup>().freezed)
            return;
        if (Input.GetKeyDown(keycode)&&valid())
        {
            Ray ray = Client.Cameras.current.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                actionToTake();
            }
        }
    }



    /// <summary>
    ///   Every action needs to have a behaviour,this one is the function you should use from update
    ///   when the action is valid.Valid is the result of the function above.
    /// </summary>
    public override void actionToTake()
    {
        
        updatePlayerAction(this);
        if (Hit.transform.gameObject == null)
        {
            reset();
            return;
        }
        if (!Hit.transform.gameObject.tag.Equals("minion")&&moving==null)
        {
            reset();
            return;
        }    
            if (Hit.transform.gameObject != null && Hit.transform.gameObject.tag.Equals("minion") && Hit.transform.gameObject.GetComponent<minion>().Owner == Client.Id1)
        {
            if (!Hit.transform.gameObject.GetComponent<minion>().Move)
            { //&&moving==null
                if (moving != null)
                {
                    moving.GetComponent<minion>().Deactivate();
                    moving = null;
                    resetArenaColors();
                }
                Hit.transform.gameObject.GetComponent<minion>().Activate();
                moving = Hit.transform.gameObject;
                moveHighlight(moving.transform.position.x, moving.transform.position.z);
            }
            else
            {
                Hit.transform.gameObject.GetComponent<minion>().Deactivate();
                moving = null;
                resetArenaColors();
            }

            return;
        }

        if (moving != null && !Hit.transform.gameObject.tag.Equals("minion"))
        {
            newPosition = Hit.point;
            getCenter();
            if (legal()&&checkMana()&&checkMyRemaining(MOVE))
            {
                CmdMove(newPosition, moving);
                CmdAddManaEnergy(-moving.GetComponent<minion>().MoveManaNeeds, 0,gameObject.GetComponent<playerSetup>().MyGod);
                CmdReduceMyRemaining(MOVE,Client.Id1);
            }
            moving.GetComponent<minion>().Deactivate();
            moving = null;
            resetArenaColors();

        }
        hit = new RaycastHit();
    }

    /// <summary>
    ///      A must implement function of any action child,resets not finished work,when
    ///    the user wants to do another action.
    /// </summary>
    public override void reset()
    {
        moving = null;
        resetArenaColors();
        //Destroy(this);
    }

    /// <summary>
    /// Here you have to determine the state that the game must be in so the action you are creating should be used.
    /// </summary>
    public override bool valid()
    {
        if (Client.myState == Client.PLAYING&&!multiStepActionUsed)
            return true;
        reset();
        return false;
    }

    /// <summary>
    /// Not implemented for Move.
    /// </summary>
    public override void finalize()
    {
    }

    /**
     * Below there are all the helpful functions created
     * to fully implement the action.
     * */

    /// <summary>
    /// Checks move eligibility using the color of the cell you clicked.
    /// </summary>
    /// <returns>true if the move is possible</returns>
    bool legal()
    {
        if (Hit.transform.GetComponent<Renderer>().material.color == Color.yellow) /*&& (child.GetComponent<Manager>().IsPath)*/
        {
            return true;
        }

        return false;
    }


    bool checkMana()
    {
        return gameObject.GetComponent<playerSetup>().MyGod.GetComponent<tess>().checkMana(moving.GetComponent<minion>().MoveManaNeeds);
    }

    void getCenter()
    {

        float tmp = -8.5f;
        float difference = 100f;
        while (difference > Mathf.Abs(tmp - newPosition.x))
        {

            difference = Mathf.Abs(tmp - newPosition.x);
            tmp += 1;
        }
        tmp -= 1;
        newPosition.x = tmp;

        difference = 100f;
        tmp = -5.5f;
        while (difference > Mathf.Abs(tmp - newPosition.z))
        {

            difference = Mathf.Abs(tmp - newPosition.z);
            tmp += 1;
        }
        tmp -= 1;
        newPosition.z = tmp;
        newPosition.y = 0.5f;
    }





    /// <summary>
    /// Recursively finds the MAXMOVE cells that a minion can move on.
    /// </summary>
    /// <param name="x">collumn of the array representing the cells of the arena</param>
    /// <param name="y">row of the array representing the cells of the arena</param>
    /// <param name="count">Used for recursive reasons.Should be zero,excpet if you want to cover more(-1,-2...)or less(1,2,3..) distance.</param>
    void analdromi(int x, int y, int count)
    {

        if (count == MAXMOVE + 1)
            return;

        pathTransforms[y, x].GetComponent<Renderer>().material.color = Color.yellow;

        if (y != 17 && isPath[y + 1, x] && !isMinion[y + 1, x])
        {
            analdromi(x, y + 1, count + 1);
        }
        if (y != 0 && isPath[y - 1, x] && !isMinion[y - 1, x])
        {
            analdromi(x, y - 1, count + 1);
        }
        if (x != 11 && isPath[y, x + 1] && !isMinion[y, x + 1])
        {
            analdromi(x + 1, y, count + 1);
        }
        if (x != 0 && isPath[y, x - 1] && !isMinion[y, x - 1])
        {
            analdromi(x - 1, y, count + 1);
        }



    }

    void moveHighlight(float x1, float z)
    {
        bool t1 = false;

        int y = (int)(x1 + 8.6);
        int x = (int)(z + 5.6);
        int i = 0;

        analdromi(x, y, spellAffectMove);
        /*
        foreach (Transform child in Nodes.transform)
        {
            if ((Mathf.Abs(child.position.x - x) + Mathf.Abs(child.position.z - z) <= 3.1f)) //&& (child.GetComponent<Manager>().IsPath))
            {
                child.GetComponent<Renderer>().material.color = Color.yellow;
                transformsChanged.Add(child.gameObject);
            }
        }*/
    }
    /// <summary>
    /// Makes a move known to all players.
    /// </summary>
    /// <param name="position">The position that minion will move on.</param>
    /// <param name="moves">The minion that moves.</param>
    [Command]
    void CmdMove(Vector3 position, GameObject moves)
    {
        isMinion[(int)(moves.transform.position.x + 8.6), (int)(moves.transform.position.z + 5.6)] = false;

        isMinion[(int)(position.x + 8.6), (int)(position.z + 5.6)] = true;
        Vector3 tmp = moves.transform.position;
        position.y = 0.8f;
        moves.transform.position = position;
        RpcMoveUpdate(position, moves, tmp);
    }

    /// <summary>
    /// Servers sends to the clients the changes that both should make.
    /// </summary>
    /// <param name="position">New position of minion</param>
    /// <param name="moves">Minion that moves</param>
    /// <param name="old">The old position so we can make the old position false in the array of minions</param>
    [ClientRpc]
    void RpcMoveUpdate(Vector3 position, GameObject moves, Vector3 old)
    {
        isMinion[(int)(old.x + 8.6), (int)(old.z + 5.6)] = false;
        isMinion[(int)(position.x + 8.6), (int)(position.z + 5.6)] = true;
        MinionObjects[(int)(position.x + 8.6), (int)(position.z + 5.6)] = MinionObjects[(int)(old.x + 8.6), (int)(old.z + 5.6)];
        MinionObjects[(int)(old.x + 8.6), (int)(old.z + 5.6)] = null;
        spellAffectMove = 0;
        if(hasAuthority)
        hfaistosSpellHelper(position, moves);
        
    }

    private void hfaistosSpellHelper(Vector3 position, GameObject moves)
    {
        if (isPathP[(int)(position.x + 8.6), (int)(position.z + 5.6)] != moves.gameObject.GetComponent<minion>().owner)
        {//lava 
            if (pathTransforms[(int)(position.x + 8.6), (int)(position.z + 5.6)].GetComponent<Cell>().cattack)
            {
                int b = pathTransforms[(int)(position.x + 8.6), (int)(position.z + 5.6)].GetComponent<Cell>().aattackpower;
                CmdMinionTakeDamage(moves, b);
                CmdSetAttckBack(pathTransforms[(int)(position.x + 8.6), (int)(position.z + 5.6)].gameObject);
            }
        }
    }

    [Command]
    private void CmdSetAttckBack(GameObject changeMe)
    {
        changeMe.GetComponent<Cell>().cattack = false;
    }

}
