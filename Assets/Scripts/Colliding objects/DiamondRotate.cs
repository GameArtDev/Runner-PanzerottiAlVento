using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DiamondRotate : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private float speedRotation = 200f;

    private void Start()
    {
        rb= this.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        transform.Rotate(0,0,speedRotation * Time.deltaTime);
    }


 
   
}
