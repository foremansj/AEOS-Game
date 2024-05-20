using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRotationFunction : MonoBehaviour
{
    [SerializeField] float turnSpeed;
    [SerializeField] float moveSpeed;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        BasicRotation();
        MoveOffScreen();
    }

    private void BasicRotation(){
        if((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && 
            !(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))) {
            transform.Rotate(0, 0, -turnSpeed * Time.deltaTime);
        }

        if((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) &&
            !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))) {
            transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
        }
    }

    private void MoveOffScreen(){
        rb.velocity = Vector2.left * moveSpeed * Time.deltaTime;
    }

    public void SetMoveSpeed(float speed){
        moveSpeed = speed;
    }
}
