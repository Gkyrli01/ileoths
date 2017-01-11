using UnityEngine;
using UnityEngine.Networking;

public class ActionAttack : Action {


    RaycastHit hit;
    bool attack;
    GameObject attacker;
     

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

   
  
    void Start()
    {
        keycode = KeyCode.Mouse1;

    }

    void Update()
    {
        if (!isLocalPlayer|| gameObject.GetComponent<playerSetup>().freezed)
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
        if (!Hit.transform.gameObject.tag.Equals("minion") && attacker == null)
        {
            reset();
            return;
        }

        if (Hit.transform.gameObject != null && Hit.transform.gameObject.tag.Equals("minion") &&
            Hit.transform.gameObject.GetComponent<minion>().Owner == Client.Id1
            &&checkMana(Hit.transform.gameObject)&&checkMyRemaining(ATTACK))
            {
                if (!Hit.transform.gameObject.GetComponent<minion>().Attacking)
                {
                    if (attack)
                    {
                        attacker.GetComponent<minion>().DeactivateA();
                        attack = false;
                        attacker = null;
                        resetMinions();

                    }
                    Hit.transform.gameObject.GetComponent<minion>().ActivateA();
                    attack = true;
                    attacker = Hit.transform.gameObject;
                    int i = findCellInArrayAttack(attacker.transform);
                    
                    analdromiAttack(i % COLLUMNS, i / COLLUMNS, 2, 0);
                }
                else
                {
                    Hit.transform.gameObject.GetComponent<minion>().DeactivateA();
                    attack = false;
                    attacker = null;
                    resetMinions();
                }

            }
            else if (Hit.transform.gameObject != null && (
                     Hit.transform.gameObject.tag.Equals("minion") && 
                     !Hit.transform.gameObject.GetComponent<minion>().Attacking && 
                     attack &&
                     Hit.transform.gameObject.GetComponent<minion>().Owner != Client.Id1)||(Hit.transform.gameObject.GetComponent<tess>()!=null&&attack))
            {
            //kaleis ton server
            if (Hit.transform.gameObject.GetComponent<minion>().CanGetAttacked)
            {
                CmdAttack(attacker, Hit.transform.gameObject);
                CmdAddManaEnergy(-attacker.GetComponent<minion>().AttackManaNeeds, 0, gameObject.GetComponent<playerSetup>().MyGod);
                CmdReduceMyRemaining(ATTACK,Client.Id1);
            }
                attack = false;
                attacker.GetComponent<minion>().Deactivate();
                attacker = null;
                resetMinions();
            }


        hit = new RaycastHit();

    }

    /// <summary>
    ///      A must implement function of any action child,resets not finished work,when
    ///    the user wants to do another action.
    /// </summary>
    public override void reset()
    {
        attack = false;
        attacker = null;
        resetMinions();
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

    /*
     * Below we have the help functions that will provide simpler code
     * and so not everything is in the main function that is callable from
     * the player script.
     * */



        /// <summary>
        /// Check mana needed from minion for an attack.
        /// </summary>
        /// <param name="minion">Minion to check it's needs</param>
        /// <returns>Returns true if if the player have sufficient mana.</returns>
    bool checkMana(GameObject minion)
    {
        return gameObject.GetComponent<playerSetup>().MyGod.GetComponent<tess>().checkMana(minion.GetComponent<minion>().AttackManaNeeds);

    }

    /// <summary>
    /// Finds which minion of the array representing the arena minions is the given Transform.
    /// </summary>
    /// <param name="toFind">The transform used</param>
    /// <returns>a number that if div with with collums y get row of the array,and if you mod with COLLUMNS you get column of the array.</returns>
    int findCellInArrayAttack(Transform toFind)
    {
        for (int i = 0; i < ROWS; i++)
        {
            for (int j = 0; j < COLLUMNS; j++)
            {
                if (MinionObjects[i, j] != null && toFind == MinionObjects[i, j].transform)
                {
                    return i * COLLUMNS + j;
                }
            }
        }
        return -1;

    }




    int getEnemy()
    {
        if (Client.Id1 == 1)
            return 4;
        else
            return 3;
    }
    /// <summary>
    /// A recursive funtion that finds the possible enemies to attack from a certain minion.
    /// </summary>
    /// <param name="x">Collumn of the array representing the arena</param>
    /// <param name="y">Row of the array representing the arena</param>
    /// <param name="MaxRange">Max range of attack</param>
    /// <param name="count">Used in for recursion reasons,should be 0.</param>
    void analdromiAttack(int x, int y, int MaxRange, int count)
    {

        if (count == MaxRange + 1)
            return;

        if (y != 17 && isPath[y + 1, x] && !isMinion[y + 1, x])
        {
            analdromiAttack(x, y + 1, MaxRange, count + 1);
        }
        else if (y != 17 && isPath[y + 1, x] && isMinion[y + 1, x])

        {
            if (MinionObjects[y + 1, x].GetComponent<minion>().owner != Client.Id1)
            {
                MinionObjects[y + 1, x].GetComponent<minion>().target();
            }
        }
        if (y != 0 && isPath[y - 1, x] && !isMinion[y - 1, x])
        {
            analdromiAttack(x, y - 1, MaxRange, count + 1);
        }
        else if (y != 0 && isPath[y - 1, x] && isMinion[y - 1, x])
        {
            if (MinionObjects[y - 1, x].GetComponent<minion>().owner != Client.Id1)
            {
                MinionObjects[y - 1, x].GetComponent<minion>().target();
            }
        }
        if (x != 11 && isPath[y, x + 1] && !isMinion[y, x + 1])
        {
            analdromiAttack(x + 1, y, MaxRange, count + 1);
        }
        else if (x != 11 && isPath[y, x + 1] && isMinion[y, x + 1])
        {
            if (MinionObjects[y, x + 1].GetComponent<minion>().owner != Client.Id1)
            {
                MinionObjects[y, x + 1].GetComponent<minion>().target();
            }
        }
        if (x != 0 && isPath[y, x - 1] && !isMinion[y, x - 1])
        {
            analdromiAttack(x - 1, y, MaxRange, count + 1);
        }
        else if (x != 0 && isPath[y, x - 1] && isMinion[y, x - 1])
        {
            if (MinionObjects[y, x - 1].GetComponent<minion>().owner != Client.Id1)
            {
                MinionObjects[y, x - 1].GetComponent<minion>().target();
            }
        }


    }
  

    /// <summary>
    /// Function finalizing the attack to every client.
    /// </summary>
    /// <param name="attacker">Minion that attacks</param>
    /// <param name="defender">Minion that defends</param>
    [Command]
    void CmdAttack(GameObject attacker, GameObject defender)
    {
        if (defender.GetComponent<tess>() != null)
        {
            defender.GetComponent<tess>().attacked(attacker.GetComponent<health>().attack);
            return;
        }
        RpcAttack(attacker, defender);
    }

    /// <summary>
    ///Every client runs the attack to their game.
    /// </summary>
    /// <param name="attacker"></param>
    /// <param name="defender"></param>
    [ClientRpc]
    void RpcAttack(GameObject attacker, GameObject defender)
    {
        Vector3 a = attacker.transform.position;
        Vector3 b = defender.transform.position;
        if (attacker.GetComponent<health>().TakeDamage(defender.GetComponent<health>().attack))
        {
            playerSetup.isMinion[(int)(a.x + 8.6), (int)(a.z + 5.6)] = false;
            playerSetup.MinionObjects[(int)(a.x + 8.6), (int)(a.z + 5.6)] = null;

        }
        if (defender.GetComponent<health>().TakeDamage(attacker.GetComponent<health>().attack))
        {
            playerSetup.isMinion[(int)(b.x + 8.6), (int)(b.z + 5.6)] = false;
            playerSetup.MinionObjects[(int)(b.x + 8.6), (int)(b.z + 5.6)] = null;
        }
        //Destroy(toDestroy);
    }
    /// <summary>
    /// Not implemented for Attack.
    /// </summary>
    public override void finalize()
    {
    }
}
