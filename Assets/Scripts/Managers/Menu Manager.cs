using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject MenuWindow;
    [SerializeField] GameObject OptionsWidnow;
    [SerializeField] GameObject TrophyWindow;

    private void Start()
    {
        OptionsWidnow.SetActive(false);
        TrophyWindow.SetActive(false);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Options()
    {
        OptionsWidnow.SetActive(true);
        MenuWindow.SetActive(false);
    }

    public void Trophy()
    {
        TrophyWindow.SetActive(true);
        MenuWindow.SetActive(false);
    }

    public void GoBack()
    {
        OptionsWidnow.SetActive(false);
        TrophyWindow.SetActive(false);
        MenuWindow.SetActive(true);
    }

    public void Quit()
    {
        Debug.Log("Saliste de la aplicación");
        Application.Quit();
    }
}
