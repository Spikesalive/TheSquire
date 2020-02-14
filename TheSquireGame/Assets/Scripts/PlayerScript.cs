using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update

    public float MovementSpeed;

    public GameObject Player;
    public int PlayerHealth;
    bool Left;
    bool right;

    public GameObject Playermodel;

    bool Blocking;
    public GameObject Sword;
    public GameObject Shield;
    public Transform SpawnSpot;
    GameObject shieldtest;
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Left == true)
        {
            Player.transform.Translate(Vector2.left * MovementSpeed * Time.deltaTime);
        }
        if (right == true)
        {
            Player.transform.Translate(Vector2.right * MovementSpeed * Time.deltaTime);
        }
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
    // player movement
    public void MoveRight()
    {
        right = true;
        Playermodel.transform.rotation = new Quaternion(0, 0, 0,0);
    }

    public void MoveLeft()
    {
        Left = true;
        Playermodel.transform.rotation = new Quaternion(0, 180, 0, 0);
    }

    public void StopRight()
    {
        right = false;
    }

    public void StopLeft()
    {
        Left = false;
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
