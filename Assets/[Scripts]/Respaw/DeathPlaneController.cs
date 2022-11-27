using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DeathPlaneController : MonoBehaviour
{
    public Transform playerSpawnPoint;
    private SoundManager soundManager;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Player")
        {
            //other.gameObject.GetComponent<PlayerBehaviour>().life.LoseLife();
            //other.gameObject.GetComponent<PlayerBehaviour>().Health.resetHealth();
            var player = other.gameObject.GetComponent<PlayerBehaviour>();
            player.life.LoseLife();
            player.Health.resetHealth();

            if(player.life.value > 0)
            {
                ReSpawn(other.gameObject);

                FindObjectOfType<SoundManager>().PlaySoundFX(SoundFX.DEATH, Channel.PLAYER_DEATH_FX);
                //soundManager.PlaySoundFX(SoundFX.DEATH);
                // TODO: Play Death Sound
            }

        }
    }

    public void ReSpawn(GameObject go)
    {
        go.transform.position = playerSpawnPoint.position;
    }
}
