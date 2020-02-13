using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update

    public float MovementSpeed;

    public GameObject Player;
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
    }

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
