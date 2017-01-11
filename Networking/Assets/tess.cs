using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class tess : NetworkBehaviour
{

    public readonly int MANAEACHTURN = 1000;
    public readonly int ENERGYEACHTURN = 1000;
    [SyncVar]
    int player;
    Text HBT;
    Image HB;
    [SyncVar]
    float hitpoint;
    float maxHitpoint = 10000;
    public Color MHcolor;
    public Color LHcolor;
    float Hratio = 1;
    Text MBT;
    Image MB;
    [SyncVar]
    float Mana;
    float Maxmana = 10000;
    public Color MMcolor;
    public Color LMcolor;
    float Mratio = 1;
    Text EBT;
    Image EB;
    [SyncVar]
    float energy;
    float maxenergy = 10000;
    public Color MEcolor;
    public Color LEcolor;
    float Eratio = 1;
    public Color Test;

    public int Player
    {
        get
        {
            return player;
        }

        set
        {
            player = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        hitpoint = maxHitpoint;
        Mana = 1000;
        energy = 1000;

    }

    void assingGodAttrubutes()
    {

    }
    public void assignTextToGods()
    {
        MB = GameObject.Find("mana" + player).GetComponent<Image>();
        MBT = GameObject.Find("manat" + player).GetComponent<Text>();
        HB = GameObject.Find("health" + player).GetComponent<Image>();
        HBT = GameObject.Find("healtht" + player).GetComponent<Text>();
        EB = GameObject.Find("energy" + player).GetComponent<Image>();
        EBT = GameObject.Find("energyt" + player).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((player == 1 || player == 2) && MB == null)
            assignTextToGods();
        if (MB == null)
            return;
        if (Mana > Maxmana)
            Mana = Maxmana;
        if (energy > maxenergy)
            energy = maxenergy;
        Hratio = hitpoint / maxHitpoint;
        Mratio = Mana / Maxmana;
        Eratio = energy / maxenergy;

        HB.rectTransform.localScale = new Vector3(Hratio, 1, 1);
        Test = Color.Lerp(LHcolor, MHcolor, Hratio);
        HB.color = Test;
        HBT.text = ((Hratio * 100).ToString("0") + '%');
        MB.rectTransform.localScale = new Vector3(Mratio, 1, 1);
        Test = Color.Lerp(LMcolor, MMcolor, Mratio);
        MB.color = Test;
        MBT.text = ((Mratio * 100).ToString("0") + '%');
        EB.rectTransform.localScale = new Vector3(Eratio, 1, 1);
        Test = Color.Lerp(LEcolor, MEcolor, Eratio);
        EB.color = Test;
        EBT.text = ((Eratio * 100).ToString("0") + '%');

    }

    public void newTurn()
    {
        Mana += MANAEACHTURN;
        energy += ENERGYEACHTURN;
    }

    public void add(int mana, int energy)
    {
        Mana += mana;
        this.energy += energy;
    }

    public void attacked(int damage)
    {
        hitpoint -= damage * 100;

        if (hitpoint <= 0)
        {
            CmdEndGame(player);
        }
    }

    [Command]
    void CmdEndGame(int loser)
    {
        //SceneManager.LoadScene("Start menu");
        RpcEndGame(loser);
    }

    [ClientRpc]
    void RpcEndGame(int loser)
    {
        SceneManager.LoadScene("Start menu");

    }

    [ClientRpc]
    public void RpcnewTurn()
    {
        Mana += MANAEACHTURN;
        energy += ENERGYEACHTURN;
    }

    public bool checkEnergy(int energyNeeded)
    {
        if (energy >= energyNeeded)
            return true;
        GameObject.Find("Warning").GetComponent<Animate>().go(Animate.ENERGYLESS);
        return false;
    }

    public bool checkMana(int manaNeeded)
    {
        if (Mana >= manaNeeded)
            return true;
        GameObject.Find("Warning").GetComponent<Animate>().go(Animate.MANALESS);
        return false;
    }
}

