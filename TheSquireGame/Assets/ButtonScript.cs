using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

    public class ButtonScript : MonoBehaviour 
{
    public UnityEvent buttonDown;
    public UnityEvent buttonUp;
    public Sprite ButtonDownSprite;
    public Sprite ButtonUpSprite;

    void Awake()
    {
        if(buttonDown == null)
        {
            buttonDown = new UnityEvent();
        }
        if (buttonUp == null)
        {
            buttonUp = new UnityEvent();
        }
    }
    void OnMouseDown()
    {
        Debug.Log("down");
        buttonDown.Invoke();
        GetComponent<SpriteRenderer>().sprite = ButtonDownSprite;
    }
    void OnMouseUp()
    {
        Debug.Log("up");
        buttonUp.Invoke();
        GetComponent<SpriteRenderer>().sprite = ButtonUpSprite;
    }
}
