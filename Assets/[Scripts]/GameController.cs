using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject onScreenContols;

    private SoundManager soundManager;
    // Start is called before the first frame update
    void Awake()
    {
        onScreenContols = GameObject.Find("OnScreenControls");

        onScreenContols.SetActive(Application.isMobilePlatform);

        soundManager = FindObjectOfType<SoundManager>();

        soundManager.PlayMusic();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            FindObjectOfType<HealthBarController>().takeDamage(20);
        }
    }

}
