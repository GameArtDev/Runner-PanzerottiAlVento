using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DiamondMove : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private float speedMove = 1f;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.transform.Translate(Vector2.left * speedMove * Time.deltaTime);
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag =="Player")
        {
            Destroy(this.gameObject);
            //ScoreUI.OnAddDiamound();
        }
    }

}
