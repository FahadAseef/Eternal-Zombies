using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public bool isActive;
    public Image foreGround;

    public void disableHealth()
    {
        isActive = false;
        foreGround.gameObject.SetActive(false);
    }

    public void activeHealth()
    {
        isActive=true;
        foreGround.gameObject.SetActive(true);
    }

}
