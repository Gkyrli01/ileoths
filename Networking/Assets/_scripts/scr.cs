using UnityEngine;
using UnityEngine.UI;

public class scr : MonoBehaviour {
    public Transform MainMenu, optionMenu,helpmenu,gpanel,hpanel,mpanel,rpanel;
    public Slider volumeSlider;
    public AudioSource volumeAudio;
    public void LoadScene(string name)
    {
        Application.LoadLevel(name);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void OptionMenu(bool clicked)
    {     
            optionMenu.gameObject.SetActive(clicked);
            MainMenu.gameObject.SetActive(!clicked);
            helpmenu.gameObject.SetActive(false);
    }
    public void VolumeControl(float volumecontrol)
    {
        AudioListener.volume = volumeSlider.value;
    }
    public void HelpMenu(bool clicked)
    {  
            helpmenu.gameObject.SetActive(clicked);
            MainMenu.gameObject.SetActive(!clicked);
            optionMenu.gameObject.SetActive(false);
            gpanel.gameObject.SetActive(false);
            hpanel.gameObject.SetActive(clicked);
            mpanel.gameObject.SetActive(false);
            rpanel.gameObject.SetActive(false);
    }
}
