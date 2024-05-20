using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] float minSpeed;
    [SerializeField] float maxSpeed; 
    //[SerializeField] GameObject targetCircle;
    [SerializeField] GameObject targetTrackPrefab;
    [SerializeField] GameObject targetPrefab;
    GameObject target;
    float obstacleSpeed;
    Rigidbody2D rb;
    float turnSpeed;        
    Vector3 origScale;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        obstacleSpeed = Random.Range(minSpeed, maxSpeed);
        SetInitialTarget();
        origScale = transform.localScale;
        FlipSprite();
    }

    private void FixedUpdate()
    {
        MoveToTarget();
        ObstacleRotation();
    }

    private void MoveToTarget(){
        //if it gets too far from player (off screen), destroy it and target
        if(Vector2.Distance(this.transform.position, Vector3.zero) > 17){
            DestroyTarget();
            Destroy(this.gameObject);
        }
        //if it reaches its target, destroy both
        else if(Vector2.Distance(this.transform.position, target.transform.position) < 1f){
            DestroyTarget();
            Destroy(this.gameObject);
        }
        //otherwise move to the target
        else {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, obstacleSpeed * Time.deltaTime);
        }
    }

    private void SetInitialTarget(){
        //calculate the vector between the new obstacle and the player, normalize to 1;
        Vector3 direction = FindObjectOfType<PlayerController>().transform.position - this.transform.position;
        direction.Normalize();
        //set up the target track, and its movement vector, and chasing obstacle (this obstacle)
        GameObject targetTrack = Instantiate(targetTrackPrefab, GameObject.Find("Goal Objects").transform);
        targetTrack.transform.position = Vector3.zero + direction * 15;
        targetTrack.GetComponent<ObsTargetMover>().SetMovementVector(direction);
        targetTrack.GetComponent<ObsTargetMover>().SetChasingObstacle(this.gameObject);
        
        //calculate where to instantiate the actual target the obstacle will move to
        Vector2 targetLoc = Vector3.zero + direction * 30f;
        //instantiate the target, reposition the target on the track, and resize it
        target = Instantiate(targetPrefab, targetTrack.transform);
        target.transform.position = targetLoc;
        target.transform.localScale = Vector2.one * (1f / 20f);
    }

    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Player"){
            if(FindObjectOfType<PlayerController>().GetCurrentPlayerHealth() > 1){
                StartCoroutine(FindObjectOfType<CollisionHitstop>().PlayerCollision(this.gameObject, other.gameObject));
            }
            
            else{
                StartCoroutine(FindObjectOfType<GameOver>().GameOverHitstop(FindObjectOfType<PlayerController>().gameObject)); 
                this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                this.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            }
        }

        else {
            StartCoroutine(FindObjectOfType<CollisionHitstop>().ObstacleCollision(this.gameObject));
        }
    }

    public void FlipSprite(){
        int xFlip = Random.Range(0, 2);
        int yFlip = Random.Range(0, 2);
        if(xFlip == 1){
            transform.localScale = new Vector3(-origScale.x, origScale.y, origScale.z);
        }
        
        if(yFlip == 1){
            transform.localScale = new Vector3(origScale.x, -origScale.y, origScale.z);
        }
    }

    public void UnFlipSprite(){
        transform.localScale = origScale;
    }

    public void DestroyTarget(){
        Destroy(target.transform.parent.gameObject);
    }

    private void ObstacleRotation() {
        if((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && 
            !(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))) {
            transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
        }

        if((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) &&
            !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))) {
            transform.Rotate(0, 0, -turnSpeed * Time.deltaTime);
        }

        if((Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) || 
            (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))) {
            transform.Rotate(0, 0, 0);
        }
    }
}
