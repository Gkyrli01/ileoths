using UnityEngine;
using System.Collections;

public class LoadingAnimation : MonoBehaviour {

    int counter = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (counter == 10)
        {
            transform.rotation = playerSetup.rotate(transform.rotation.eulerAngles, 0, 0, 22.5f);
            counter = 0;
        }
        else counter++;
            
	}
}
