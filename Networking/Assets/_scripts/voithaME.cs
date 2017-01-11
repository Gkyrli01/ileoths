using UnityEngine;

public class voithaME : MonoBehaviour {
    public Transform hpanel, gpanel, mpanel, rpanel;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void GMenu(bool clicked)
    {      
            gpanel.gameObject.SetActive(clicked);
            hpanel.gameObject.SetActive(!clicked);
            mpanel.gameObject.SetActive(false);
            rpanel.gameObject.SetActive(false);
    }
    public void RMenu(bool clicked)
    {
            rpanel.gameObject.SetActive(clicked);
            hpanel.gameObject.SetActive(!clicked);
            mpanel.gameObject.SetActive(false);
            gpanel.gameObject.SetActive(false);
    }
    public void MMenu(bool clicked)
    {    
            mpanel.gameObject.SetActive(clicked);
            hpanel.gameObject.SetActive(!clicked);
            gpanel.gameObject.SetActive(false);
    }

}
