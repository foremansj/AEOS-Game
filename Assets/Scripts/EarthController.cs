using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthController : MonoBehaviour
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
        EarthRotation();
        MoveOffScreen();
    }

    private void EarthRotation(){
        if((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && 
            !(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))) {
            transform.Rotate(0, 0, -turnSpeed * Time.deltaTime);
            //fire left thrust effect
            //gas starts draining
            //obstacle adjusts course
        }

        if((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) &&
            !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))) {
            transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
            //fire right thrust effect
            //gas starts draining
            //obstacle adjusts course
        }
    }

    private void MoveOffScreen(){
        rb.velocity = Vector2.left * moveSpeed * Time.deltaTime;
    }

    public void SetMoveSpeed(float speed){
        moveSpeed = speed;
    }
}
