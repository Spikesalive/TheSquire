using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeScript : MonoBehaviour
{
    public float MovementSpeed;
    public Rigidbody2D RB;
    public bool EnemyMelee;
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        RB.velocity = transform.right * MovementSpeed;
    }
    void Update()
    {
        time -= 1 * Time.deltaTime;
        if(time <= 0)
        {
            Invoke("Death",0);
        }
    }

    void Death()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (EnemyMelee == true)
        {
            if (hitInfo.CompareTag("Player") || hitInfo.CompareTag("Shield"))
            {
                Destroy(gameObject);
            }
        }
        if (EnemyMelee == false)
        {
            if (hitInfo.CompareTag("Enemy"))
            {
                Destroy(gameObject);
            }
        }


    }
}
