using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class HealthBarController : MonoBehaviour
{
    [Header("Health Properties")]
    public int value;

    [Header("Display Properties")]
    public Slider HealthBar;

    // Start is called before the first frame update
    void Start()
    {
        HealthBar = GetComponentInChildren<Slider>();
        resetHealth();
    }

    public void resetHealth()
    {
        HealthBar.value = 100;
        value = (int)HealthBar.value;
    }

    public void takeDamage(int damage)
    {
        HealthBar.value -= damage;
        if(HealthBar.value < 0)
        {
            HealthBar.value = 0;
        }
        value = (int)HealthBar.value;
    }

    public void healDamage(int damage)
    {
        HealthBar.value += damage;
        if (HealthBar.value > 100)
        {
            HealthBar.value = 100;
        }
        value = (int)HealthBar.value;
    }
}
