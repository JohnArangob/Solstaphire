using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject MenuWindow;
    [SerializeField] GameObject OptionsWidnow;
    [SerializeField] GameObject TrophyWindow;

    public Animator crossfade;

    private float transitionTime = 1f;

    private void Start()
    {
        OptionsWidnow.SetActive(false);
        TrophyWindow.SetActive(false);
    }

    public void Play()
    {
        LoadNextLevel();
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
        Debug.Log("Saliste de la aplicaci�n");
        Application.Quit();
    }

    void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        crossfade.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
