using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour
{
    public float speed = 1;
    public Vector3 rotationValue = new Vector3(15, 30, 45);

    // Update is called once per frame
    void Update()
    {
        //Rotate the object according to the provided Vector 3 over time
        transform.Rotate(rotationValue * Time.deltaTime * speed);
    }
}
