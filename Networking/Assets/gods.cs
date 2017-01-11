using UnityEngine;
using System.Collections;

public class gods : MonoBehaviour {

    public const  int HERMES = 1;

    public GameObject hermes;

    public GameObject giveMeGodPrefab(int code)
    {
        switch (code)
        {
            case HERMES:
                return hermes;
        }

        return null;
    }

}
