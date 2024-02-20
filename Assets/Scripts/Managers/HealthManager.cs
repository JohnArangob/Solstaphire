using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.TimeZoneInfo;

public class HealthManager : MonoBehaviour
{
    [SerializeField] Image healthBar;
    [SerializeField] float healthAmount = 100;

    public float ArrowDamage;

    [SerializeField] float healingAmount = 2f;


    [SerializeField] DoorSceneChange DSC;

    [SerializeField] PlayerMovement pMovement;

    // Start is called before the first frame update
    void Start()
    {
        DSC = FindObjectOfType<DoorSceneChange>();
        pMovement = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.UpArrow)){

            Heal();
        }

        if(healthAmount <= 0)
        {
            Death();
        }
    }

    public void TakeDamageArrow()
    {

        healthAmount -= ArrowDamage;
        healthBar.fillAmount = healthAmount / 100f;
    }

    public void Heal(){

        healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);

        healthBar.fillAmount = healthAmount / 100f;
    }


    public void Death() 
    {

        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        DSC.transition.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(levelIndex);
        
    }
}
