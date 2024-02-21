using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        //Set the offset of the camera based on the player's position
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Make the Transform position of the camera follow the Transform position of the player
        transform.position = player.transform.position + offset;
    }
}
