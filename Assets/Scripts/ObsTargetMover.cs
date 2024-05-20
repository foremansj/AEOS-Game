using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsTargetMover : MonoBehaviour
{
    [SerializeField] float turnSpeed;
    [SerializeField] float moveSpeed;
    GameObject chasingObstacle;
    Vector2 movementVector;
    Rigidbody2D rb;
    float currentRotation;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //BasicRotation();
        //MoveTheTarget();
        float newRotation = currentRotation + -Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;
        RotateTarget(newRotation);
    }

    /*private void BasicRotation(){
        if((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && 
            !(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))) {
            transform.Rotate(0, 0, Mathf.Clamp(turnSpeed * Time.deltaTime, -135, 135));
        }

        if((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) &&
            !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))) {
            transform.Rotate(0, 0, Mathf.Clamp(-turnSpeed * Time.deltaTime, -135, 135));
        }
    }*/

    private void RotateTarget(float rotation){
        currentRotation = Mathf.Clamp(rotation, -140, 140);
        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }

    /*private void MoveTheTarget(){
        rb.velocity = movementVector * moveSpeed * Time.deltaTime;
    }*/

    public void SetMoveSpeed(float speed){
        moveSpeed = speed;
    }

    public void SetChasingObstacle(GameObject obstacle){
        chasingObstacle = obstacle;
    }

    public void SetMovementVector(Vector2 moveVector){
        movementVector = moveVector;
    }
}
