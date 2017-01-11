using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class collectionbuttons : MonoBehaviour
{
    public Transform lvl1a, lvl1s, lvl2a, lvl2s, lvl3a, lvl3s,agods,sgods,lathos;
    public GameObject Hide;
    public GameObject textgameobject;
    public bool HHide = true;
    public Transform lala;
    public GameObject l;
    public Transform parentToReturnTo = null;
    int number; int minionid; int godid; int i;
    int [] selected =  new int[20];
    int test,count;string mpee;
       string Minima1,Minima2,Minima3,Minima4;
    bool emf1=false, emf2=false, emf3=false, emf4=false;
    public Text Minima = null;


    public void emfanise()
    {
        test = 0;
        mpee = "Minion" + 0;
        test = PlayerPrefs.GetInt(mpee, 365);
        
        
            for (int i = 0; i < 20; i++)
            {
                mpee = "Minion" + i;
                selected[i] = PlayerPrefs.GetInt(mpee);
            }
            l = lvl1s.transform.gameObject;
            Hide = lvl1a.transform.gameObject;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < Hide.transform.childCount; j++)
                {
                    lala = Hide.gameObject.transform.GetChild(j);
                    id idd = lala.GetComponent<id>();
                    minionid = idd.id1;
                    if (minionid == selected[i])
                    {
                        lala.transform.SetParent(l.transform);
                        //break;
                    }
                }
            }
            l = lvl2s.transform.gameObject;
            Hide = lvl2a.transform.gameObject;

            for (int i = 10; i < 17; i++)
            {
                for (int j = 0; j < Hide.transform.childCount; j++)
                {
                    lala = Hide.gameObject.transform.GetChild(j);
                    id idd = lala.GetComponent<id>();
                    minionid = idd.id1;
                    if (minionid == selected[i])
                    {
                        lala.transform.SetParent(l.transform);
                        //break;
                    }
                }
            }
            l = lvl3s.transform.gameObject;
            Hide = lvl3a.transform.gameObject;

            for (int i = 17; i < 20; i++)
            {
                for (int j = 0; j < Hide.transform.childCount; j++)
                {
                    lala = Hide.gameObject.transform.GetChild(j);
                    id idd = lala.GetComponent<id>();
                    minionid = idd.id1;
                    if (minionid == selected[i])
                    {
                        lala.transform.SetParent(l.transform);
                        //break;
                    }
                }
            }
        mpee = "God" + 0;
        test = PlayerPrefs.GetInt(mpee, 365);
        if (test != 365)
        {
            l = sgods.transform.gameObject;
            Hide = agods.transform.gameObject;
            for (int j = 0; j < Hide.transform.childCount; j++)
            {
                lala = Hide.gameObject.transform.GetChild(j);
                id idd = lala.GetComponent<id>();
                minionid = idd.id1;
                if (minionid == test)
                {
                    lala.transform.SetParent(l.transform);
                    //break;
                }
            }
        }
    }
    void Start()
    {
        emfanise();
        Minima = textgameobject.GetComponent<Text>();

    }


    // Update is called once per frame
    void Update()
    {

    }
    public void takemeback(string name)
    {
        Application.LoadLevel(name);
    }


    public void lvl2(bool clicked)
    {
        lvl1a.gameObject.SetActive(!clicked);
        lvl1s.gameObject.SetActive(!clicked);
        lvl2a.gameObject.SetActive(clicked);
        lvl2s.gameObject.SetActive(clicked);
        lvl3a.gameObject.SetActive(!clicked);
        lvl3s.gameObject.SetActive(!clicked);
        agods.gameObject.SetActive(!clicked);
        sgods.gameObject.SetActive(!clicked);
        Minima.text = Minima2;
        lathos.gameObject.SetActive(emf2);
    }
    public void lvl3(bool clicked)
    {
        lvl1a.gameObject.SetActive(!clicked);
        lvl1s.gameObject.SetActive(!clicked);
        lvl2a.gameObject.SetActive(!clicked);
        lvl2s.gameObject.SetActive(!clicked);
        lvl3a.gameObject.SetActive(clicked);
        lvl3s.gameObject.SetActive(clicked);
        Minima.text = Minima3;
        lathos.gameObject.SetActive(emf3);
        agods.gameObject.SetActive(!clicked);
        sgods.gameObject.SetActive(!clicked);

    }
    public void lvl1(bool clicked)
    {
        lvl1a.gameObject.SetActive(clicked);
        lvl1s.gameObject.SetActive(clicked);
        lvl2a.gameObject.SetActive(!clicked);
        lvl2s.gameObject.SetActive(!clicked);
        lvl3a.gameObject.SetActive(!clicked);
        lvl3s.gameObject.SetActive(!clicked);
        agods.gameObject.SetActive(!clicked);
        sgods.gameObject.SetActive(!clicked);
        Minima.text = Minima1;
        lathos.gameObject.SetActive(emf1);
    }
    public void gods(bool clicked)
    {
        agods.gameObject.SetActive(clicked);
        sgods.gameObject.SetActive(clicked);
        lvl1a.gameObject.SetActive(!clicked);
        lvl1s.gameObject.SetActive(!clicked);
        lvl2a.gameObject.SetActive(!clicked);
        lvl2s.gameObject.SetActive(!clicked);
        lvl3a.gameObject.SetActive(!clicked);
        lvl3s.gameObject.SetActive(!clicked);         
            Minima.text = Minima4;        
        lathos.gameObject.SetActive(emf4);
    }
    public void randomNum(bool clicked)
    {
        if (lvl1a.transform.gameObject.activeInHierarchy)
        {
          
            l = GameObject.Find("selected");
                Hide = GameObject.Find("all cards");
            int x = l.transform.childCount;
            int y = Hide.transform.childCount;
            while (x<10)
                {
                    number = Random.Range(0, y-1);
                    lala = Hide.gameObject.transform.GetChild(number);
                    lala.transform.SetParent(l.transform);
                y--;
                x++;
                }
            emf1 = false;
            lathos.gameObject.SetActive(emf1);
        }
        else if (lvl2a.gameObject.activeInHierarchy)
        {
            
            
                l = GameObject.Find("selected2");
                Hide = GameObject.Find("all cards2");
            int x = l.transform.childCount;
            int y = Hide.transform.childCount;
            while (x < 7)
            {
                number = Random.Range(0, y - 1);
                lala = Hide.gameObject.transform.GetChild(number);
                lala.transform.SetParent(l.transform);
                y--;
                x++;
            }
            emf2 = false;
            lathos.gameObject.SetActive(emf2);
        }
        else if (lvl3a.gameObject.activeInHierarchy)
        {
            
            
                l = GameObject.Find("selected3");
                Hide = GameObject.Find("all cards3");
            int x = l.transform.childCount;
            int y = Hide.transform.childCount;
            while (x < 3)
            {
                number = Random.Range(0, y - 1);
                lala = Hide.gameObject.transform.GetChild(number);
                lala.transform.SetParent(l.transform);
                y--;
                x++;
            }
            emf3= false;
            lathos.gameObject.SetActive(emf3);
        }
        else if (agods.transform.gameObject.activeInHierarchy)
        {

            l = GameObject.Find("Sgod");
            Hide = GameObject.Find("allgods");
            int x = l.transform.childCount;
            int y = Hide.transform.childCount;
            while (x < 1)
            {
                number = Random.Range(0, y - 1);
                lala = Hide.gameObject.transform.GetChild(number);
                lala.transform.SetParent(l.transform);
                y--;
                x++;
            }
            emf4 = false;
            lathos.gameObject.SetActive(emf4);
        }
    }

 /*   public void resetb()
    {

        if (lvl1a.gameObject.activeInHierarchy)
        {
            l = GameObject.Find("selected");
            Hide = GameObject.Find("all cards");
            lala = l.gameObject.transform.GetChild(0);
            while (lala != null)
            {
                lala.transform.SetParent(Hide.transform);
                lala = l.gameObject.transform.GetChild(0);
            }
        }
        if (lvl2a.gameObject.activeInHierarchy)
        {
            l = GameObject.Find("selected2");
            Hide = GameObject.Find("all cards2");
            lala = l.gameObject.transform.GetChild(0);
            while (lala != null)
            {
                lala.transform.SetParent(Hide.transform);
                lala = l.gameObject.transform.GetChild(0);
            }
        }
        if (lvl3a.gameObject.activeInHierarchy)
        {
            l = GameObject.Find("selected3");
            Hide = GameObject.Find("all cards3");
            lala = l.gameObject.transform.GetChild(0);
            while (lala != null)
            {
                lala.transform.SetParent(Hide.transform);
                lala = l.gameObject.transform.GetChild(0);
            }
        }

    }
    */
    public void resetb(bool clicked)
    {
       
        if (lvl1a.gameObject.activeInHierarchy)
        {
            l = GameObject.Find("selected");
            Hide = GameObject.Find("all cards");
            count= l.transform.childCount;
            if (count == 0)
            {
                lala = null;
            }
            else
            {
                lala = l.gameObject.transform.GetChild(0);
                for (int i = 0; i < 10; i++)
                {
                    mpee = "Minion" + i;
                    PlayerPrefs.SetInt(mpee, 365);
                }
            }
            while (lala!=null)
            {
                lala.transform.SetParent(Hide.transform);
                if (l.transform.childCount > 0)
                {
                    lala = l.gameObject.transform.GetChild(0);
                }else
                {

                    lala = null;
                }
                
            }
            emf1= false;
            lathos.gameObject.SetActive(emf1);
        }
       else if (lvl2a.gameObject.activeInHierarchy)
        {
            l = GameObject.Find("selected2");
            Hide = GameObject.Find("all cards2");
            count = l.transform.childCount;
            if (count == 0)
            {
                lala = null;
            }
            else
            {
                lala = l.gameObject.transform.GetChild(0);
                for (int i = 10; i < 17; i++)
                {
                    mpee = "Minion" + i;
                    PlayerPrefs.SetInt(mpee, 365);
                }
            }
            
            while (lala != null)
            {
                lala.transform.SetParent(Hide.transform);
                count = l.transform.childCount;
                if (count == 0)
                {
                    lala = null;
                }
                else
                {
                    lala = l.gameObject.transform.GetChild(0);
                }
            }
            emf2 = false;
            lathos.gameObject.SetActive(emf2);
        }
       else if (lvl3a.gameObject.activeInHierarchy)
        {
            l = GameObject.Find("selected3");
            Hide = GameObject.Find("all cards3");
            count = l.transform.childCount;
            if (count == 0)
            {
                lala = null;
            }
            else
            {
                lala = l.gameObject.transform.GetChild(0);
                for (int i = 17; i < 20; i++)
                {
                    mpee = "Minion" + i;
                    PlayerPrefs.SetInt(mpee, 365);
                }

            }

            while (lala != null)
            {
                lala.transform.SetParent(Hide.transform);
                count = l.transform.childCount;
                if (count == 0)
                {
                    lala = null;
                }
                else
                {
                    lala = l.gameObject.transform.GetChild(0);
                }
            }
            emf3 = false;
            lathos.gameObject.SetActive(emf3);
        }
        else if (agods.transform.gameObject.activeInHierarchy)
        {

            l = GameObject.Find("Sgod");
            Hide = GameObject.Find("allgods");
            int x = l.transform.childCount;
           
            if (x ==1)
            {
                lala = l.gameObject.transform.GetChild(0);
                lala.transform.SetParent(Hide.transform);
               
                    mpee = "God" + 0;
                    PlayerPrefs.SetInt(mpee, 365);
                
            }
            emf4 = false;
            lathos.gameObject.SetActive(emf4);
        }

    }


    public void LongPass(bool clicked)
    {
        l = lvl1s.transform.gameObject;
        int x = l.transform.childCount;
        int y;
        if (x != 10)
        {
            Hide =lvl1a.transform.gameObject;
            y = Hide.transform.childCount;
            while (x < 10)
            {
                number = Random.Range(0, y - 1);
                lala = Hide.gameObject.transform.GetChild(number);
                lala.transform.SetParent(l.transform);
                y--;
                x++;
            }
        }
        for (int i = 0; i < 10; i++)
        {
            lala = l.gameObject.transform.GetChild(i);

            id idd1 = lala.GetComponent<id>();
            minionid = idd1.id1;
            mpee = "Minion" + i;
            PlayerPrefs.SetInt(mpee, minionid);
            
        }

        l = lvl2s.transform.gameObject;

        x = l.transform.childCount;
        if (x != 7)
        {
            Hide = lvl2a.transform.gameObject;
            y = Hide.transform.childCount;

            while (x < 7)
            {
                number = Random.Range(0, y - 1);
                lala = Hide.gameObject.transform.GetChild(number);
                lala.transform.SetParent(l.transform);
                y--;
                x++;
            }
        }
        for (int i = 10; i < 17; i++)
        {
            lala = l.gameObject.transform.GetChild(i-10);

            id idd1 = lala.GetComponent<id>();
            minionid = idd1.id1;
            mpee = "Minion" + i;
            PlayerPrefs.SetInt(mpee, minionid);
          
        }
        l = lvl3s.transform.gameObject;
        x = l.transform.childCount;
        if (x != 3)
        {
            Hide = lvl3a.transform.gameObject;
            y = Hide.transform.childCount;

            while (x < 3)
            {
                number = Random.Range(0, y - 1);
                lala = Hide.gameObject.transform.GetChild(number);
                lala.transform.SetParent(l.transform);
                y--;
                x++;
            }
        }
        for (int i = 17; i < 20; i++)
        {
            lala = l.gameObject.transform.GetChild(i-17);
            id idd1 = lala.GetComponent<id>();
            minionid = idd1.id1;
            mpee = "Minion" + i;
            PlayerPrefs.SetInt(mpee, minionid);
            
        }
        l = sgods.transform.gameObject;
        x = l.transform.childCount;
         
        if (x != 1)
        {
            Hide = lvl1a.transform.gameObject;
            y = Hide.transform.childCount;
            while (x < 1)
            {
                number = Random.Range(0, y - 1);
                lala = Hide.gameObject.transform.GetChild(number);
                lala.transform.SetParent(l.transform);
                y--;
                x++;
            }
        }
        
            lala = l.gameObject.transform.GetChild(0);
            id idd = lala.GetComponent<id>();
            minionid = idd.id1;
            mpee = "God" + 0;
            PlayerPrefs.SetInt(mpee, minionid);
    }//tha kalesti apo tin play sto main menu ;)
    

    public void SaveMe(bool clicked)
    {
        emf1 = false; emf3 = false;
         emf2 = false; emf4 = false;
        l = lvl1s.transform.gameObject;
        int x = l.transform.childCount;
        int y;
        if (x != 10)
        {
                Minima1 = "You need " + (10 - x )+ " lvl1 monsters for your collection";
            emf1 = true;
                }
        for (int i = 0; i < x; i++)
        {
            lala = l.gameObject.transform.GetChild(i);
            id idd1 = lala.GetComponent<id>();
            minionid = idd1.id1;
            mpee = "Minion" + i;
            PlayerPrefs.SetInt(mpee, minionid);

        }
        l = lvl2s.transform.gameObject;
         x = l.transform.childCount;
        if (x != 7)
        {
            
                Minima2= "You need " + (7 - x) + " lvl2 monsters for your collection";
               
            emf2 = true;
        }
        for (int i = 10; i < x+10; i++)
        {
            lala = l.gameObject.transform.GetChild(i-10);
            id idd1 = lala.GetComponent<id>();
            minionid = idd1.id1;
            mpee = "Minion" + i;
            PlayerPrefs.SetInt(mpee, minionid);

        }
        l = lvl3s.transform.gameObject;
        x = l.transform.childCount;
        if (x != 3)
        {
                Minima3 = "You need " + (3 - x) + " lvl3 monsters for your collection";
                
                emf3 = true;
            
        }
        for (int i = 17; i < x + 17; i++)
        {
            lala = l.gameObject.transform.GetChild(i - 17);
            id idd1 = lala.GetComponent<id>();
            minionid = idd1.id1;
            mpee = "Minion" + i;
            PlayerPrefs.SetInt(mpee, minionid);
        }
        l = sgods.transform.gameObject;
        x = l.transform.childCount;

        if (x != 1)
        {
            Minima4 = "Choose a god";
            emf4 = true;
        }

        else
        {
            lala = l.gameObject.transform.GetChild(0);
            id idd = lala.GetComponent<id>();
            minionid = idd.id1;
            mpee = "God" + 0;
            PlayerPrefs.SetInt(mpee, minionid);
        }
        if (sgods.gameObject.activeInHierarchy)
        {
            Minima.text = Minima4;
            lathos.gameObject.SetActive(emf4);
        }
        else if (lvl1a.gameObject.activeInHierarchy)
        {
            Minima.text = Minima1;
            lathos.gameObject.SetActive(emf1);
        }
        else if (lvl2a.gameObject.activeInHierarchy)
        {
            Minima.text = Minima2;
            lathos.gameObject.SetActive(emf2);
        }
        else if (lvl3a.gameObject.activeInHierarchy)
        {
            Minima.text = Minima3;
            lathos.gameObject.SetActive(emf3);
        }
    }
     
}
    


    



    