using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointOfNoReturn : MonoBehaviour
{
    [SerializeField] float obstacleSpeed;
    [SerializeField] float waitBeforeMoving;
    ApproachAngleCalculator approachAngleCalculator;
    float gameTimer;
    
    private void Awake()
    {
        approachAngleCalculator = FindObjectOfType<ApproachAngleCalculator>();
    }

    private void FixedUpdate()
    {
        MoveToTarget();
        gameTimer += Time.deltaTime;
    }

    private void Update()
    {
        gameTimer += Time.deltaTime;
    }

    private void MoveToTarget(){
        if(gameTimer >= waitBeforeMoving){
            transform.position = Vector2.MoveTowards(transform.position, Vector2.zero, obstacleSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player"){
            if(approachAngleCalculator.GetAngle() <= 5f){
                FindObjectOfType<GoalController>().PlayerWins();
            }
            
            else {
                FindObjectOfType<GoalController>().PlayerLoses();
            }
        }
    }
}
