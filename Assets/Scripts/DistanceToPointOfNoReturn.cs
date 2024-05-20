using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceToPointOfNoReturn : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject target;
    float distanceFromPlayerToTarget;
    //bool isTesting = true;

    private void Start()
    {
        //StartCoroutine(DistanceTester());
    }

    private void Update()
    {
        CalculateDistanceToTarget();
    }
    private void CalculateDistanceToTarget(){
        distanceFromPlayerToTarget = Vector2.Distance(player.transform.position, target.transform.position);
    }

    public float GetDistanceToTarget(){
        return distanceFromPlayerToTarget;
    }

    /*IEnumerator DistanceTester(){
        while(isTesting){
            CalculateDistanceToTarget();
            Debug.Log("Distance to target is" + distanceFromPlayerToTarget);
            yield return new WaitForSeconds(5f);
        }
        yield return null;
    }*/
}
