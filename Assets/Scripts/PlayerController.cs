using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1f;
    private Rigidbody rb;
    private int pickUpCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //Get the number of pickups in our scene
        pickUpCount = GameObject.FindGameObjectsWithTag("Pick Up").Length;
        //Set Pick Up Count
        CheckPickUps();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        rb.AddForce(movement * speed);
    }

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
        print("Pick Ups Left: " + pickUpCount);
        if (pickUpCount == 0)
        {
            print("Congratulations! You Win!");
        }
    }
}
