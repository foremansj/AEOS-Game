using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject gameOverCanvas;
    
    public IEnumerator GameOverHitstop(GameObject player)
    {
        FindObjectOfType<ObstacleSpawner>().ChangeSpawningStatus(); //turn off spawning
        player.GetComponent<PolygonCollider2D>().enabled = false; //
        player.GetComponent<Renderer>().material.color = Color.red; //highlight object so it pops, indicating hit
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1f;
        Camera.main.GetComponent<CameraShake>().PlayCameraShake();
        player.transform.localScale += Vector3.one * 5; //make the explosion bigger than the original satellite sprite
        player.GetComponent<Animator>().speed = 0.5f;
        FindObjectOfType<CollisionHitstop>().PlayExplosion(player); //play the explosion animation
        yield return new WaitForSeconds(1.6f);
        TurnOffPlayer(player);
        gameOverCanvas.SetActive(true);
    }

    private static void TurnOffPlayer(GameObject player)
    {
        player.GetComponent<PlayerController>().ChangeControl();
        player.GetComponent<SpriteRenderer>().enabled = false; //turn off renderer instead of destroying, so camera stays on
        //player.GetComponent<LineRenderer>().enabled = false;
        //player.transform.GetChild(0).gameObject.SetActive(false); //turn off left thruster
        //player.transform.GetChild(1).gameObject.SetActive(false); //turn off right thruster
        //maybe this turns off both lines?
        foreach(LineRenderer line in FindObjectsOfType<LineRenderer>()){
            line.enabled = false;
        }
        
    }

    //is this even needed if the player is gone? should fix itself
    IEnumerator BlowUpRemainingObstacles(){
        List<ObstacleController> obstaclesList = new List<ObstacleController>(FindObjectsOfType<ObstacleController>());
        foreach(ObstacleController obstacle in obstaclesList){
            //turn off their colliders
        }
        yield return null;
    }

    public void EnableGameOverOverlay(){
        gameOverCanvas.SetActive(true);
    }
}
