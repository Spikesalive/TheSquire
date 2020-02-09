using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModelScript : MonoBehaviour
{
    
    public PlayerScript PlayerScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ladder"))
        {
            Debug.Log("ladderEnter");
            PlayerScript.Ladder = true;
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ladder"))
        {
            Debug.Log("ladderExit");
            PlayerScript.Ladder = false;
        }
    }
}
