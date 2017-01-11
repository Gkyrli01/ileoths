using UnityEngine;

public class Magkies : MonoBehaviour {
    public GameObject[] Hide;
    // Use this for initialization
    public bool HHide = true;
    public Transform lala;
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.H))
        {
         HHide = !HHide;
        Hide = GameObject.FindGameObjectsWithTag("minion");
           
            foreach (GameObject x in Hide)
            {
                lala = x.gameObject.transform.GetChild(0);
                lala.gameObject.SetActive(HHide);
            }
        }
    }

    }

