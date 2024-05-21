using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float sceneLoadDelay = 2f;
    public bool isHardModeUnlocked = false;
    static LevelManager instance;
    public AudioSource audioSource;
    [SerializeField] AudioClip menuMusic;
    [SerializeField] AudioClip regularGameMusic;
    [SerializeField] AudioClip hardModeMusic;

    void Awake()
    {
        ManageSingleton();
    }

    private void Start()
    {
        audioSource = instance.GetComponent<AudioSource>();
    }

    void ManageSingleton()
    {
        if(instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void UnlockHardMode(){
        isHardModeUnlocked = true;
    }

    public bool CheckHardMode(){
        return isHardModeUnlocked;
    }
    
    public void LoadTheGame() {
        SceneManager.LoadScene("Game Scene");
        instance.audioSource.clip = regularGameMusic;
        if(!instance.audioSource.isPlaying){
            instance.audioSource.Play();
        }
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene("Main Menu");
        instance.audioSource.clip = menuMusic;
        if(!instance.audioSource.isPlaying){
            instance.audioSource.Play();
        }
    }

    public void LoadGameOver() {
        StartCoroutine(WaitAndLoad("Game Over", sceneLoadDelay));
    }

    public void LoadCredits() {
        SceneManager.LoadScene("Credits Scene");
        instance.audioSource.clip = menuMusic;
        if(!instance.audioSource.isPlaying){
            instance.audioSource.Play();
        }
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void ReloadCurrentScene() {
        Scene scene = SceneManager.GetActiveScene(); 
        WaitAndLoad(scene.name, 0.5f);
    }

    public void LoadHardMode() {
        SceneManager.LoadScene("Hard Mode");
        instance.audioSource.clip = hardModeMusic;
        if(!instance.audioSource.isPlaying){
            instance.audioSource.Play();
        }
    }

    IEnumerator WaitAndLoad(string sceneName, float delay) {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
