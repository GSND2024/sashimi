using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using AK;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingMenu;

    private void Start()
    {
        
    }

    public void PlayGame()
    {
        PlayerPrefs.SetInt("roomCounter1", 0);
        PlayerPrefs.SetInt("roomCounter2", 0);
        SceneManager.LoadScene("Chapter1");
    }

    public void SettingMenu()
    {
        mainMenu.SetActive(false);
        settingMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        mainMenu.SetActive(true);
        settingMenu.SetActive(false);
    }

    public void BackButton()
    {
        SceneManager.LoadScene("MainMenu");

        // Stops all gameplay sounds
        AkSoundEngine.StopAll();

        // Set the music back to "TitleScreen"
        AkSoundEngine.SetState("Music_State", "TitleScreen");

        // Call the method on AudioManager static instance
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.RestartMusic();
        }
    }

    public void Restart()
    {
        int level = PlayerPrefs.GetInt("levelCounter");

        if(level == 0)
        {
            PlayerPrefs.SetInt("roomCounter1", 0);
            PlayerPrefs.SetInt("roomCounter2", 0);
            SceneManager.LoadScene("Chapter1");
        }
        else if(level == 1)
        {
            PlayerPrefs.SetInt("roomCounter1", 0);
            PlayerPrefs.SetInt("roomCounter2", 0);
            SceneManager.LoadScene("Chapter2");
        }


    }

}
