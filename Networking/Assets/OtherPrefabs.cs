using UnityEngine;
using System.Collections;

public class OtherPrefabs : MonoBehaviour {


    public const int PLAYERSETUPTEXT = 1;
    public const int SEALCASE = 2;


    public GameObject playerSetupText;
    public GameObject sealCase;


    public GameObject giveMeMinionSealPrefab(int code)
    {
        switch (code)
        {
            case PLAYERSETUPTEXT:
                return playerSetupText;
            case SEALCASE:
                return sealCase;
        }

        return null;
    }
}
