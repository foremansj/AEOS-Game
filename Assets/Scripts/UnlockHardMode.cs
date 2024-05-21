using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockHardMode : MonoBehaviour
{
    [SerializeField] GameObject hardModeButton;
    LevelManager levelManager;

    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }
    
    void Update()
    {
        if(levelManager != null){
            if(levelManager.CheckHardMode()){
                hardModeButton.SetActive(true);
            }
        }
    }
}
