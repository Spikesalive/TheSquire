using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public Vector2 relativePoint;

    public GameObject Player;

    public GameObject BabySlime;
    
    public GameObject RangedAttack;
    public Transform ProjectileSpawn;
    public float SlimeSpawnSpeed;
    public float Attackspeed;
    public float AttackDistance;
    public int Health;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        InvokeRepeating("Shoot", 0, Attackspeed);
        InvokeRepeating("SpawnSlime", SlimeSpawnSpeed, SlimeSpawnSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        // checks whether player is left or right of enemy and rotates enemy to face player
        relativePoint = transform.InverseTransformPoint(Player.transform.position);
        if (relativePoint.x < 0f && Mathf.Abs(relativePoint.x) > Mathf.Abs(relativePoint.y))
        {
            //Debug.Log("Right");
            transform.Rotate(0, 0, 0);

        }
        if (relativePoint.x > 0f && Mathf.Abs(relativePoint.x) > Mathf.Abs(relativePoint.y))
        {
            //Debug.Log("Left");
            transform.Rotate(0, 180, 0);

        }

        if (Health <= 0)
        {
            Invoke("Death", 0);
        }
    }
    // shoots boss projectile
    void Shoot()
    {
        if (Vector2.Distance(transform.position, Player.transform.position) < AttackDistance)
        {
            Instantiate(RangedAttack, ProjectileSpawn.position, ProjectileSpawn.rotation);
        }
    }
    //melee detection
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Melee"))
        {
            Health -= 1;
        }
    }
    //spawns a baby spitter slime
    void SpawnSlime()
    {
        if (Vector2.Distance(transform.position, Player.transform.position) < AttackDistance)
        {
            Instantiate(BabySlime, ProjectileSpawn.position, ProjectileSpawn.rotation);
        }
    }

    void Death()
    {
        //open door
        Destroy(gameObject);
    }
}
