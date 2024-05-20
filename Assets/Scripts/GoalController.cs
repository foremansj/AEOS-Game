using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    [SerializeField] float obstacleSpeed;
    [SerializeField] AudioClip successAudioClip;
    [SerializeField] float waitBeforeMoving;
    PlayerController playerController;
    Rigidbody2D rb;
    float turnSpeed;
    float gameTimer;
    public Vector2 targetLocation;
    public Vector2 targetDirection;
    ObstacleSpawner obstacleSpawner;

    GameObject target;
    [SerializeField] GameObject targetTrackPrefab;
    [SerializeField] GameObject targetPrefab;
    [SerializeField] TextMeshProUGUI gameOverHeadline;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        obstacleSpawner = FindObjectOfType<ObstacleSpawner>();
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CalculateInitialTargets(this.transform, playerController.transform);
        turnSpeed = playerController.GetTurnSpeed();
    }

    private void FixedUpdate()
    {
        MoveToTarget();
        ObstacleRotation();
        gameTimer += Time.deltaTime;
    }

    private void MoveToTarget(){
        if(gameTimer >= waitBeforeMoving){
            transform.Translate(Vector2.down * obstacleSpeed * Time.deltaTime);
        }
    }

    private void CalculateInitialTargets(Transform start, Transform midpoint){
        targetLocation = new Vector2((2*midpoint.position.x - start.position.x), 
                                     (2*midpoint.position.y - start.position.y));
        targetDirection = start.position - midpoint.position;
        transform.up = targetDirection;
    }

    private void ObstacleRotation(){
        if((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && 
            !(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))) {
            transform.Rotate(0, 0, -turnSpeed * Time.deltaTime);
        }

        if((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) &&
            !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))) {
            transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
        }

        if((Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) || 
            (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))) {
            transform.Rotate(0, 0, 0);
        }
    }

    public void PlayerWins(){
        obstacleSpeed = 0;
        obstacleSpawner.ChangeSpawningStatus();
        FindObjectOfType<DialogueController>().BeginVictoryDialogues();
        gameOverHeadline.SetText("CONGRATULATIONS!");
        FindObjectOfType<GameOver>().EnableGameOverOverlay();
        FindObjectOfType<PlayerController>().GetComponent<PolygonCollider2D>().enabled = false;
        FindObjectOfType<PlayerController>().GetComponent<AudioSource>().PlayOneShot(successAudioClip, 0.3f);
    }

    public void PlayerLoses(){
        obstacleSpeed = 0;
        obstacleSpawner.ChangeSpawningStatus();
        FindObjectOfType<DialogueController>().BeginFailureDialogues();
        gameOverHeadline.SetText("GAME OVER");
        FindObjectOfType<GameOver>().EnableGameOverOverlay();
        //turn off the line renderers
        FindObjectOfType<EarthController>().SetMoveSpeed(300);
    }
}