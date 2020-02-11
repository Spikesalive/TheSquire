using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderScript : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D col)
    {
        Debug.Log("ladderEnter");
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("ladderEnter");
            col.GetComponent<PlayerScript>().Ladder = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("ladderExit");
            col.GetComponent<PlayerScript>().Ladder = false;
        }
    }

   

   
}
