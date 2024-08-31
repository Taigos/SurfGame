using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;


    private int gems = 0;
    private float moveStep = 2.5f;
    private float jumpSpeed;
    [SerializeField]
    GameController MyGameController;
    [SerializeField] float normalJumpSpeed = 350;
    [SerializeField] float superJumpSpeed = 600;

    SpawnManager gameManager;
    [SerializeField]
    MenuManager myMenuManager;

    /// <summary>
    /// 1 - left, 2 - middle, 3 - right
    /// </summary>
    private int currentLane;

    private bool isOnGround = true;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        currentLane = 2;
        jumpSpeed = normalJumpSpeed;

        gameManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    void Update()
    {
        ChangeLanes();

        // jump
        if (Input.GetKeyDown(KeyCode.UpArrow) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            isOnGround = false;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && !isOnGround)
        {
            playerRb.AddForce(Vector3.down * jumpSpeed, ForceMode.Impulse);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            myMenuManager.PauseOn();
        }

        // float horizontalInput = Input.GetAxis("Horizontal");
        // transform.Translate(Vector3.right * horizontalInput * moveStep);
    }

    private void ChangeLanes()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentLane > 1)
        {
            StartCoroutine(ChangeLane(-1));
            currentLane--;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && currentLane < 3)
        {
            StartCoroutine(ChangeLane(1));
            currentLane++;
        }
    }

    IEnumerator ChangeLane(int sign)
    {
        for (float i = 0; i < moveStep; i += 0.1f)
        {
            transform.position += new Vector3(0.1f * sign, 0, 0);
            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            isOnGround = true;
        }
        
    }

    IEnumerator SuperJump()
    {
        jumpSpeed = superJumpSpeed;
        Debug.Log("super sneakers");
        StartCoroutine(gameManager.uiHandler.ShowPowerupBar(gameManager.powerups[0], 5));
        yield return new WaitForSeconds(5);
        Debug.Log("normal jump");
        jumpSpeed = normalJumpSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            if (other.gameObject.name == "Multiplier Powerup(Clone)")
            {
                gameManager.ScoreMultiplier();
            }
            else if (other.gameObject.name == "Super Jump Powerup(Clone)")
            {
                StopAllCoroutines();
                StartCoroutine(SuperJump());
            }
            Destroy(other.gameObject);
        } 
        else if (other.CompareTag("Obstacle"))
        {
            MyGameController.GameOver(gameManager, this);
            //Destroy(gameManager);
            //Destroy(gameObject);
        } 
        else if (other.CompareTag("Gem"))
        {
            Destroy(other.gameObject);
            gems++;
            gameManager.uiHandler.UpdateGemsText(gems);
        }
    }
}
