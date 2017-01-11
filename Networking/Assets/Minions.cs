using UnityEngine;
using System.Collections;

public class Minions : MonoBehaviour {


    public const int CYCLOPS = 1;

    public GameObject cyclops;

    public GameObject giveMeMinionSealPrefab(int code)
    {
        switch (code)
        {
            case CYCLOPS:
                return cyclops;
        }

        return null;
    }
}
