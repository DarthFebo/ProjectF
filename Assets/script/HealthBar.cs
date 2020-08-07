using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private float barWidth = 0;
    private void Awake()
    {
        barWidth = transform.localScale.x;
        Debug.Log("healthBar width" + barWidth);
    }

    public void SetHealth(int currentHealth, int maxHealth)
    {
        Debug.Log(currentHealth + "/" + maxHealth);

        // --- Change Size Of Bar --- \\
        Debug.Log("new health %" + (barWidth) * (float)((float)currentHealth / (float)maxHealth));
        transform.localScale = new Vector3((barWidth) * ((float)currentHealth / (float)maxHealth), transform.localScale.y, 1);

        Debug.Log("HealthBarSize: " + transform.localScale);

    }

}