using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AnimationSript : MonoBehaviour
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

    Animator animator;
    //Flags to check when certain animations are playing
    /*
    bool _isPlaying_walk = false;
    bool _isPlaying_block = false;
    bool _isPlaying_attack = false;
    */

    //The values in the animator conditions
    const int STATE_IDLE = 0;
    const int STATE_WALK = 1;
    const int STATE_ATTACK = 2;
    const int STATE_BLOCK = 3;
    
    //Sets the beginning state to idle
    int _currentAnimationState = STATE_IDLE;

    private void Start()
    {
        //Defines the animator attached to the player
        animator = this.animator.GetComponent<Animator>();

        //Allows the player to move
        PlayerRigidbody2D = transform.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //Jump Mechanic for the Player
        if (IsGrounded() && joystick.Vertical > .2)
        {
            PlayerRigidbody2D.velocity = Vector2.up * JumpVelocity;
        }

        //Joystick Movement
        if (joystick.Horizontal > 0)
        {
            PlayerRigidbody2D.velocity = new Vector2(+MovementSpeed * joystick.Horizontal, PlayerRigidbody2D.velocity.y);
            Playermodel.transform.rotation = new Quaternion(0, 0, 0, 0);

            //Sets the player animation to move
            changeState(STATE_WALK);
        }
        if (joystick.Horizontal < 0)
        {
            PlayerRigidbody2D.velocity = new Vector2(MovementSpeed * joystick.Horizontal, PlayerRigidbody2D.velocity.y);
            Playermodel.transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        //player health / damage
        if (PlayerHealth <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }
    //Changes the players animation state
    void changeState(int state)
    {
        if (_currentAnimationState == state)
            return;
        switch (state)
        {
            case STATE_WALK:
                animator.SetInteger("state", STATE_WALK);
                break;

            case STATE_IDLE:
                animator.SetInteger("state", STATE_IDLE);
                break;

            case STATE_BLOCK:
                animator.SetInteger("state", STATE_BLOCK);
                break;

            case STATE_ATTACK:
                animator.SetInteger("state", STATE_ATTACK);
                break;
        }
        _currentAnimationState = state;
    }


    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(PCollider.bounds.center, PCollider.bounds.size, 0f, Vector2.down, .1f, Platformslayermask);
        //Debug.Log(raycastHit2D.collider);
        return raycastHit2D.collider != null;
    }

}
