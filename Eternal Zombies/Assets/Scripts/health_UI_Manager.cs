using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class health_UI_Manager : MonoBehaviour
{
    public List<Health>allHealth=new List<Health>();
    public int healthCount = 0;

    public Text levelText;
    private int levelNo=0;

    private void Start()
    {
        foreach (var health in allHealth)
        {
            if (health.isActive)
            {
                healthCount++;
            }
        }

        levelNoShow();

    }

    public void healthIncrease()
    {
        for (int i = 0; i < allHealth.Count; i++)
        {
            if (!allHealth[i].isActive)
            {
                allHealth[i].activeHealth();
                healthCount++;
                break;
            }
        }

        if (healthCount >= allHealth.Count)
        {
            Debug.Log("player has max health");
        }

    }

    public void healthDecrease()
    {
        for (int i = allHealth.Count - 1; i >= 0; i--)
        {
            if (allHealth[i].isActive)
            {
                allHealth[i].disableHealth();
                healthCount--;
                break;
            }
        }

        if (healthCount <= 0)
        {
            Debug.Log("player has died ");
        }
    }

    public void levelNoShow()
    {
        levelNo++;
        // Update the level text on a separate GameObject
        levelText.text = "" + levelNo.ToString();
    }

}
