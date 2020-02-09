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
    bool Up;
    bool Down;
    public bool Ladder;

   
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
        if (Up == true && Ladder == true)
        {
            Player.transform.Translate(Vector2.up * MovementSpeed * Time.deltaTime);
        }
        if (false == true && Ladder == true)
        {
            Player.transform.Translate(Vector2.down * MovementSpeed * Time.deltaTime);
        }

    }
    public void MoveRight()
    {
        right = true; 
    }

    public void MoveLeft()
    {
        Left = true;
    }

    public void StopRight()
    {
        right = false;
    }

    public void StopLeft()
    {
        Left = false;
    }

    public void MoveUp()
    {
        Up = true;
    }

    public void StopUp()
    {
        Up = false;
    }

    public void MoveDown()
    {
        Down = true;
    }

    public void StopDown()
    {
        Down = false;
    }
}
