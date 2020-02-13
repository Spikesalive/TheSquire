using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float MovementSpeed;
    public Rigidbody2D RB;
    
    // Start is called before the first frame update
    void Start()
    {
        RB.velocity = transform.right * MovementSpeed;
        Invoke("Death", 5);
    }

    // Update is called once per frame
 
    void Death()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Player") || hitInfo.CompareTag("Shield"))
        {
            Destroy(gameObject);
        }
    }
}
