using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceToPointOfNoReturn : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject target;
    float distanceFromPlayerToTarget;
    bool hasReachedTarget = false;

    private void Start()
    {
        //StartCoroutine(DistanceTester());
    }

    private void Update()
    {
        CalculateDistanceToTarget();
    }
    private void CalculateDistanceToTarget(){
        if(!hasReachedTarget){
            distanceFromPlayerToTarget = Vector2.Distance(player.transform.position, target.transform.position);
        }
        
        else {
            distanceFromPlayerToTarget = 0f;
        }
    }

    public float GetDistanceToTarget(){
        return distanceFromPlayerToTarget;
    }

    public void SetDistanceToZero(){
        hasReachedTarget = true;
    }
}
