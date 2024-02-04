using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rosette : MonoBehaviour
{
    [SerializeField] private Texture2D cursorDefault;
    [SerializeField] private Texture2D cursorHand;
    
    [SerializeField] private bool _enabled = false;

    [SerializeField] private Animator animator1;
    [SerializeField] private Animator animator2;
    private void OnMouseEnter()
    {
        Cursor.SetCursor(cursorHand, Vector2.zero, CursorMode.ForceSoftware);
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(cursorDefault, Vector2.zero, CursorMode.ForceSoftware);
    }

    private void OnMouseDown()
    {
        if (_enabled)
        {
            if ((this.gameObject.name == "Rosette1"))
            {
                animator1.SetTrigger("Close");
            }
            if ((this.gameObject.name == "Rosette2"))
            {
                animator2.SetTrigger("Close");
            }
            _enabled = false;
        }
        else
        {
            if ((this.gameObject.name == "Rosette1"))
            {
                animator1.SetTrigger("Open");   
            }
            if ((this.gameObject.name == "Rosette2"))
            {    
                animator2.SetTrigger("Open");
            }
            _enabled = true;
        }
    }
}
