using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInstruction : MonoBehaviour
{
    [SerializeField] private Texture2D cursorDefault;
    [SerializeField] private Texture2D cursorLoupe;

    private Animator animator;
    [SerializeField] private bool isOpen = false;

    [SerializeField] private Camera camera;
    private bool cameraStartPosition = false;
    private float time = 0;
    private Vector3 startPosition;
    

    void Start()
    {
        Cursor.SetCursor(cursorDefault, Vector2.zero, CursorMode.ForceSoftware);
        animator = this.GetComponent<Animator>();
        startPosition = camera.transform.position;
    }

    void Update()
    {
        if (cameraStartPosition){
            camera.transform.position = Vector3.Lerp(startPosition, new Vector3 (0, 1, -10), time);
            time += Time.deltaTime;
            if (time >= 1f){
                cameraStartPosition = false;
                print("sss");
            }
        }
    }

    private void OnMouseEnter()
    {
        Cursor.SetCursor(cursorLoupe, Vector2.zero, CursorMode.ForceSoftware);
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(cursorDefault, Vector2.zero, CursorMode.ForceSoftware);
    }

    private void OnMouseDown()
    {
        cameraStartPosition = true;
        if (isOpen){
            animator.SetTrigger("Close");
        }
        else{
            animator.SetTrigger("Open");
        }
        isOpen = !isOpen;
    }
}
