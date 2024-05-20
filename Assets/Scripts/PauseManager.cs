using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject pauseCanvas;
    Color originalDimmerColor;
    List<AudioSource> audioSourcesPaused = new List<AudioSource>();
    bool isPaused = false;

    private void Start()
    {
        
    }
    private void Update()
    {
        PauseGame();
    }

    public void PauseGame(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(!isPaused)
            {
                Time.timeScale = 0;
                isPaused = true;
                pauseCanvas.SetActive(true);
                PauseAudio();
            }

            else {
                UnPauseGame();
            }
        }
    }

    private void PauseAudio() {
        foreach (AudioSource audioSource in FindObjectsOfType<AudioSource>()) {
            if (audioSource.isPlaying)
            {
                audioSourcesPaused.Add(audioSource);
                audioSource.Pause();
            }
        }
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        pauseCanvas.SetActive(false);
        UnPauseAudio();
    }

    private void UnPauseAudio() {
        foreach (AudioSource audioSource in audioSourcesPaused) {
            audioSource.Play();
        }
        
        audioSourcesPaused.Clear();
    }
}
