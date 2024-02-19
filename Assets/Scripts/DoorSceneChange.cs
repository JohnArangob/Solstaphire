using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorSceneChange : MonoBehaviour
{
    [SerializeField] DoorManager doorManager;

    public Animator transition;
    public float transitionTime = 1f;

    private bool InDoors = false;
    // Start is called before the first frame update
    void Start()
    {
        doorManager = FindObjectOfType<DoorManager>();

        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && doorManager.Portal == false && InDoors == true)
            {
                LoadNextLevel();
            }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        InDoors = true;
    }


    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
