using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproachAngleCalculator : MonoBehaviour
{
    [SerializeField] LineRenderer originalLineToGoal;
    [SerializeField] LineRenderer currentLineToGoal;
    Vector2 originalLine;
    Vector2 currentLine;
    float angle;
    int degrees;
    int minutes;
    int seconds;
    //bool isTesting = true;

    private void Start()
    {
        //StartCoroutine(AngleTester());
    }
    private void Update()
    {
        CalculateApproachAngle();
    }
    private void CalculateApproachAngle(){
        originalLine = originalLineToGoal.GetPosition(1) - originalLineToGoal.GetPosition(0);
        currentLine = currentLineToGoal.GetPosition(0) - currentLineToGoal.GetPosition(1);
        angle = Vector2.Angle(originalLine, currentLine);
        degrees = Mathf.FloorToInt(angle);
        minutes = Mathf.FloorToInt((angle - degrees) * 60);
        seconds = Mathf.FloorToInt((((angle - degrees) * 60) - minutes) * 60);
    }

    /*IEnumerator AngleTester(){
        while(isTesting){
            Debug.Log("The approach angle is :" + degrees + "degrees, " + minutes + "minutes, and " + seconds + "seconds");
            Debug.Log("The approach angle float is :" + angle);
            yield return new WaitForSeconds(5f);
        }
        yield return null;
    }*/

    public int GetAngleDegrees()
    {
        return degrees;
    }

    public int GetAngleMinutes(){
        return minutes;
    }

    public int GetAngleSeconds(){
        return seconds;
    }

    public float GetAngle(){
        return angle;
    }
}
