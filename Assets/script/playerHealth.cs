using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{
    [SerializeField] int startHealth = 3;
    [SerializeField] int currentHealth = 0;
    [SerializeField] RectTransform healthTransform;

    [SerializeField] HealthBar healthBar;
    [SerializeField] Image Health;

    private float rectStartWidth = 0;

    private void Awake()
    {
        currentHealth = startHealth;
    }

    public void HurtPlayer(int damageToGive)
    {
        currentHealth -= damageToGive;

        Debug.Log("Enemy damaged player: " + damageToGive);

        if (healthBar && currentHealth > -1)
        {
            Health.fillAmount -= .33f;  
           //healthBar.SetHealth(currentHealth, startHealth);
        }

        if (currentHealth <= 0)
        {
            GameManager gameManager = GameObject.FindObjectOfType<GameManager>();

            if (gameManager)
            {
                gameManager.EndGame();
            }
        }
    }

    public void PickUp(int HealthBonus)
    {
        if (currentHealth < startHealth)
        {
            currentHealth = currentHealth + HealthBonus;
            //healthbar.fillAmount = currentHealth / startHealth;
        }
    }


    public void HealtPlayer(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > startHealth)
        {
            currentHealth = startHealth;
        }
    }
}
