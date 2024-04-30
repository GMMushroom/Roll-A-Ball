using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

public class PlayerController : MonoBehaviour
{
    public float speed = 1f;
    private Rigidbody rb;
    public static float currentPickUpCount = 0f;
    public static float maxPickUpCount = 0f;
    private GameObject resetPoint;
    private bool resetting = false;
    private bool grounded = true;
    private Color originalColor;
    private Timer timer;
    private bool gameOver = false;

    //Controllers
    CameraController cameraController;
    SoundController soundController;

    [Header("UI")]
    public TMP_Text pickUpText;
    public TMP_Text timerText;
    public TMP_Text winTimeText;
    public GameObject winPanel;
    public GameObject displayTimer;
    public GameObject inGamePanel;
    public GameObject pausePanel;


    // Start is called before the first frame update
    void Start()
    {
        //Set timeScale for Pause script
        Time.timeScale = 1;

        //Reset Pickup count
        currentPickUpCount = 0f;
        maxPickUpCount = 1f;

        //Turn on our In-game Panel & turn off Win Panel
        inGamePanel.SetActive(true);
        displayTimer.SetActive(true);
        pausePanel.SetActive(false);
        winPanel.SetActive(false);

        //Get RigidBody
        rb = GetComponent<Rigidbody>();

        //Get Controllers
        cameraController = FindObjectOfType<CameraController>();

        //Get the number of pickups in our scene
        maxPickUpCount = GameObject.FindGameObjectsWithTag("Pick Up").Length;

        //Set Pick Up Count
        CheckPickUps();

        //Get the timer object and start the timer
        timer = FindObjectOfType<Timer>();
        timer.StartTimer();

        //Find soundController
        soundController = FindObjectOfType<SoundController>();

        //Get Reset Point
        resetPoint = GameObject.Find("Reset Point");
        originalColor = GetComponent<Renderer>().material.color;
    }

    //Display time during gameplay
    private void Update()
    {
        timerText.text = "Time: " + timer.GetTime().ToString("F2");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameOver == true)
            return;

        if (resetting)
            return;

        if (grounded)
        { //Movement
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

            //Camera Controls
            if (cameraController.cameraStyle == CameraStyle.Free)
            {
                //Rotates the player to the direction of the camera
                transform.eulerAngles = Camera.main.transform.eulerAngles;
                //Translates the input vectors into coordinates
                movement = transform.TransformDirection(movement);
            }

            rb.AddForce(movement * speed);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
            grounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
            grounded = false;
    }

    //Interacting with Pickups
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pick Up")
        {
            //Destroy other objects on collision
            Destroy(other.gameObject);
            //Count the Pickup count
            currentPickUpCount++;
            //Run the CheckPickUps() function
            CheckPickUps();
            //Play pickupSound
            soundController.PlayPickupSound();
        }
    }

    void CheckPickUps()
    {
        pickUpText.text = "Fragments Collected: " + currentPickUpCount + "/" + maxPickUpCount;
        if (currentPickUpCount == maxPickUpCount)
        {
            winGame();
        }
    }

    //Respawn player or play collision sound
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Respawn"))
        {
            StartCoroutine(ResetPlayer());
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            soundController.PlayCollisionSound(collision.gameObject);
        }
    }

    void winGame()
    {
        //Set gameover to be true
        gameOver = true;

        //Turn off our In-game Panel & turn Win Panel on
        /*inGamePanel.SetActive(false);*/
        displayTimer.SetActive(false);
        winPanel.SetActive(true);

        //Change the font size and color of text when all Pick-Ups = 0
        pickUpText.color = Color.green;
        timerText.color = Color.yellow;
        /*pickUpText.fontSize = 120;
        timerText.fontSize = 120;*/

        //Stop the timer
        timer.StopTimer();

        //Display our time to the win time text
        winTimeText.text = "Your time was: " + timer.GetTime().ToString("F2");

        //Play winSound
        soundController.PlayWinSound();

        //Stop the player from moving
        /*rb.velocity = Vector3.zero;
        rb.angularDrag = 0;*/
        rb.isKinematic = true;
    }

    public IEnumerator ResetPlayer()
    {
        resetting = true;
        GetComponent<Renderer>().material.color = Color.black;
        rb.velocity = Vector3.zero;
        Vector3 startPos = transform.position;
        float resetSpeed = 2f;
        var i = 0.0f;
        var rate = 1.0f / resetSpeed;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.position = Vector3.Lerp(startPos, resetPoint.transform.position, i);
            yield return null;
        }

        GetComponent<Renderer>().material.color = originalColor;
        resetting = false;
    }
}