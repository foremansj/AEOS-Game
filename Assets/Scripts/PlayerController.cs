using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D playerRigidbody;
    [SerializeField] float turnSpeed;
    [SerializeField] int startingPlayerHealth;
    int currentPlayerHealth;

    [SerializeField] ParticleSystem leftThrustFX;
    [SerializeField] ParticleSystem rightThrustFX;
    [SerializeField] AudioClip rocketBoostClip;
    [SerializeField] AudioSource leftThrusterSource;
    [SerializeField] AudioSource rightThrusterSource;
    [SerializeField] float thursterVolume;

    [SerializeField] AudioClip explosionClip;
    

    ObstacleSpawner obstacleSpawner;
    GoalController goalController;
    LineRenderer lineRenderer;
    bool isThrusting;
    bool hasControl = true;

    
    private void Awake()
    {
        obstacleSpawner = FindObjectOfType<ObstacleSpawner>();
        goalController = FindObjectOfType<GoalController>();
    }
    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
        currentPlayerHealth = startingPlayerHealth;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        HandleThrust();
        DrawTargetTrajectory();
    }
    private void Update()
    {
        KillThrusters();
    }

    private void HandleThrust(){
        if(hasControl){
            if((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && 
            !(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))) {
            transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
            FireRightThruster();
            }

            if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) &&
                !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)))
            {
                transform.Rotate(0, 0, -turnSpeed * Time.deltaTime);
                FireLeftThruster();
            }

            if ((Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) || 
                (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))) {
                transform.Rotate(0, 0, 0);
                FireLeftThruster();
                FireRightThruster();
            }
        }
    }

    private void FireLeftThruster()
    {
        leftThrustFX.Play();
        isThrusting = true;
        if (!leftThrusterSource.isPlaying)
        {
            leftThrusterSource.volume = thursterVolume;
            leftThrusterSource.Play();
        }
    }

    private void FireRightThruster()
    {
        isThrusting = true;
        rightThrustFX.Play();
        if (!rightThrusterSource.isPlaying)
        {
            rightThrusterSource.volume = thursterVolume;
            rightThrusterSource.Play();
        }
    }

    private void KillThrusters(){
        if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow)){
            rightThrustFX.Stop();
            isThrusting = false;
            if(!isThrusting){
                StartCoroutine(FindObjectOfType<AudioFader>().AudioFadeOutStop(rightThrusterSource, 0.2f));
            }
        }

        if(Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow)){
            leftThrustFX.Stop();
            isThrusting = false;
            if(!isThrusting){
                StartCoroutine(FindObjectOfType<AudioFader>().AudioFadeOutStop(leftThrusterSource, 0.2f));
            }
        }
    }

    public void LowerHealth(int damage) {
        currentPlayerHealth -= damage;
        if(currentPlayerHealth <= 0){
            GetComponent<PolygonCollider2D>().enabled = false;
            FindObjectOfType<CollisionHitstop>().PlayExplosion(this.gameObject);
            obstacleSpawner.ChangeSpawningStatus();
            //Destroy(this.gameObject);
            //game over screen
        }
    }

    private void DrawTargetTrajectory(){
        if(hasControl){
            lineRenderer.SetPosition(1, new Vector2(goalController.transform.position.x + 10, goalController.transform.position.y + 10));
        }
        //lineRenderer.SetPosition(1, new Vector2(goalController.transform.position.x + 10, goalController.transform.position.y + 10));
    }

    public float GetTurnSpeed(){
        return turnSpeed;
    }

    public int GetCurrentPlayerHealth(){
        return currentPlayerHealth;
    }

    public int GetStartingPlayerHealth(){
        return startingPlayerHealth;
    }

    public void ChangeControl(){
        hasControl = !hasControl;
    }
}
