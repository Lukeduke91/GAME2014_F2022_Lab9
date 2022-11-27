using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class LifeCounterController : MonoBehaviour
{
    [Header("Life Properties")]
    public int value = 3;

    private Image LifeImage;

    // Start is called before the first frame update
    void Start()
    {
        LifeImage = GetComponent<Image>();
        ResetLives();
    }

    public void ResetLives()
    {
        value = 3;
        LifeImage.sprite = Resources.Load<Sprite>("Sprites/Life3");
    }

    public void LoseLife()
    {
        value -= 1;
        if(value < 0)
        {
            value = 0;
        }
        LifeImage.sprite = Resources.Load<Sprite>($"Sprites/Life2");
    }

    public void GainLife()
    {
        value += 1;
        if (value > 3)
        {
            value = 3;
        }
        LifeImage.sprite = Resources.Load<Sprite>($"Sprites/Life3");
    }
}
