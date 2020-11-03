using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private bool smoothOn = false;
    [SerializeField]
    private float smoothSpeed = 0.7f;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Compute offset on x axis with the player
        Vector3 moveCamera = new Vector3(player.transform.position.x + offset.x, transform.position.y, transform.position.z);

        if (!smoothOn)
        {
            transform.position = moveCamera;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, moveCamera, smoothSpeed * Time.deltaTime);
        }
    }
}
