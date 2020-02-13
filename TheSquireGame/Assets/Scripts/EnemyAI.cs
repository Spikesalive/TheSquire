using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject Player;
    public float MovementSpeed;
    public float StoppingDistance;
    public float AttackDistance;
    public int Health;

    public bool MeleeClass;
    public Transform ProjectileSpawn;
    public GameObject RangedAttack;
    public GameObject MeleeAttack;
    public float Attackspeed;

    public Vector2 relativePoint;

    
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        if (MeleeClass == false)
        {
            InvokeRepeating("Shoot", 0, Attackspeed);
        }
        if (MeleeClass == true)
        {
            InvokeRepeating("Melee", 0, Attackspeed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, Player.transform.position) > StoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, MovementSpeed * Time.deltaTime);
        }
        if(Vector2.Distance(transform.position, Player.transform.position) < AttackDistance)
        {
            //run attack animation
            if (MeleeClass == true)
            {
                //melee
            }
        }
        

        //checking whether player is left or right of Ai
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
    }
    void Shoot()
    {
        if (Vector2.Distance(transform.position, Player.transform.position) < AttackDistance)
        {
            Instantiate(RangedAttack, ProjectileSpawn.position, ProjectileSpawn.rotation);
        }
    }
    void Melee()
    {
        if (Vector2.Distance(transform.position, Player.transform.position) < AttackDistance)
        {
            Instantiate(MeleeAttack, ProjectileSpawn.position, ProjectileSpawn.rotation);
        }
    }
}
