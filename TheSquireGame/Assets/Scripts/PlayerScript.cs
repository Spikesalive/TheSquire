using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public LayerMask Platformslayermask;

    public float MovementSpeed;
    public float JumpVelocity;

    public GameObject Player;
    public int PlayerHealth;

    public GameObject Playermodel;

    bool Blocking;
    public GameObject Sword;
    public GameObject Shield;
    public Transform SpawnSpot;
    private GameObject shieldtest;

    public Joystick joystick;
    public Rigidbody2D PlayerRigidbody2D;
    public Collider2D PCollider;

    public Animator animator;

    //Flags to check when certain animations are playing
    /*bool _isPlaying_walk = false;
    bool _isPlaying_block = false;
    bool _isPlaying_attack = false;*/

    //The values in the animator conditions
    const int STATE_IDLE = 0;
    const int STATE_WALK = 1;
    const int STATE_ATTACK = 2;
    const int STATE_BLOCK = 3;

    int _currentAnimationState = STATE_IDLE;

    void Start()
    {
        PlayerRigidbody2D = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Player.transform.Translate(joystick.Horizontal* MovementSpeed *Time.deltaTime, 0, 0);

        //jump
        if (IsGrounded() && joystick.Vertical > .2)
        {
            PlayerRigidbody2D.velocity = Vector2.up * JumpVelocity;
        }

        // joystick movement
        if(joystick.Horizontal > 0)
        {
            PlayerRigidbody2D.velocity = new Vector2(+MovementSpeed * joystick.Horizontal, PlayerRigidbody2D.velocity.y);
            Playermodel.transform.rotation = new Quaternion(0, 0, 0, 0);
            changeState(STATE_WALK);
        }
        if (joystick.Horizontal < 0)
        {
            PlayerRigidbody2D.velocity = new Vector2(MovementSpeed * joystick.Horizontal , PlayerRigidbody2D.velocity.y);
            Playermodel.transform.rotation = new Quaternion(0, 180, 0, 0);
            changeState(STATE_WALK);
        }

        if (joystick.Horizontal == 0)
        {
            changeState(STATE_IDLE);
        }

        //player health / damage
        if (PlayerHealth <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyMelee"))
        {
            //if player is facing left
            if (Playermodel.transform.eulerAngles.y == 180)
            {
                if(Playermodel.transform.position.x > collision.transform.position.x && Blocking == false)
                {
                    PlayerHealth -= 1;
                }
                if (Playermodel.transform.position.x < collision.transform.position.x)
                {
                    PlayerHealth -= 1;
                }
            }
            // if player is facing right
            else
            {
                if (Playermodel.transform.position.x < collision.transform.position.x && Blocking == false)
                {
                    PlayerHealth -= 1;
                }
                if (Playermodel.transform.position.x > collision.transform.position.x)
                {
                    PlayerHealth -= 1;
                }
            }
        }
    }

    //Changes the players animation state
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

            case STATE_BLOCK:
                animator.SetInteger("State", STATE_BLOCK);
                break;

            case STATE_ATTACK:
                animator.SetInteger("State", STATE_ATTACK);
                break;
        }
        _currentAnimationState = STATE_IDLE;
    }
            
    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(PCollider.bounds.center, PCollider.bounds.size, 0f, Vector2.down , .1f, Platformslayermask);
        //Debug.Log(raycastHit2D.collider);
        return raycastHit2D.collider != null;
    }

    //player attack / block
    public void Attack()
    {
        if (Blocking == false)
        {
            Instantiate(Sword, SpawnSpot.position, SpawnSpot.rotation);
            changeState(STATE_ATTACK);
        }
    }

    public void Block()
    {
        Blocking = true;
        shieldtest = Instantiate(Shield, SpawnSpot.position, SpawnSpot.rotation);
        shieldtest.transform.parent = gameObject.transform;
        changeState(STATE_BLOCK);
    }

    public void NotBlock()
    {
        Blocking = false;
        Destroy(shieldtest);
        changeState(STATE_IDLE);
    }
}
