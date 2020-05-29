using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float turnSpeed;
    public float jumpHeight;
    public float gravityStrenght;
    public float coins;
    public bool gameIsActive;  
    public bool isGrounded;
    
    private float horizontalInput;
    private float forwardInput;

    public Rigidbody playerRB;
    private GameObject block;
    public GameObject playAgain;
    public GameObject startGame;
    public GameObject restart;
    private Timer timer;  
    private Health health;
    private AudioSource playerAudio;
    public AudioClip jumpSound;
    public AudioClip coinSound;
    public AudioClip hitSound;   
    public TextMeshProUGUI goal;
    public TextMeshProUGUI time;
    public TextMeshProUGUI numberOfCoins;

    // Start is called before the first frame update
    void Start()
    {
        timer = gameObject.GetComponent<Timer>();
        health = gameObject.GetComponent<Health>();
        playerRB = GetComponent<Rigidbody>();
        block = GameObject.Find("Block");
        playerAudio = GetComponent<AudioSource>();

        coins = 0;

        startGame.gameObject.SetActive(true);

        isGrounded = true;
        gameIsActive = false;
    }

    void FixedUpdate()
    {
        //Add physics
        Vector3 gravityS = new Vector3(0, gravityStrenght, 0);
        Physics.gravity = gravityS;

        // Get player input
        horizontalInput = Input.GetAxisRaw("Horizontal");
        forwardInput = Input.GetAxisRaw("Vertical");

        //Check if game is active
        if (gameIsActive == true)
        {
            // Moves the player base on vertical input
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed * forwardInput);
            // Turns the player based on horizontal input
            transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);

            //Jump when space is pressed and isGrouded is true, play jump sound
            if (Input.GetKey(KeyCode.Space) && isGrounded == true)
            {
                Jump();
                playerAudio.PlayOneShot(jumpSound, 1.0f);
            }

        }

        //Check bool alive in health script and show game over if alive is false
        if(health.alive == false)
        {
            gameIsActive = false;
            restart.gameObject.SetActive(true);         
        }
    }

    void Jump()
    {
        playerRB.AddForce(new Vector3(0, jumpHeight, 0));

        isGrounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Stump"))
        {
            isGrounded = true;
        }

        else if (collision.gameObject.CompareTag("Trap") || collision.gameObject.CompareTag("Wall"))
        {
            isGrounded = false;
            StartCoroutine(JumpTimer());
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        //Collect coin
        if (other.CompareTag("Coin"))
        {
            coins = coins + 1;
            Destroy(other.gameObject);
            playerAudio.PlayOneShot(coinSound, 1.0f);

            if (coins == 30)
            {
                block.SetActive(false);
            }

            numberOfCoins.text = coins.ToString();
        }

        //Stop timer and show play again button
        if (other.CompareTag("Goal"))
        {
            timer.startTimer = false;

            playAgain.gameObject.SetActive(true);

            time.text = "Time: " + timer.timePlayingStr;

            gameIsActive = false;
        }

        //Lose one heart
        if (other.CompareTag("Trap"))
        {
            health.DamageTaken();
            playerAudio.PlayOneShot(hitSound, 1.0f);
        }
    }

    //Start timer
    public void StartGame()
    {
        timer.startTimer = true;
        gameIsActive = true;
        startGame.gameObject.SetActive(false);
    }

    //Load scene
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator JumpTimer()
    {
        yield return new WaitForSeconds(1);
        isGrounded = true;
    }

}
