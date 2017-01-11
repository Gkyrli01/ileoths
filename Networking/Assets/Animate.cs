using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Animate : MonoBehaviour {

    public const string MANALESS = "Not enough Mana";
    public const string ENERGYLESS = "Not enough Energy";
    public const string MOVELESS = "Not enough Moves";
    public const string ATTACKLESS = "Not enough Attacks";
    public const string SUMMONLESS = "Not enough Summon rituals";
    public const string SPELLESS = "Not enough Spells";
    public const string REDPATHESTABLISH = "Not valid path";




    // Use this for initialization
    Animation anim;

    void Start()
    {
        anim = gameObject.GetComponent<Animation>();
    }
	void Update () {
	
	}

    /*
     * 
     * 
     * if (!state)
         {   
             anim[mcamera].speed = 1;
             anim[mcamera].time = 0;
             anim.Play();
             Client.myState = Client.SEALCASE;
         }
         else
         {

             anim[mcamera].speed = -1;
             anim[mcamera].time = anim[mcamera].length;
             anim.Play();
             Client.myState = Client.PLAYING;
         }
     * 
     * */
     public  void go(string toShow)

    {
        anim["WarningTextAnimation"].time = 0;
        anim = gameObject.GetComponent<Animation>();
        gameObject.GetComponent<Text>().text = toShow;
        anim["WarningTextAnimation"].speed = 0.5f;
        anim.Play();
    }

    public static void callGo(string toShow)
    {
        GameObject.Find("Warning").GetComponent<Animate>().go(toShow);
    }
}
