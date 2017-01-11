using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

using System.Collections;

public class SealCase : NetworkBehaviour {


    const int CASESIZE = 4;
    int owner;
    [SyncVar]
    int count = 0;
    int nextFree = 0;
    bool[] places = new bool[4];
    List<Vector3> positions = new List<Vector3>();
    //List<Quaternion> rotations = new List<Quaternion>();

    // Use this for initialization
    void Start () {
        owner = Client.Id1;
        for(int i = 0; i < CASESIZE; i++)
        {
            if(owner==1)
            positions.Add(new Vector3(0.5f+(i + 1), 1.1f, -9));
            else
                positions.Add(new Vector3(-0.5f-(i + 1), 1.1f, -9));
        }
    }

    bool free()
    {
        for(int i = 0; i < places.Length; i++)
        {
            if (!places[i])
            {
                nextFree = i;
                return true;
            }
        }
        return false;
    }
    public bool newMinionSeal(GameObject minionSeal,int code)
    {
        if (!isLocalPlayer)
            return false;
        if (!free())
            return false;
        if (owner != Client.Id1)
            return false;
        GameObject ok=(GameObject) Instantiate(minionSeal, positions[nextFree], minionSeal.transform.rotation);
        ok.GetComponent<MinionSeal>().Code = code;
        CmdSealCount(+1);
        places[nextFree] = true;
        return true;
    }

    public void removeSeal(GameObject minionSeal)
    {
        Destroy(minionSeal);
        CmdSealCount(-1);
        places[positions.IndexOf(minionSeal.transform.position)] = false;
    }

    [Command]
    void CmdSealCount(int num)
    {
        count+=num;
    }

}
