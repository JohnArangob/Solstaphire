using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] Image healthBar;
    [SerializeField] float healthAmount = 100;

    public float ArrowDamage = 5f;

    [SerializeField] float healingAmount = 2f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.UpArrow)){

            Heal();
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

}
