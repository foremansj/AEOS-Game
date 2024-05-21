using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    [SerializeField] float shotSpeed;
    Vector3 shotDirection;
    Rigidbody2D rb;
    float lifeTimer;
    void Start()
    {
        shotDirection = FindObjectOfType<PlayerController>().gameObject.transform.rotation.eulerAngles;
        //transform.eulerAngles = new Vector3(shotDirection.x, shotDirection.y, shotDirection.z - 90);
        //this.transform.rotation = shotDirection;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //rb.velocity = Vector2.forward * shotSpeed * Time.deltaTime;
        transform.Translate(Vector2.up * shotSpeed * Time.deltaTime);
        lifeTimer += Time.deltaTime;
        if(lifeTimer > 4f){
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Obstacle") {
            //FindObjectOfType<CollisionHitstop>().ObstacleCollision(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
