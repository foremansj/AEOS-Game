using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Mission Control Text", fileName = "New Text Box")]
public class MissionControlTextSO : ScriptableObject
{
    [TextArea(2,6)]
    [SerializeField] string missionControlText = "Enter new mission control text";

    public string GetMissionControlText()
    {
        return missionControlText;
    }
}
