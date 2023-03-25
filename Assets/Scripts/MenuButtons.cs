using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsMenu;

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Settings()
    {
        settingsMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void SettingsBack()
    {
        mainMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
