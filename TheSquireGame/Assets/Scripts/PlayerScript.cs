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
    GameObject shieldtest;

    public Joystick joystick;
    public Rigidbody2D PlayerRigidbody2D;
    public Collider2D PCollider;

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
            Debug.Log("jump");
            PlayerRigidbody2D.velocity = Vector2.up * JumpVelocity;
            
        }
        // joystick movement
        if(joystick.Horizontal > 0)
        {
            PlayerRigidbody2D.velocity = new Vector2(+MovementSpeed * joystick.Horizontal, PlayerRigidbody2D.velocity.y);
            Playermodel.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        if (joystick.Horizontal < 0)
        {
            PlayerRigidbody2D.velocity = new Vector2(MovementSpeed * joystick.Horizontal , PlayerRigidbody2D.velocity.y);
            Playermodel.transform.rotation = new Quaternion(0, 180, 0, 0);
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
        }
    }

    public void Block()
    {
        Blocking = true;
        shieldtest = Instantiate(Shield, SpawnSpot.position, SpawnSpot.rotation);
        shieldtest.transform.parent = gameObject.transform;
    }

    public void NotBlock()
    {
        Blocking = false;
        Destroy(shieldtest);
    }
}
