using UnityEngine;

public class EndTurn : MonoBehaviour
{

    bool wasClicked;
    public bool WasClicked
    {
        get
        {
            return wasClicked;
        }

        set
        {
            wasClicked = value;
        }
    }

    public GameObject manager;


    public void OnMouseDown()
    {
        WasClicked = true;
    }

}
