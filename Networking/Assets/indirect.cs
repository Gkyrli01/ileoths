using UnityEngine;

public class indirect : MonoBehaviour {


    private bool accept = false;
    private bool decline = false;

    public bool Accept
    {
        get
        {
            return accept;
        }

        set
        {
            accept = value;

            FindObject(GameObject.Find("Image"), "Accept").SetActive(false);
            FindObject(GameObject.Find("Image"), "Decline").SetActive(false);

        }
    }

    public bool Decline
    {
        get
        {
            return decline;
        }

        set
        {
            decline = value;
            FindObject(GameObject.Find("Image"), "Accept").SetActive(false);
            FindObject(GameObject.Find("Image"), "Decline").SetActive(false);


        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void useMe()
    {
        FindObject(GameObject.Find("Image"), "Searching").SetActive(true);
        FindObject(GameObject.Find("Image"), "SearchingText").SetActive(true);
        //FindObject(GameObject.Find("Image"), "LobbyWay").GetComponent<network1>().FindInternetMatch("");
      
        GameObject.Find("LobbyWay").GetComponent<network1>().FindInternetMatch("");
    }


    public static GameObject FindObject( GameObject parent, string name)
    {
        Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trs)
        {
            if (t.name == name)
            {
                return t.gameObject;
            }
        }
        return null;
    }
}
