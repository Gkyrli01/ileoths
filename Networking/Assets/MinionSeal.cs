using UnityEngine;
using System.Collections;

public class MinionSeal : MonoBehaviour {

    int energyNeeds = 500;
    public GameObject minion;
    int code;
    public int EnergyNeeds
    {
        get
        {
            return energyNeeds;
        }

        set
        {
            energyNeeds = value;
        }
    }

    public int Code
    {
        get
        {
            return code;
        }

        set
        {
            code = value;
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    /*public bool checkEnergy(GameObject god)
    {
        god.GetComponent<tess>().checkEnergy(energyNeeds);
    }*/
    public Camera toSummonCamera()
    {
       return Client.enableCamera(Client.SUMMON);
    }


}
