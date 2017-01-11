using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class health : NetworkBehaviour
{
    public Image currentHealthbar;
   // public Text ratioText;
    public float hitpoint;
    public float maxHitpoint;
    public Color Mcolor;
    public Color Lcolor;
    public Color Test;
    public int attack;
    public string Name;
    public float ratio = 1;
    [SyncVar]
    int changed = -1;
    public GameObject[] objs;
    void Start()
    {
        int i = 0;
        hitpoint = maxHitpoint;

        /*foreach (Transform child in transform)
        {
            Debug.Log(child.position);
            foreach(Transform child1 in child)
            {
                if (i == 0) ;
                    //currentHealthbar = child1.GetComponent<Image>();
                if (i == 1)
                {
                    currentHealthbar = child1.GetChild(0).GetComponent<Image>();
                    //ratioText = child1.GetChild(0).GetComponent<Image>(); 

                }
                i++;
            }
        }*/

        //currentHealthbar = gameObject.transform.
        UpdateHealthbar();

    }
    private bool UpdateHealthbar()
    {
        ratio = hitpoint/maxHitpoint;// hitpoint / maxHitpoint;
        currentHealthbar.rectTransform.localScale = new Vector3(ratio, 1, 1);
        //ratioText.text = ((ratio * 100).ToString("0") + '%');
        Test = Color.Lerp(Lcolor, Mcolor, ratio);
        currentHealthbar.color = Test;
        return false;


    }
    void Update()
    {
        
        Test = Color.Lerp(Lcolor, Mcolor, ratio);
        currentHealthbar.color = Test;
        
    }
    public bool TakeDamage(float damage)
    {

        hitpoint -= damage;
        changed = 2;

        if (hitpoint <= 0)
        {
            Destroy(gameObject);
            return true;
        }
        return UpdateHealthbar();
    }
   

}