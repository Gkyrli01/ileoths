using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class boardManager : NetworkBehaviour {
    Vector3 newPosition;
    public GameObject minion;

    void Start()
    {
        newPosition = transform.position;
    }

    // Update is called once per frame
    void Update () {
        /*
        bool spawn = true;
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                newPosition = hit.point;
                newPosition.x = newPosition.x + 0.5f - newPosition.x % 1f;
                newPosition.z = newPosition.z + 0.5f - newPosition.z % 1f;
                newPosition.y = 0.5f;
                foreach (GameObject tmp in minions)
                {
                    if (tmp.transform.position == newPosition)
                        spawn = false;
                }
                if (spawn)
                {
                 
                    CmdspawnMinion(newPosition, hit.transform.rotation);
                }
            }*/
        }

        
        /*
        if(minion.GetComponent<minion>().WasClicked)
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                newPosition = hit.point;
                newPosition.x = newPosition.x+ 0.5f-newPosition.x % 1f;
                newPosition.z = newPosition.z + 0.5f - newPosition.z % 1f;
                newPosition.y = 0.5f;
                minion.transform.position = newPosition;
            }
                minion.GetComponent<minion>().WasClicked = false;
                minion.GetComponent<minion>().Deactivate();

            }*/
    }/*
    [Command]
    void CmdspawnMinion(Vector3 i,Quaternion z)
    {
         
        var minionSpawn = (GameObject)Instantiate(minion, i, z);
        NetworkServer.Spawn(minionSpawn);
        minions.Add(minionSpawn);
        
    }*/

