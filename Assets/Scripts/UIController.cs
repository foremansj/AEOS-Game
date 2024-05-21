using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI approachAngleNumberText;
    [SerializeField] TextMeshProUGUI distanceToTargetText;
    [SerializeField] TextMeshProUGUI playerHealthText;
    [SerializeField] GameObject hardModeButton;
    bool isPlaying = true;

    void Start()
    {
        StartCoroutine(SetApproachAngleText());
        StartCoroutine(SetTargetDistanceText());
    }

    IEnumerator SetApproachAngleText(){
        while(isPlaying){
            ApproachAngleCalculator approachAngleCalculator = FindObjectOfType<ApproachAngleCalculator>();
            approachAngleNumberText.SetText(String.Format("{0:00}°{1:00}ʹ {2:00}\"", approachAngleCalculator.GetAngleDegrees(), 
                                                            approachAngleCalculator.GetAngleMinutes(), approachAngleCalculator.GetAngleSeconds()));
            yield return new WaitForSeconds(.2f);
        }
        yield return null;
    }

    IEnumerator SetTargetDistanceText(){
        while(isPlaying){
            DistanceToPointOfNoReturn distanceToTarget = FindObjectOfType<DistanceToPointOfNoReturn>();
            float distance = distanceToTarget.GetDistanceToTarget() * 10f;
            distanceToTargetText.SetText("{0:000}km", Mathf.RoundToInt(distance));
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }

    public void UpdatePlayerHealthUI(){
        PlayerController playerController = FindAnyObjectByType<PlayerController>();
        float healthPercent = Mathf.Round((float)playerController.GetCurrentPlayerHealth() / playerController.GetStartingPlayerHealth() * 100);
        playerHealthText.SetText("{0:000} %", healthPercent);
    }

    public void ToggleHardMode(){
        if(hardModeButton != null){
            if(!hardModeButton.activeInHierarchy){
            hardModeButton.SetActive(true);
            }

            else {
                hardModeButton.SetActive(false);
            }
        }
        
    }
}
