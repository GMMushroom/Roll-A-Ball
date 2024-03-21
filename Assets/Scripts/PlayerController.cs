using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

public class PlayerController : MonoBehaviour
{
    public float speed = 1f;
    private Rigidbody rb;
    private int pickUpCount = 0;
    private Timer timer;
    private bool gameOver = false;

    [Header("UI")]
    public TMP_Text pickUpText;
    public TMP_Text timerText;
    public TMP_Text winTimeText;
    public GameObject winPanel;
    public GameObject inGamePanel;


    // Start is called before the first frame update
    void Start()
    {
        //Turn on our In-game Panel & turn off Win Panel
        inGamePanel.SetActive(true);
        winPanel.SetActive(false);

        rb = GetComponent<Rigidbody>();
        //Get the number of pickups in our scene
        pickUpCount = GameObject.FindGameObjectsWithTag("Pick Up").Length;
        //Set Pick Up Count
        CheckPickUps();
        //Get the timer object and start the timer
        timer = FindObjectOfType<Timer>();
        timer.StartTimer();
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

        //Movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        rb.AddForce(movement * speed);
    }

    //Interacting with Pickups
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pick Up")
        {
            //Destroy other objects on collision
            Destroy(other.gameObject);
            //Decrement the Pickup count
            pickUpCount--;
            //Run the CheckPickUps() function
            CheckPickUps();
        }
    }

    void CheckPickUps()
    {
        pickUpText.text = "Pick Ups Left: " + pickUpCount;
        if (pickUpCount == 0)
        {
            winGame();
        }
    }

    void winGame()
    {
        //Set gameover to be true
        gameOver = true;

        //Turn off our In-game Panel & turn Win Panel on
        inGamePanel.SetActive(false);
        winPanel.SetActive(true);

        //Change the font size and color of text when all Pick-Ups = 0
        pickUpText.color = Color.green;
        timerText.color = Color.yellow;
        pickUpText.fontSize = 120;
        timerText.fontSize = 120;

        //Stop the timer
        timer.StopTimer();

        //Display our time to the win time text
        winTimeText.text = "Your time was: " + timer.GetTime().ToString("F2");

        //Stop the player from moving
        /*rb.velocity = Vector3.zero;
        rb.angularDrag = 0;*/
        rb.isKinematic = true;
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
