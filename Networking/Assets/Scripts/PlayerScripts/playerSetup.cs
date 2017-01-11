using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class playerSetup :NetworkBehaviour  {

    

    [SyncVar]
    int attacksRemaining;
    [SyncVar]
    int movesRemaining;
    [SyncVar]
    int spellsRemaining;
    [SyncVar]
    int summonsRemaining;
    /// <summary>
    /// Used for infinity number of permitted moves,attacks,spells etc.
    /// </summary>
    public const int INFINITY = -1;
    public static readonly int TYPE1 = 1;
    public static readonly int TYPE2 = 2;
    public static readonly int TYPE3 = 3;
    public static readonly int TYPE4 = 4;
    public static readonly int D0 = 0;
    public static readonly int D90 = 1;
    public static readonly int D180 = 2;
    public static readonly int D270 = 3;
    public static readonly int DEGREES = 90;

    public static readonly int COLLUMNS = 12;
    public static readonly int ROWS = 18;
    GameObject myGod;
    Action myAction;
    MinionSeal currentSealToSummon;

    List<MinionSeal> sealCollection = new List<MinionSeal>();


    enum MyEnum
    {
        
    }
    public static bool refresh = false;
    public static int num = 0;
    int id;
    public Text timerP;
    //Camera myCam;
    public GameObject minion;
    public GameObject sealCase;
    GameObject Nodes;
    public Color ChildColor;
    Color myColor=Color.green;
    Color enemyColor=Color.cyan;
    Color wrongColor = Color.red;
    Color targetColor = Color.magenta;
    bool helper = false;

    public bool justChanged = false;
    ArrayList minions = new ArrayList();
    Vector3 newPosition;
    public bool freezed;
    public static Transform[,] pathTransforms =new Transform[ROWS, COLLUMNS];
    
    public static bool[,] isPath = new bool[ROWS, COLLUMNS];
    
    public static int[,] isPathP = new int[ROWS, COLLUMNS];

    public static bool[,] isMinion = new bool[ROWS, COLLUMNS];

    public static GameObject[,] MinionObjects = new GameObject[ROWS, COLLUMNS];

    int tabClicked = 0;
    int qClicked = TYPE1;
    bool attack = false;
    GameObject attacker = null;
    GameObject moving = null;
    List<GameObject> transformsChanged = new List<GameObject>();



    public int Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }

    public Action MyAction
    {
        get
        {
            return myAction;
        }

        set
        {
            myAction = value;
        }
    }

    public GameObject MyGod
    {
        get
        {
            return myGod;
        }

        set
        {
            myGod = value;
        }
    }

    public int AttacksRemaining
    {
        get
        {
            return attacksRemaining;
        }

        set
        {
            attacksRemaining = value;
        }
    }

    public int MovesRemaining
    {
        get
        {
            return movesRemaining;
        }

        set
        {
            movesRemaining = value;
        }
    }

    public int SpellsRemaining
    {
        get
        {
            return spellsRemaining;
        }

        set
        {
            spellsRemaining = value;
        }
    }

    public int SummonsRemaining
    {
        get
        {
            return summonsRemaining;
        }

        set
        {
            summonsRemaining = value;
        }
    }

    /// <summary>
    /// Initial values of the arrays used to represent certain states of the arena such as,minions and if there is a minion on a certain cell(true-false),path,whose path is(1 or 2),the transforms of each position
    /// </summary>
    void initializeArray()
    {
        int i = ROWS-1;
        int j = COLLUMNS-1;
    
        foreach (Transform child in Nodes.transform)
        {
            pathTransforms[i, j] = child;
            j--;
            if (j < 0)
            {
                j = COLLUMNS-1;
                i--;
            }
        }
        pathTransforms[17, 5].GetComponent<Renderer>().material.color = myColor;
        pathTransforms[17, 6].GetComponent<Renderer>().material.color = myColor;
        isPath[17, 5] = true;
        isPathP[17, 5] = 1;
        isPathP[17, 6] = 1;
        isPath[17, 6] = true;
        isPath[0, 5] = true;
        isPath[0, 6] = true;
        isPathP[0, 5] = 2;
        isPathP[0, 6] = 2;
        isMinion[0, 5] = true;
        isMinion[0, 6] = true;
        isMinion[17, 5] = true;
        isMinion[17, 6] = true;
        pathTransforms[0, 5].GetComponent<Renderer>().material.color = enemyColor;
        pathTransforms[0, 6].GetComponent<Renderer>().material.color = enemyColor;

    }
    // Use this for initialization
    void Start () {

        Client.Cameras.player1 = GameObject.FindWithTag("MainCamera");
        Client.Cameras.player2 = GameObject.FindWithTag("Cam2");
        
        Client.Cameras.summon = GameObject.Find("SummonCamera");
        Nodes = GameObject.Find("Nodes");
        initializeArray();
        Client.setInitialTransforms();
        ChildColor = Nodes.GetComponentInChildren<Renderer>().material.color;
        if (isLocalPlayer)
        {
            Transform pos;
            if (num == 0)
            {
                id = 1;
                GameObject.Find("GameInstanceManager").GetComponent<Client>().Id = id;
                freezed = false;
                pos = GameObject.Find("SealCase1Pos").GetComponent<Transform>();
                GameObject textObject = (GameObject)Instantiate(sealCase,pos.position,pos.rotation);
                //MyGod = GameObject.Find("hermes");
                CmdcallMyGod((gods.HERMES), id);    
            }
            tag = "player1";
            if (num == 1)
            {
                tag = "player2";
                id = 2;
                GameObject.Find("GameInstanceManager").GetComponent<Client>().Id = id;
                CmdsecondPlayerIsOnline();
                pos = GameObject.Find("SealCase2Pos").GetComponent<Transform>();
                GameObject textObject = (GameObject)Instantiate(sealCase, pos.position, pos.rotation);
                freezed = true;
                CmdcallMyGod((gods.HERMES), id);                   // MyGod = GameObject.Find("hermes1");
                swapGodUI();
                CmdsyncArrays();
            }

            Client.enableCamera(Client.PLAYER);
            freeze();

            /**
             * Temporary Code for Testing
             * 
             * 
             * 
             * 
             * 
             * */
            gameObject.GetComponent<SealCase>().newMinionSeal(retrieveMinionSeal(Minions.CYCLOPS),Minions.CYCLOPS);
        /**
         * 
         * 
         * End of temporary
         * 
         * */
        }
        else
        {

        }
        num++;
        

    }

    GameObject retrieveMinionSeal(int Code)
    {
        return GameObject.Find("minionsealPrefabs").GetComponent<Minions>().giveMeMinionSealPrefab(Code);
    }

    [Command]
    void CmdcallMyGod(int resource,int player)
    {

        Transform pos;
        if (player == 1)
            pos = pathTransforms[17, 5];
        else
            pos = pathTransforms[0, 5];

        Vector3 tmp = Vector3ByValue(pos.position);
        tmp.z = tmp.z + 0.5f;
        GameObject god;
        if (player==1)
             god = (GameObject)Instantiate(GameObject.Find("godprefabs").GetComponent<gods>().giveMeGodPrefab(resource), tmp, rotate(QuaternionByValue(pos.rotation).eulerAngles, 0, 0, 0));
        else
             god = (GameObject)Instantiate(GameObject.Find("godprefabs").GetComponent<gods>().giveMeGodPrefab(resource), tmp, rotate(QuaternionByValue(pos.rotation).eulerAngles,0,180,0));
        god.GetComponent<tess>().Player = player;
        god.GetComponent<minion>().owner = player;
        NetworkServer.SpawnWithClientAuthority(god,gameObject);
        
        RpcupdateGod(god, player);
    }


    public static Quaternion rotate(Vector3 refr,float x,float y,float z)
    {
        return Quaternion.Euler(refr.x+x, refr.y + y, refr.z+z);
    }

    public static Vector3 Vector3ByValue(Vector3 refr)
    {
        Vector3 ret = new Vector3(refr.x, refr.y, refr.z);
        return ret;
    }
    public static Quaternion QuaternionByValue(Quaternion refr)
    {
        Quaternion ret = new Quaternion(refr.x, refr.y, refr.z, refr.w);
        return ret;
    }

    [ClientRpc]
    void RpcupdateGod(GameObject god,int player)
    {
        
        god.GetComponent<tess>().assignTextToGods();
        MyGod = god;
        if (player == 1)
        {
            MinionObjects[17, 5] = god;
            MinionObjects[17, 6] = god;
        }
        if (player == 2)
        {
            MinionObjects[0, 5] = god;
            MinionObjects[0, 6] = god;
        }
    }
    void Update()
    {
        if (isLocalPlayer)
        {
            if (justChanged)
                freeze();
        }
        if (refresh)
        {
            cancelHighlight2(false);
            refresh = !refresh;
        }
        if (!isLocalPlayer)
            return;
        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit hit;
            Ray ray = Client.Cameras.current.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                newPosition = hit.point;
                if (Client.myState == Client.SEALCASE)
                {
                    toSummonCamera(hit);
                }
                else if (Client.myState == Client.SUMMON)
                {
                    getCenter();
                    if (!freezed)
                            
                    {
                        if (makePath(hit)  )
                        {
                            callMinion(hit);
                        }
                        Cell.Pause = false;
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            tabClicked += DEGREES;
            if (tabClicked == 360)
                tabClicked = 0;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            qClicked++;
            if (qClicked == TYPE4 + 1)
                qClicked = TYPE1;
        }

        if (GameObject.Find("Button").GetComponent<EndTurn>().WasClicked)
        {
            GameObject.Find("Button").GetComponent<EndTurn>().WasClicked = false;
            CmdEndTurn();
        }
    }

    /// <summary>
    /// Makes the god information bar to go in the correct place,depending on the player.
    /// </summary>
    void swapGodUI()
    {
        Vector3 tmp = GameObject.Find("Host").transform.position;
        GameObject.Find("Host").transform.position = GameObject.Find("Server").transform.position;
        GameObject.Find("Server").transform.position = tmp;
    }
    /// <summary>
    /// When the second player is here,we tell to the manager to set SecondPlayer flag as online,to eventually start the timer.
    /// </summary>
    [Command]
    void CmdsecondPlayerIsOnline()
    {
        GameObject.Find("GameInstanceManager").GetComponent<playerTimer>().SecondPlayer = true;
    }

    /// <summary>
    /// The command that takes the cell coordinates and turns them as true,owner id.
    /// </summary>
    /// <param name="i">Row of array</param>
    /// <param name="j">Collumn of array</param>
    /// <param name="owner">The player who makes the path(1 or 2)</param>
    [Command]
    void CmdPath(int i,int j,int owner)
    {
        isPath[i, j] = true;
        isPathP[i, j] = owner;
        pathTransforms[i, j].gameObject.GetComponent<Cell>().Owner = owner;
        RpcPath(i, j, owner);
    }

    /// <summary>
    /// Asks every client to update their arrays accordingly.
    /// </summary>
    /// <param name="i"></param>
    /// <param name="j"></param>
    /// <param name="owner"></param>
    [ClientRpc]
    void RpcPath(int i,int j,int owner)
    {
        
        isPath[i, j] = true;
        isPathP[i, j] = owner;
        pathTransforms[i, j].gameObject.GetComponent<Cell>().Owner = owner;
        
    }

    /// <summary>
    /// Cancels  highlight of cells.If establish is true then it makes the highlighted cells permanent and makes it a path of player 1 or 2.
    /// </summary>
    /// <param name="establish">Whether is a create path function or a highlight cancelation one.</param>
    /// <returns></returns>
     bool cancelHighlight2(bool establish)
    {
        bool path = false;
        if (establish)
        {
            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLLUMNS; j++)
                {
                    if (pathTransforms[i, j].GetComponent<Renderer>().material.color == myColor) { 
                        if(isPath[i, j] == false)
                            path = true;
                        isPath[i, j]=true;
                        isPathP[i, j] = Client.Id1;
                        CmdPath(i, j, Client.Id1);
                        
                    }

                }
            }
            return path;
        }

        for (int i = 0; i < ROWS; i++)
        {
            for (int j = 0; j < COLLUMNS; j++)
            {
                if(isPathP[i,j]==0)
                    pathTransforms[i,j].GetComponent<Renderer>().material.color = ChildColor;
                else if (pathTransforms[i, j].GetComponent<Cell>().getTargetable() || pathTransforms[i, j].GetComponent<Cell>().cattack)
                {

                }
                else if(isPathP[i,j]==Client.Id1)
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
    /// <summary>
    /// Finds the center of a certain cell.May be not needed anymore because we can find the cell using hit.transform.gameobject....
    /// </summary>
    void getCenter()
    {

        float tmp=-8.5f;
        float difference = 100f;
        while (difference > Mathf.Abs(tmp - newPosition.x))
        {
            
            difference = Mathf.Abs(tmp - newPosition.x);
            tmp += 1;
        }
        tmp -= 1;
        newPosition.x=tmp;

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
    /// Finds which cell of the array representing the arena is the given Transform.
    /// </summary>
    /// <param name="toFind">The transform used</param>
    /// <returns>a number that if div with with collums y get row of the array,and if you mod with COLLUMNS you get column of the array.</returns>
    int findCellInArray(Transform toFind)
    {
        for(int i = 0; i < ROWS; i++)
        {
            for(int j = 0; j < COLLUMNS; j++)
            {
                if (toFind == pathTransforms[i, j])
                {
                    return i * COLLUMNS + j;
                }
            }
        }
        return -1;
        
    }

    /// <summary>
    ///Call this when you want a certain path to be created.
    /// </summary>
    /// <param name="hit">The center of the path you create.</param>
    /// <returns></returns>
    public bool makePath(RaycastHit hit)
    {
        
        Cell.Pause = true;
        return pathPreview(hit.transform, true);
    }
    /// <summary>
    /// Pathpreview,used both for creating and seeing a preview of a possible path.
    /// </summary>
    /// <param name="myT">Transform of hit when makepath was used,transform of hovered cell.</param>
    /// <param name="establish">if pathPreview was called from makePath,establish is true and it means that it is patg finalization</param>
    /// <returns></returns>
    public bool pathPreview(Transform myT,bool establish)
    {
        int i = 0;        
            i=findCellInArray(myT);
            return pathHighlight(i % COLLUMNS,i / COLLUMNS, qClicked, tabClicked,establish);
    }

    /// <summary>
    /// Clicking on a minion seal,it takes you to the Summon camera.
    /// </summary>
    /// <param name="hit"></param>
    void toSummonCamera(RaycastHit hit)
    {
        if (hit.transform.gameObject != null && hit.transform.gameObject.tag.Equals("Seal"))
        {
            currentSealToSummon = hit.transform.gameObject.GetComponent<MinionSeal>();
            Cell.Pause = false;
            Summon.MakingPath= true;
            Client.myState = Client.SUMMON;
            Client.enableCamera(Client.SUMMON);
        }

    }
    /// <summary>
    /// A minion is called here,after a path was finalized.
    /// </summary>
    /// <param name="hit">The center of the path created.</param>
     void callMinion(RaycastHit hit) {
        bool spawn = true;
        var tmp1 = id;
        if (hit.transform.gameObject != null && hit.transform.gameObject.tag.Equals("minion")||attacker!=null||moving!=null)
            return;
                if (spawn)
                {
            Quaternion neo = minion.transform.rotation;
            neo.y += 90;
            if (id == 2)
                neo.y = neo.y + 270;
                    CmdSpawnMinion(newPosition,neo, tmp1,currentSealToSummon.Code);
                }
    }
    /// <summary>
    /// Asks the instance manager to end the player turn.
    /// </summary>
    [Command]
    void CmdEndTurn()
    {
        GameObject.Find("GameInstanceManager").GetComponent<playerTimer>().CmdendTurn();
    }


    [Command]
    void CmdAddManaEnergy(int mana,int energy)
    {
        MyGod.GetComponent<tess>().add(mana,energy);
    }

    bool checkEnergy(int energyNeeded)
    {
        return MyGod.GetComponent<tess>().checkEnergy(energyNeeded);

    }
    bool checkMana(int manaNeeded)
    {
        return MyGod.GetComponent<tess>().checkMana(manaNeeded);
    }

    [Command]
    void CmdUpdateManaServer(GameObject myGod)
    {
        if (myGod == null)
            return;
        myGod.GetComponent<tess>().newTurn();
    }

    /// <summary>
    /// When turn changes this function is called to each player,and freezes or the opposite,resetting every unfinished action.
    /// </summary>
    /// 
    [Command]
    void CmdResetRemainingVariables(bool playing)
    {
        if (playing)
        {
            attacksRemaining = 2;
            movesRemaining = 2;
            spellsRemaining = 1;
            summonsRemaining = 1;
            return;
        }
        attacksRemaining = 0;
        movesRemaining = 0;
        spellsRemaining = 0;
        summonsRemaining = 0;
    }
    
    [Command]
    void CmdmakeSureEveryOneKnowsCorrectTags(int tag)
    {
        gameObject.tag = "player" + tag;
        RpcUpdateTags(tag);

    }
    [ClientRpc]
    void RpcUpdateTags(int tag)
    {
        gameObject.tag = "player" + tag;
    }
    public void freeze()
    {
        if (id == Client.Id1)
            CmdmakeSureEveryOneKnowsCorrectTags(id);
        if (GameObject.Find("WhoseTurn").GetComponent<Text>().text.Equals("MyTurn"))
        {
            freezed = false;
            CmdUpdateManaServer(MyGod);
            gameObject.GetComponent<SealCase>().newMinionSeal(retrieveMinionSeal(Minions.CYCLOPS), Minions.CYCLOPS);
            CmdResetRemainingVariables(true);

        }
        else
        {

            freezed = true;
            if (myAction != null)
            {
                MyAction.reset();
                MyAction = null;
            }
            CmdResetRemainingVariables(false);

        }
        justChanged = false;
    }

    /// <summary>
    /// Call the server to spawn a new minion.
    /// </summary>
    /// <param name="i">Position of the minion</param>
    /// <param name="z">Rotation of the minion</param>
    /// <param name="owner">The owner of the minion</param>
    [Command]
    void CmdSpawnMinion(Vector3 i, Quaternion z,int owner,int Code)
    {


        z.x = 111;
        z.eulerAngles.Set(111, 0, 0);
        Vector3 targetRot = new Vector3(111, 90, 0);
        i.y = 0.8f;
        if(owner==2)
             targetRot = new Vector3(111, 270, 0);
        var minionSpawn = (GameObject)Instantiate(retrieveMinionSeal(Code).GetComponent<MinionSeal>().minion, i, Quaternion.Euler(targetRot) );
       
        //minionSpawn.transform.rotation = Quaternion.FromToRotation(minionSpawn.transform.rotation.eulerAngles, new Vector3(111, 90, 0));
        minionSpawn.GetComponent<minion>().Owner = owner;
        NetworkServer.Spawn(minionSpawn);
        minions.Add(minionSpawn);
        MinionObjects[(int)(i.x + 8.6), (int)(i.z + 5.6)] = minionSpawn;
        RpcupdateMinions(minionSpawn,owner,minions,i);
    }

    /// <summary>
    /// Minion spawn is finalized in each client,and the arrays are updated accordingly.
    /// </summary>
    /// <param name="minions1"></param>
    /// <param name="owner"></param>
    /// <param name="namion"></param>
    /// <param name="i"></param>
    [ClientRpc]
    void RpcupdateMinions(GameObject minions1,int owner,ArrayList namion,Vector3 i)
    {
       // minions = namion;
        minions1.GetComponent<minion>().Owner = owner;
        isMinion[(int)(i.x + 8.6), (int)(i.z + 5.6)] = true;
        MinionObjects[(int)(i.x + 8.6), (int)(i.z + 5.6)] = minions1;
        // minions.Add(minions1);
        if (id == owner)
        {
            GameObject.FindGameObjectWithTag("sealcase").GetComponent<Summon>().OnMouseDown();
            gameObject.GetComponent<SealCase>().removeSeal(currentSealToSummon.gameObject);
            CmdAddManaEnergy(0, -currentSealToSummon.EnergyNeeds);
            currentSealToSummon = null;
            GetComponent<ActionMove>().CmdReduceMyRemaining(Action.SUMMON,id);
        }
    }

    [Command]
    void CmdsyncArrays()
    {

        for(int i = 0; i < ROWS; i++)
        {
            for (int j = 0; j < COLLUMNS; j++)
            {
                RpcSyncArrays(MinionObjects[i, j], i, j, isPath[i, j], isPathP[i, j]);
            }
        }
    }
    [ClientRpc]
    void RpcSyncArrays(GameObject toPlace,int i,int j,bool occupied,int owner)
    {

        if (MinionObjects[i, j] == null) { 
            MinionObjects[i, j] = toPlace;
            isPath[i, j] = occupied;
            isPathP[i, j] = owner;
            isMinion[i, j] = toPlace != null;
        }
    }


    /**
     * Apo edw kai katw tha prospathisoume na ulopoiisoume t path
     * */


     bool round(int y,int x)
    {

        if (y!=17&&isPathP[y + 1, x]==Client.Id1)
        {
            
        }
        else if(y!=0&&isPathP[y - 1, x] == Client.Id1)
        {

        }
        else if (x!=11&&isPathP[y, x+1]== Client.Id1)
        {

        }
        else if (x!=0&&isPathP[y, x-1] == Client.Id1)
        {

        }
        else
            return false;
        return true;
    }


     bool pathHighlight(int x,int y,int pathType,int rotation,bool establish)
    {

        if (establish && !helper) { 
            Animate.callGo(Animate.REDPATHESTABLISH);
            return false;
        }

        //cancelHighlight2(establish);
        if (establish)
        {
            Debug.Log("Irta");
        }
        
        if (establish&&helper&&(!checkEnergy(currentSealToSummon.EnergyNeeds) || !gameObject.GetComponent<ActionMove>().checkMyRemaining(Action.SUMMON))) {
            return false;
        }
        if (cancelHighlight2(establish))
                return true;

         helper = false;
        if (pathType == TYPE1)
        {
            helper=type1(x, y, rotation);
        }
        else if(pathType==TYPE2){
            helper = type2(x, y, rotation);

        }
        else if (pathType == TYPE3)
        {
            helper = type3(x, y, rotation);

        }
        else if (pathType==TYPE4)
        {
            helper = type4(x, y, rotation);

        }

        return false;
    }
    //    Transform[,] pathTransforms =new Transform[18,12];
    //y>0<18 x>0<11
    bool type1(int x, int y,int rotation)
    {
        
        if (rotation == 0)
        {
            if (x != 0 && x != 11 && y < 16)
            {
                if (isPath[y, x] || isPath[y, x + 1] || isPath[y, x - 1] || isPath[y + 1, x] || isPath[y + 2, x])
                    return false;
                if (!(round(y, x) || round(y, x + 1) || round(y, x - 1) || round(y + 1, x) || round(y + 2, x)))
                {
                    pathTransforms[y, x].GetComponent<Renderer>().material.color = wrongColor;
                    pathTransforms[y, x + 1].GetComponent<Renderer>().material.color = wrongColor;
                    pathTransforms[y, x - 1].GetComponent<Renderer>().material.color = wrongColor;
                    pathTransforms[y + 1, x].GetComponent<Renderer>().material.color = wrongColor;
                    pathTransforms[y + 2, x].GetComponent<Renderer>().material.color = wrongColor;
                    return false;
                }
                pathTransforms[y, x].GetComponent<Renderer>().material.color = Color.green;
                pathTransforms[y, x+1].GetComponent<Renderer>().material.color = Color.green;
                pathTransforms[y, x-1].GetComponent<Renderer>().material.color = Color.green;
                pathTransforms[y+1, x].GetComponent<Renderer>().material.color = Color.green;
                pathTransforms[y+2, x].GetComponent<Renderer>().material.color = Color.green;
            }
        }
        else if (rotation == 90)
        {
            if (y != 0 && y != 17 && x>1)
            {
                if (isPath[y, x] || isPath[y+1, x] || isPath[y-1, x] || isPath[y, x-1] || isPath[y, x-2])
                    return false;
                if (!(round(y, x) || round(y + 1, x) || round(y - 1, x) || round(y, x - 1) || round(y, x - 2)))
                {
                    pathTransforms[y, x].GetComponent<Renderer>().material.color = wrongColor;
                    pathTransforms[y + 1, x].GetComponent<Renderer>().material.color = wrongColor;
                    pathTransforms[y - 1, x].GetComponent<Renderer>().material.color = wrongColor;
                    pathTransforms[y, x - 1].GetComponent<Renderer>().material.color = wrongColor;
                    pathTransforms[y, x - 2].GetComponent<Renderer>().material.color = wrongColor;
                    return false;
                }
                pathTransforms[y, x].GetComponent<Renderer>().material.color = Color.green;
                pathTransforms[y+1, x].GetComponent<Renderer>().material.color = Color.green;
                pathTransforms[y-1, x ].GetComponent<Renderer>().material.color = Color.green;
                pathTransforms[y, x-1].GetComponent<Renderer>().material.color = Color.green;
                pathTransforms[y , x-2].GetComponent<Renderer>().material.color = Color.green;
            }
        }
        else if (rotation == 180)
        {
            if (x != 0 && x != 11 && y > 1)
            {
                if (isPath[y, x] || isPath[y - 1, x] || isPath[y - 2, x] || isPath[y, x + 1] || isPath[y, x -1])
                    return false;
                if (!(round(y, x) || round(y - 1, x) || round(y - 2, x) || round(y, x + 1) || round(y, x - 1)))
                {
                    pathTransforms[y, x].GetComponent<Renderer>().material.color = wrongColor;
                    pathTransforms[y - 1, x].GetComponent<Renderer>().material.color = wrongColor;
                    pathTransforms[y - 2, x].GetComponent<Renderer>().material.color = wrongColor;
                    pathTransforms[y, x + 1].GetComponent<Renderer>().material.color = wrongColor;
                    pathTransforms[y, x - 1].GetComponent<Renderer>().material.color = wrongColor;
                    return false;
                }
                  
                pathTransforms[y, x].GetComponent<Renderer>().material.color = Color.green;
                pathTransforms[y - 1, x].GetComponent<Renderer>().material.color = Color.green;
                pathTransforms[y- 2, x].GetComponent<Renderer>().material.color = Color.green;
                pathTransforms[y, x + 1].GetComponent<Renderer>().material.color = Color.green;
                pathTransforms[y, x - 1].GetComponent<Renderer>().material.color = Color.green;
            }
        }
        else if (rotation == 270)
        {
            if (y != 0 && y != 17 && x < 10)
            {
                if (isPath[y, x] || isPath[y + 1, x] || isPath[y - 1, x] || isPath[y, x + 1] || isPath[y, x + 2])
                    return false;
                if (!(round(y, x) || round(y + 1, x) || round(y - 1, x) || round(y, x + 1) || round(y, x + 2)))
                {
                    pathTransforms[y, x].GetComponent<Renderer>().material.color = wrongColor;
                    pathTransforms[y + 1, x].GetComponent<Renderer>().material.color = wrongColor;
                    pathTransforms[y - 1, x].GetComponent<Renderer>().material.color = wrongColor;
                    pathTransforms[y, x + 1].GetComponent<Renderer>().material.color = wrongColor;
                    pathTransforms[y, x + 2].GetComponent<Renderer>().material.color = wrongColor;
                    return false;
                }
                    
                pathTransforms[y, x].GetComponent<Renderer>().material.color = Color.green;
                pathTransforms[y + 1, x].GetComponent<Renderer>().material.color = Color.green;
                pathTransforms[y - 1, x].GetComponent<Renderer>().material.color = Color.green;
                pathTransforms[y, x + 1].GetComponent<Renderer>().material.color = Color.green;
                pathTransforms[y, x + 2].GetComponent<Renderer>().material.color = Color.green;
            }
        }
        return true;
    }

    bool type2(int x, int y, int rotation)
    {
        if (rotation == 0)
        {
            if (x > 1 && y > 1)
            {
                if (isPath[y, x] || isPath[y -1, x] || isPath[y - 2, x] || isPath[y, x - 1] || isPath[y, x - 2])
                    return false; 
                if (!(round(y, x) || round(y - 1, x) || round(y - 2, x) || round(y, x - 1) || round(y, x - 2)))
                {
                    pathTransforms[y, x].GetComponent<Renderer>().material.color = wrongColor;
                    pathTransforms[y - 1, x].GetComponent<Renderer>().material.color = wrongColor;
                    pathTransforms[y - 2, x].GetComponent<Renderer>().material.color = wrongColor;
                    pathTransforms[y, x - 2].GetComponent<Renderer>().material.color = wrongColor;
                    pathTransforms[y, x - 1].GetComponent<Renderer>().material.color = wrongColor;
                    return false;
                }
                   
                pathTransforms[y, x].GetComponent<Renderer>().material.color = Color.green;
                pathTransforms[y - 1, x].GetComponent<Renderer>().material.color = Color.green;
                pathTransforms[y - 2, x].GetComponent<Renderer>().material.color = Color.green;
                pathTransforms[y, x -2].GetComponent<Renderer>().material.color = Color.green;
                pathTransforms[y, x - 1].GetComponent<Renderer>().material.color = Color.green;
            }
        }
        else if (rotation == 90)
        {
            if (x < 10 && y > 1)
            {
                if (isPath[y, x] || isPath[y -2, x] || isPath[y - 1, x] || isPath[y, x +1] || isPath[y, x +2])
                    return false;
                if (!(round(y, x) || round(y - 2, x) || round(y - 1, x) || round(y, x + 1) || round(y, x + 2)))
                {
                    pathTransforms[y, x].GetComponent<Renderer>().material.color =wrongColor;
                    pathTransforms[y - 1, x].GetComponent<Renderer>().material.color = wrongColor;
                    pathTransforms[y - 2, x].GetComponent<Renderer>().material.color = wrongColor;
                    pathTransforms[y, x + 2].GetComponent<Renderer>().material.color = wrongColor;
                    pathTransforms[y, x + 1].GetComponent<Renderer>().material.color = wrongColor;
                    return false;
                }
                    
                pathTransforms[y, x].GetComponent<Renderer>().material.color = Color.green;
                pathTransforms[y - 1, x].GetComponent<Renderer>().material.color = Color.green;
                pathTransforms[y - 2, x].GetComponent<Renderer>().material.color = Color.green;
                pathTransforms[y, x + 2].GetComponent<Renderer>().material.color = Color.green;
                pathTransforms[y, x + 1].GetComponent<Renderer>().material.color = Color.green;
            }
        }
        else if (rotation == 180)
        {
            if (x < 10 && y < 16)
            {
                if (isPath[y, x] || isPath[y + 1, x] || isPath[y +2, x] || isPath[y, x + 1] || isPath[y, x + 2])
                    return false;
                if (!(round(y, x) || round(y + 1, x) || round(y + 2, x) || round(y, x + 1) || round(y, x + 2)))
                {
                    pathTransforms[y, x].GetComponent<Renderer>().material.color = wrongColor;
                    pathTransforms[y + 1, x].GetComponent<Renderer>().material.color = wrongColor;
                    pathTransforms[y + 2, x].GetComponent<Renderer>().material.color = wrongColor;
                    pathTransforms[y, x + 2].GetComponent<Renderer>().material.color = wrongColor;
                    pathTransforms[y, x + 1].GetComponent<Renderer>().material.color = wrongColor;
                    return false;
                }
                   
                pathTransforms[y, x].GetComponent<Renderer>().material.color = Color.green;
                pathTransforms[y + 1, x].GetComponent<Renderer>().material.color = Color.green;
                pathTransforms[y + 2, x].GetComponent<Renderer>().material.color = Color.green;
                pathTransforms[y, x + 2].GetComponent<Renderer>().material.color = Color.green;
                pathTransforms[y, x + 1].GetComponent<Renderer>().material.color = Color.green;
            }
        }
        else if (rotation == 270)
        {
            if (x > 1 && y < 16)
            {
                if (isPath[y, x] || isPath[y + 1, x] || isPath[y + 2, x] || isPath[y, x - 1] || isPath[y, x - 2])
                    return false;
                if (!(round(y, x) || round(y + 1, x) || round(y + 2, x) || round(y, x - 1) || round(y, x - 2)))
                {

                    pathTransforms[y, x].GetComponent<Renderer>().material.color = wrongColor;
                    pathTransforms[y + 1, x].GetComponent<Renderer>().material.color = wrongColor;
                    pathTransforms[y + 2, x].GetComponent<Renderer>().material.color = wrongColor;
                    pathTransforms[y, x - 2].GetComponent<Renderer>().material.color = wrongColor;
                    pathTransforms[y, x - 1].GetComponent<Renderer>().material.color = wrongColor;
                    return false;
                }
                    
                pathTransforms[y, x].GetComponent<Renderer>().material.color = Color.green;
                pathTransforms[y + 1, x].GetComponent<Renderer>().material.color = Color.green;
                pathTransforms[y + 2, x].GetComponent<Renderer>().material.color = Color.green;
                pathTransforms[y, x - 2].GetComponent<Renderer>().material.color = Color.green;
                pathTransforms[y, x - 1].GetComponent<Renderer>().material.color = Color.green;
            }
        }
        return true;
    }

    bool type3(int x, int y, int rotation)
    {
        if (rotation == 0 || rotation == 180)
        {
            if (y > 1 && y < 16)
            {
                if (isPath[y, x] || isPath[y + 1, x] || isPath[y + 2, x] || isPath[y-2, x] || isPath[y-1, x])
                    return false;
                if (!(round(y, x) || round(y + 1, x) || round(y + 2, x) || round(y - 2, x) || round(y - 1, x)))
                {
                    pathTransforms[y, x].GetComponent<Renderer>().material.color = wrongColor;
                    pathTransforms[y + 1, x].GetComponent<Renderer>().material.color = wrongColor;
                    pathTransforms[y + 2, x].GetComponent<Renderer>().material.color = wrongColor;
                    pathTransforms[y - 2, x].GetComponent<Renderer>().material.color = wrongColor;
                    pathTransforms[y - 1, x].GetComponent<Renderer>().material.color = wrongColor;
                    return false;
                }
                   
                pathTransforms[y, x].GetComponent<Renderer>().material.color = Color.green;
                pathTransforms[y + 1, x].GetComponent<Renderer>().material.color = Color.green;
                pathTransforms[y + 2, x].GetComponent<Renderer>().material.color = Color.green;
                pathTransforms[y-2, x ].GetComponent<Renderer>().material.color = Color.green;
                pathTransforms[y-1, x ].GetComponent<Renderer>().material.color = Color.green;
            }
        }
        else if (rotation == 90 || rotation == 270)
        {
            if (x > 1 && x < 10)
            {
                if (isPath[y, x] || isPath[y, x+2] || isPath[y, x+1] || isPath[y, x - 1] || isPath[y, x - 2])
                    return false;
                if (!(round(y, x) || round(y, x + 2) || round(y, x + 1) || round(y, x - 1) || round(y, x - 2)))
                {
                    pathTransforms[y, x].GetComponent<Renderer>().material.color = wrongColor;
                    pathTransforms[y, x + 2].GetComponent<Renderer>().material.color = wrongColor;
                    pathTransforms[y, x + 1].GetComponent<Renderer>().material.color = wrongColor;
                    pathTransforms[y, x - 2].GetComponent<Renderer>().material.color = wrongColor;
                    pathTransforms[y, x - 1].GetComponent<Renderer>().material.color = wrongColor;
                    return false;
                }
                    
                pathTransforms[y, x].GetComponent<Renderer>().material.color = Color.green;
                pathTransforms[y, x+2].GetComponent<Renderer>().material.color = Color.green;
                pathTransforms[y, x+1].GetComponent<Renderer>().material.color = Color.green;
                pathTransforms[y, x-2].GetComponent<Renderer>().material.color = Color.green;
                pathTransforms[y, x-1].GetComponent<Renderer>().material.color = Color.green;
            }
        }
        return true;
    }

    bool type4(int x, int y, int rotation)
    {

        if (x!=0 && x!=11 && y!=0 && y!=17)
        {
            if (isPath[y, x] || isPath[y + 1, x] || isPath[y - 1, x] || isPath[y, x - 1] || isPath[y, x + 1])
                return false;
            if (!(round(y, x) || round(y + 1, x) || round(y - 1, x) || round(y, x - 1) || round(y, x + 1)))
            {
                pathTransforms[y, x].GetComponent<Renderer>().material.color = wrongColor;
                pathTransforms[y + 1, x].GetComponent<Renderer>().material.color = wrongColor;
                pathTransforms[y - 1, x].GetComponent<Renderer>().material.color = wrongColor;
                pathTransforms[y, x + 1].GetComponent<Renderer>().material.color = wrongColor;
                pathTransforms[y, x - 1].GetComponent<Renderer>().material.color = wrongColor;
                return false;
            }
                
            pathTransforms[y, x].GetComponent<Renderer>().material.color = Color.green;
            pathTransforms[y + 1, x].GetComponent<Renderer>().material.color = Color.green;
            pathTransforms[y - 1, x].GetComponent<Renderer>().material.color = Color.green;
            pathTransforms[y, x+1].GetComponent<Renderer>().material.color = Color.green;
            pathTransforms[y , x-1].GetComponent<Renderer>().material.color = Color.green;
            
        }
        return true;
    }


    /**
     * 
     * Kapou dame enna teleiwsoume
     * 
     * */
}
