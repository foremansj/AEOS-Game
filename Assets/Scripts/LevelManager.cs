using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float sceneLoadDelay = 2f;
    
    public void LoadTheGame() {
        SceneManager.LoadScene("Game Scene");
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadGameOver() {
        StartCoroutine(WaitAndLoad("Game Over", sceneLoadDelay));
    }

    public void LoadCredits() {
        SceneManager.LoadScene("Credits Scene");
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void ReloadCurrentScene() {
        Scene scene = SceneManager.GetActiveScene(); 
        WaitAndLoad(scene.name, 0.5f);
    }

    IEnumerator WaitAndLoad(string sceneName, float delay) {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
