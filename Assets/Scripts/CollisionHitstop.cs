using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHitstop : MonoBehaviour
{
    [SerializeField] int obstacleDamage;
    [SerializeField] AudioClip explosionSFX;
    [SerializeField] float explosionVolume;

    public IEnumerator PlayerCollision(GameObject obstacle, GameObject player)
    {
        obstacle.GetComponent<PolygonCollider2D>().enabled = false; //do this first so only single collision
        player.GetComponent<PlayerController>().LowerHealth(obstacleDamage); //player health is lowered on hit
        obstacle.GetComponent<Renderer>().material.color = Color.yellow; //highlight object so it pops, indicating hit
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(0.2f);
        Time.timeScale = 1f;
        Camera.main.GetComponent<CameraShake>().PlayCameraShake();
        FindObjectOfType<UIController>().UpdatePlayerHealthUI(); //player health is updated while the explosion plays
        obstacle.transform.localScale += Vector3.one * 3; //make the explosion bigger than the original satellite sprite
        obstacle.GetComponent<ObstacleController>().UnFlipSprite(); //re-flip sprite so explosion plays correctly
        PlayExplosion(obstacle);
        //StartCoroutine(FindObjectOfType<AudioFader>().PlayAndFadeAudio(obstacle.GetComponent<AudioSource>(), explosionSFX, explosionVolume)); //new audio fader
        yield return new WaitForSeconds(0.8f);
        obstacle.GetComponent<ObstacleController>().DestroyTarget();
        Destroy(obstacle);
    }

    public void PlayExplosion(GameObject obstacle)
    {
        obstacle.GetComponent<Renderer>().material.color = Color.white; //return sprite to normal color so explosion looks like it should
        obstacle.GetComponent<Animator>().enabled = true;
        obstacle.GetComponent<AudioSource>().PlayOneShot(explosionSFX, explosionVolume);
    }

    public IEnumerator ObstacleCollision(GameObject obstacle){
        obstacle.GetComponent<PolygonCollider2D>().enabled = false; //do this first so only single collision
        obstacle.transform.localScale += Vector3.one * 3; //make the explosion bigger than the original satellite sprite
        obstacle.GetComponent<ObstacleController>().UnFlipSprite(); //re-flip sprite so explosion plays correctly
        PlayExplosion(obstacle);
        yield return new WaitForSeconds(0.8f);
        obstacle.GetComponent<ObstacleController>().DestroyTarget();
        Destroy(obstacle);
    }

    public int GetObstacleDamage(){
        return obstacleDamage;
    }
}
