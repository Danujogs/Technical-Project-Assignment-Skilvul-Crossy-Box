using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject attributes;


    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void SettingsButton()
    {
        settingsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void MainMenuButton()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void attributesButton()
    {
        attributes.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
