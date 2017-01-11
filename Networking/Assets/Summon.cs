using UnityEngine;
using System.Collections;

public class Summon : MonoBehaviour {

   // bool state = false;
    //Animation anim;
    //string mcamera="";
    private static bool makingPath = false;

    public static bool MakingPath
    {
        get
        {
            return makingPath;
        }

        set
        {
            makingPath = value;
        }
    }

    void Start()
    {

    }

    
    public void OnMouseOver2()
    {
        Client.customAnimating = Client.FRAMES;
        if (Client.myState == Client.PLAYING)
        {
            if (Client.secondaryState == Client.NORMAL)
            {
                Client.secondaryState = Client.ABOVE;
            }
            else if (Client.secondaryState == Client.ABOVE)
            {
                Client.secondaryState = Client.NORMAL;
            }
            else
            {
                Client.secondaryState = Client.ABOVE;
            }

            return;
        }
        if (Client.myState == Client.SUMMON)
        {
            Client.myState = Client.PLAYING;
            Client.enableCamera(Client.PLAYER);
        }
    }

    public void OnMouseDown()
    {
        playerSetup.refresh = true;
        if (makingPath)
        {
            Client.enableCamera(Client.PLAYER);
            Client.myState = Client.SEALCASE;
            makingPath = false;
            return;
        }
            if (Client.myState == Client.SEALCASE)
            {
                Client.myState = Client.PLAYING;
            Client.secondaryState = Client.NORMAL;
            }
            else
            {
                Client.myState = Client.SEALCASE;
            }
        Client.customAnimating = Client.FRAMES;
        //GameObject.Find("SealCase1Pos").transform;
        /*
         if (mcamera.Equals("")&&GameObject.Find("GameInstanceManager").GetComponent<Client>().Id == 1)
         {
             anim = GameObject.Find("Player1Camera").GetComponent<Animation>();
             mcamera = "player1Camera";
         }
         else if(mcamera.Equals(""))
         {
             anim = GameObject.Find("Player2Camera").GetComponent<Animation>();
             mcamera = "player2Camera";
         }
         if (!state)
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

         state = !state;*/
    }
}
