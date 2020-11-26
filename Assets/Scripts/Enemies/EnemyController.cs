using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    protected int attackDamage = 1;

    [SerializeField]
    protected int health = 1;

    private bool collided = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !collided)
        {
            collided = true;
            other.gameObject.GetComponent<PlayerController>().TakeDamage(attackDamage);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collided = false;
    }
}
