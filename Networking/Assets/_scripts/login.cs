using UnityEngine;
using UnityEngine.UI;

public class login : MonoBehaviour {
    //initialize variables
    public GameObject username;
    public GameObject password;
    private string Username;
    private string Password;
    private string[] lines;
    private char decrypted;
    private string decryptedp;
    public Transform lusername, lpass, ltitle, rusername, rpass, rcpass,
        remail, rtitle,wuser,wpass,euser,epass;
    public Transform tuser, wemail, rcpassw, empass;
    // Use this for initialization
    void Start() {

    }
    //log in button(clicked)
    public void emfanise(bool clicked) {
        
        // na fenonde stin othoni gia log in/register analoga
            lusername.gameObject.SetActive(clicked);
            lpass.gameObject.SetActive(clicked);
            ltitle.gameObject.SetActive(clicked);
            rusername.gameObject.SetActive(!clicked);
            rpass.gameObject.SetActive(!clicked);
            rcpass.gameObject.SetActive(!clicked);
            remail.gameObject.SetActive(!clicked);
            rtitle.gameObject.SetActive(!clicked);
            tuser.gameObject.SetActive(false);
            wemail.gameObject.SetActive(false);
            rcpassw.gameObject.SetActive(false);
            empass.gameObject.SetActive(false);
    }
    //log in button(clicked)
    public void LoginButton() { 
        
    //local variables
        bool UN = false;
        bool PW = false;
        decryptedp = "";
        
        // null check
        
        
            if (System.IO.File.Exists(Username + ".txt"))//prosorina ston xoro tou pexnidiou(.exe),sto melon database
            {
                UN = true;
                lines = System.IO.File.ReadAllLines(  Username + ".txt"); //prosorina ston xoro tou pexnidiou(.exe),sto melon database
            }
            else
            {
                wuser.gameObject.SetActive(true);//wrong-user message active
                
            }
        
        
        
            if (System.IO.File.Exists(Username + ".txt"))//prosorina ston xoro tou pexnidiou(.exe),sto melon database
            {
                int i = 1;
                foreach (char c in lines[2])//decrypt
                {
                    
                    i++;
                    char decrypted = (char)(c / i);
                    decryptedp += decrypted.ToString();
                }
                if (decryptedp == Password)
                {
                    PW = true;
                }
                else
                {
                    wpass.gameObject.SetActive(true);
               
                }

           }
        
        if(UN==true && PW == true)//succesfull login
        {
            
            Application.LoadLevel("Start Menu"); //takes screen to main menu,na dw gia Load Screen
        }
      
    }
    
  public void UpdateCheck()
    {
        getInfo();
        if (Password == "")
        {
            epass.gameObject.SetActive(true);

        }
        else if (Username == "")
        {
            euser.gameObject.SetActive(true);

        }
        else if (Password != "" && Username != "")
        {
            LoginButton();
        }

    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.Tab)) //tab move
        {
            if (username.GetComponent<InputField>().isFocused)
            {
                password.GetComponent<InputField>().Select();
            }

        }
        if (Input.GetKeyDown(KeyCode.Return)) //enter ->login
        {
            if (username.gameObject.activeInHierarchy)
                LoginButton2(); 
        }
    }
    public void getInfo()
    {
        Username = username.GetComponent<InputField>().text;
        Password = password.GetComponent<InputField>().text;
        wuser.gameObject.SetActive(false);
        wpass.gameObject.SetActive(false);
        euser.gameObject.SetActive(false);
        epass.gameObject.SetActive(false);
    }
    //elegxos gia kena stixia+emfanisi minimatos k epita kalw Login gia 
    public void LoginButton2() 
    {
        if (username.gameObject.activeInHierarchy)
        {
            getInfo();
            if (Password == "" && Username == "")
            {
                epass.gameObject.SetActive(true);
                euser.gameObject.SetActive(true);
            }

            else if (Password == "")
            {
                epass.gameObject.SetActive(true);

            }
            else if (Username == "")
            {
                euser.gameObject.SetActive(true);

            }
            else if (Password != "" && Username != "")
            {
                LoginButton();
            }
        }
    }
}
