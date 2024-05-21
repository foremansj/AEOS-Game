using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class DialogueController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI missionControlText;
    [SerializeField] List<MissionControlTextSO> missionControlTextList;
    [SerializeField] List<MissionControlTextSO> victoryDialogues;
    [SerializeField] List<MissionControlTextSO> failureDialogues;
    [SerializeField] float timeBetweenDialogue;
    [SerializeField] float typingSpeed;
    [SerializeField] AudioClip keyboardSounds;
    [SerializeField] AudioSource keyboardSource;
    int currentDialogueIndex = 0;
    bool isTyping = false;
    bool isGameOver = false;
    int totalCharsInDialogue = 0;

    private void Start()
    {
        isTyping = true;
        missionControlText.SetText("");
        if(missionControlTextList.Count > 0){
            StartCoroutine(textTypewriter(missionControlText, missionControlTextList[currentDialogueIndex].GetMissionControlText()));
        }
        
    }
    private void Update()
    {
        if(!isTyping && !isGameOver){
            NextDialogueText();
        }
    }
    IEnumerator textTypewriter(TextMeshProUGUI textBox, string text){
        keyboardSource.Play();
        if(text != null){
            foreach(char c in text){
                isTyping = true;
                textBox.SetText(textBox.text + c);
                totalCharsInDialogue ++;
                yield return new WaitForSeconds(typingSpeed);
            }
        }
        
        else {
            missionControlText.SetText("");
        }
        
        keyboardSource.Stop();
        yield return new WaitForSeconds(timeBetweenDialogue);
        isTyping = false;
    }

    private void NextDialogueText(){
        if(currentDialogueIndex < missionControlTextList.Count - 1){
            currentDialogueIndex += 1;
            missionControlText.SetText("");
            StartCoroutine(textTypewriter(missionControlText, missionControlTextList[currentDialogueIndex].GetMissionControlText()));
        }

        else if(currentDialogueIndex == missionControlTextList.Count - 1){
            missionControlText.SetText("");
        }

        else {
            missionControlText.SetText("");
        }
    }

    private void PreviousDialogueText(){
        if(currentDialogueIndex >= 0){
            currentDialogueIndex -= 1;
            missionControlText.SetText("");
            StartCoroutine(textTypewriter(missionControlText, missionControlTextList[currentDialogueIndex].GetMissionControlText()));
        }
    }

    private void ControlTypingSound(){
        if(isTyping && !keyboardSource.isPlaying){
            keyboardSource.PlayOneShot(keyboardSounds);
        }
        else{
            keyboardSource.Stop();
        }
    }

    public IEnumerator EndGameDialogueTyping(){
        while(currentDialogueIndex <= missionControlTextList.Count - 1){
            missionControlText.SetText("");
            StartCoroutine(textTypewriter(missionControlText, missionControlTextList[currentDialogueIndex].GetMissionControlText()));
            yield return new WaitForSeconds(timeBetweenDialogue + 5f);
            currentDialogueIndex += 1;
        }
        
        yield return null;
    }

    public void BeginVictoryDialogues(){
        StopAllCoroutines();
        isGameOver = true;
        missionControlText.SetText("");
        missionControlTextList.Clear();
        missionControlTextList.AddRange(victoryDialogues);
        currentDialogueIndex = 0;
        StartCoroutine(EndGameDialogueTyping());
    }

    public void BeginFailureDialogues(){
        StopAllCoroutines();
        isGameOver = true;
        //missionControlText.SetText("");
        missionControlTextList.Clear();
        missionControlTextList.AddRange(failureDialogues);
        currentDialogueIndex = 0;
        StartCoroutine(EndGameDialogueTyping());
    }
}
