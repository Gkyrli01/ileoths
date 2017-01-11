using UnityEngine;
using UnityEngine.UI;
using System;
public class register : MonoBehaviour {
    //variables
    public GameObject username;
    public GameObject password;
    public GameObject email;
    public GameObject confpassword;
    private string Username;
    private string Password;
    private string Confpassword;
    private string Email;
    private string form;
    private bool EmailValid = false;
    public Transform lusername, lpass, ltitle, rusername, rpass, rcpass,
        remail, rtitle, tuser, wemail, rcpassw, empass;
    public Transform wuser, wpass, euser, epass;

    private string[] characters = {"Q","W","E","R","T","Y","U","I","O","P","A","S","D","F","G",
                                    "H","J","K","L","Z","X","C","V","B","N","M","1","2","3","4",
                                    "5","6","7","8","9","0","q","w","e","r","t","y","u","i","o","p",
                                    "a","s","d","f","g","h","j","k","l","z","x","c","v","b","n","m","-","_"};
    // Use this for initialization
    void Start() {

    }
    
    public void emfanise(bool clicked)
    {
        
            lusername.gameObject.SetActive(!clicked);
            lpass.gameObject.SetActive(!clicked);
            ltitle.gameObject.SetActive(!clicked);
            rusername.gameObject.SetActive(clicked);
            rpass.gameObject.SetActive(clicked);
            rcpass.gameObject.SetActive(clicked);
            remail.gameObject.SetActive(clicked);
            rtitle.gameObject.SetActive(clicked);
            wuser.gameObject.SetActive(false);
            wpass.gameObject.SetActive(false);
            euser.gameObject.SetActive(false);
            epass.gameObject.SetActive(false);
    }
    public void RegisterButton()
    {
        //local variables
        bool UN = false;
        bool EM = false;
        bool PS = false;
        bool PSC = false;

        if (!System.IO.File.Exists( Username + ".txt"))
        {
            UN = true;
        }
        else
        {

            tuser.gameObject.SetActive(true);
        }
        EmailValidation();
        if (EmailValid)
        {
            if (Email.Contains("@"))
            {
                if (Email.Contains("."))
                {
                    EM = true;
                }
                else
                {
                    wemail.gameObject.SetActive(true);
                }
            }
            else {
                wemail.gameObject.SetActive(true);
            }
        }
        else
        {
            wemail.gameObject.SetActive(true);
        }
        if (Password.Length > 5)
        {
            if (Password == Confpassword)
            {
                PSC = true;
                PS = true;
                
            }
            else
            {
                rcpassw.gameObject.SetActive(true);
                
                PS = true;
            }

        }
        else
        {
            empass.gameObject.SetActive(true);
        }

        if (UN == true && PSC == true && PS == true && EM == true)//encrypt
        {
            bool clear = true;
            int i = 1;
            foreach (char c in Password)
            {
                if (clear)
                {
                    Password = "";
                    clear = false;
                }
                i++;
                char Encrypted = (char)(c * i);
                Password += Encrypted.ToString();
            }
            form = (Username + Environment.NewLine + Email + Environment.NewLine + Password);
            System.IO.File.WriteAllText(Username + ".txt", form);
            getorclear(0);
            Application.LoadLevel("Start Menu");
        }
    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.Tab))
        {

            if (username.GetComponent<InputField>().isFocused)
            {
                email.GetComponent<InputField>().Select();
            }
            if (email.GetComponent<InputField>().isFocused)
            {
                password.GetComponent<InputField>().Select();
            }
            if (password.GetComponent<InputField>().isFocused)
            {
                confpassword.GetComponent<InputField>().Select();
            }
            if (confpassword.GetComponent<InputField>().isFocused)
            {
                username.GetComponent<InputField>().Select();
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (username.gameObject.activeInHierarchy)
            {

                getorclear(1);
                if (Password != "" && Username != "" && Confpassword != "" && Email != "")
                {
                    RegisterButton();
                }
                
            }
        }
    }
    public void getorclear(int b)
    {
        if (b == 1)
        {
            Username = username.GetComponent<InputField>().text;
            Password = password.GetComponent<InputField>().text;
            Confpassword = confpassword.GetComponent<InputField>().text;
            Email = email.GetComponent<InputField>().text;
        }else
        {
            username.GetComponent<InputField>().text = "";
            password.GetComponent<InputField>().text = "";
            confpassword.GetComponent<InputField>().text = "";
            email.GetComponent<InputField>().text = "";
        }
        tuser.gameObject.SetActive(false);
        wemail.gameObject.SetActive(false);
        rcpassw.gameObject.SetActive(false);
        empass.gameObject.SetActive(false);
    }
    public void reg2()
    {
        if (username.gameObject.activeInHierarchy)
        {
            getorclear(1);

            if (Password != "" && Username != "" && Confpassword != "" && Email != "")
            {
                RegisterButton();
            }
        }
    }
    void EmailValidation()
    {
        bool SW = false;
        bool EW = false;
        for (int i = 0; i < characters.Length; i++)
        {
            if (Email.StartsWith(characters[i]))
            {
                SW = true;
            }
        }
        for (int i = 0; i < characters.Length; i++)
        {
            if (Email.EndsWith(characters[i]))
            {
                EW = true;
            }
        }
        if (SW == true && EW == true)
        {
            EmailValid = true;
        } else
        {
            EmailValid = false;
        }
    }
}
