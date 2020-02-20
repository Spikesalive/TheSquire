using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    [SerializeField] public LayerMask Platformslayermask;

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

    public Collider2D AICollider;

    public Vector2 vector2;

    public Vector2 relativePoint;

    public Animator animator;

    const int STATE_IDLE = 0;
    const int STATE_WALK = 1;
    const int STATE_ATTACK = 2;

    int _currentAnimationState = STATE_IDLE;


    // Start is called before the first frame update
    void Start()
    {
        animator = this.animator.GetComponent<Animator>();
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
        vector2 = new Vector2(Player.transform.position.x ,transform.position.y);
        if(Vector2.Distance(transform.position, Player.transform.position) > StoppingDistance && Vector2.Distance(transform.position, Player.transform.position) < 10)
        {
       
            transform.position = Vector2.MoveTowards(transform.position, vector2, MovementSpeed * Time.deltaTime);
            changeState(STATE_WALK);
            //transform.position = Vector2.MoveTowards(transform.position, vector2, MovementSpeed * Time.deltaTime);
        }

        if (Vector2.Distance(transform.position, Player.transform.position) < 10)
        {
            changeState(STATE_IDLE);
        }

        if(Health <= 0)
        {
            Destroy(gameObject);
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
            changeState(STATE_ATTACK);
        }
    }
    void Melee()
    {
        if (Vector2.Distance(transform.position, Player.transform.position) < AttackDistance)
        {
            Instantiate(MeleeAttack, ProjectileSpawn.position, ProjectileSpawn.rotation);
            changeState(STATE_ATTACK);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Melee"))
        {
            Health -= 1;
        }
    }

    void changeState(int State)
    {
        if (_currentAnimationState == State)
            return;
        switch (State)
        {
            case STATE_WALK:
                animator.SetInteger("State", STATE_WALK);
                break;

            case STATE_IDLE:
                animator.SetInteger("State", STATE_IDLE);
                break;

            case STATE_ATTACK:
                animator.SetInteger("State", STATE_ATTACK);
                break;
        }
        _currentAnimationState = STATE_IDLE;
    }

}
