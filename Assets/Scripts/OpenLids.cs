using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLids : MonoBehaviour
{
    [SerializeField] private Texture2D cursorDefault;
    [SerializeField] private Texture2D cursorHand;
    private Animator animator;
    private bool isOpen = false;

    [SerializeField] private Camera camera;
     private bool isMove = false;
    private float time = 0;
    private Vector3 startPosition;
    [SerializeField] private float speed  = 2.5f;
    private Vector3 targetPosition;
    [SerializeField] private float cameraOffset = 2.5f;
    [SerializeField] bool isAnimation = false;
    void Start()
    {
        if(isAnimation) animator = transform.parent.GetComponent<Animator>();
        startPosition = camera.transform.position;
        targetPosition = transform.position;
        camera.orthographicSize = cameraOffset;
        targetPosition.z -= cameraOffset*2;
    }

    private void OnMouseOver()
    {
        if(!isMove){
            Cursor.SetCursor(cursorHand, Vector2.zero, CursorMode.ForceSoftware);
        }
        else{
             Cursor.SetCursor(cursorDefault, Vector2.zero, CursorMode.ForceSoftware);
        }
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(cursorDefault, Vector2.zero, CursorMode.ForceSoftware);
    }

    private void OnMouseDown()
    {       
        if(!isMove){
        isMove = true;
            if (isOpen){
                if(isAnimation) animator.SetTrigger("Close");
                camera.orthographic = false;
                StartCoroutine(MoveToTarget(startPosition)); 
            }
            else{
                if(isAnimation) animator.SetTrigger("Open");
                StartCoroutine(MoveToTarget(targetPosition)); 
            }
        }
    }
    IEnumerator MoveToTarget(Vector3 target)
    {
        yield return new WaitForEndOfFrame();
        camera.transform.position = Vector3.Lerp(camera.transform.position, target, speed * Time.deltaTime);
        if(Vector3.Distance(camera.transform.position, target)>0.1f){
            StartCoroutine(MoveToTarget(target));
        }
        else{
            if(!isOpen)
            {
                camera.orthographic = true;
                camera.orthographicSize = cameraOffset;
                targetPosition.z -= cameraOffset*2;
            }
            isOpen = !isOpen;
            isMove = false;
        }
    }
}
