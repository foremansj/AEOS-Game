using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFader : MonoBehaviour
{
    
    public IEnumerator AudioFadeOutStop (AudioSource audioSource, float FadeTime) {
        float startVolume = audioSource.volume;
 
        while (audioSource.volume > 0) {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
 
            yield return null;
        }
 
        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    public IEnumerator AudioFadeOutPause (AudioSource audioSource, float FadeTime) {
        float startVolume = audioSource.volume;
 
        while (audioSource.volume > 0) {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
 
            yield return null;
        }
 
        audioSource.Pause();
        audioSource.volume = startVolume;
    }

    public void StopAllAudioFadeCoroutines(){
        StopAllCoroutines();
    }
    
}
