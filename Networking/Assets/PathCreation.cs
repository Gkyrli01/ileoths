using UnityEngine;
using UnityEngine.Networking;


public class PathCreation : NetworkBehaviour {



    public readonly int TYPE1 = 1;
    public readonly int TYPE2 = 2;
    public readonly int TYPE3 = 3;
    public readonly int TYPE4 = 4;



    public GameObject cell;
     Transform[,] pathTransforms = new Transform[18, 12];


    // Use this for initialization
    void Start () {
        RaycastHit hit = new RaycastHit();
        Vector3 tmp = new Vector3(0.0f,0.5f,0.0f);
        hit.point = tmp;
      //  if (isLocalPlayer)
           // CmdSpawnPath(hit, 0, true, 1);
	}

    

    // Update is called once per frame
    void Update () {

        /*Otan o paixtis thelei na kalesei ena minion mia metavliti se auti tin klasi allazei
         * kai mesa sto update kaleitai h sunartisi me tis epiloges monopatiwn p exeis stin diathesi tou
         * o paiktis.
         * Uparxei grafiko sto paixnidi p deixnei p tha vrisketai to monopati me highlight(prasino an einai
         * efikto,kokkino adunato).Tha uparxei dunatotita allagis proepsikopisis monopatiou mesw t tab gia paradeigma
         * Otan o paiktis epilegei tin topothesia tou monopatiou ginetai confirm i kinisi stelnontas ston server tin entoli spawn
         * gia na dimiourgithei to celli.
         * 
         * Pros to paron tha uparxei mono to kalesma tou server gia to path p tha dimiorgisei.
         * 
         * Tags=Cell gia to kelli p tha dimiourgeitai.
         * 
         * https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnMouseOver.html
         * Gia to hover
         * 
         * file:///C:/Program%20Files/Unity/Editor/Data/Documentation/en/Manual/SupportedEvents.html
         * Allos tropos pithanon
         * */

    }

    /*
     *owner mesw tou tag
     * */

    [Command]
    void CmdSpawnPath(RaycastHit startPoint,int pathType,bool test,int owner)
    {
        if (test)
        {

            Quaternion t = new Quaternion();
            Vector3 tmp = startPoint.point;
            tmp.y = 0.5f;
            var minionSpawn = (GameObject)Instantiate(cell, tmp,t);
            minionSpawn.GetComponent<Cell>().Owner = owner;
            NetworkServer.Spawn(minionSpawn);
        }
    }


    void pathHighlight(int x, int y, int pathType)
    {

    }




}
