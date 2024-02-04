using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChainPieces : MonoBehaviour
{
    [SerializeField] private Texture2D cursorDefault;
    [SerializeField] private Texture2D cursorHand;

    [SerializeField] private bool isOpen = false;

    [SerializeField] private Camera camera;
    // private bool cameraStartPosition = false;
    private float time = 0;
    private Vector3 startPosition;
    private float speed  = 5f;
    private Vector3 positionCameraWithBox1 = new Vector3 (2.9f, 7.5f, -10f);
    private Vector3 positionCameraWithBox2 = new Vector3 (2.9f, 1f, -10f);
    

    void Start()
    {
        startPosition = camera.transform.position;
    }


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
        if (isOpen){
            if (this.gameObject.name == "Boxs1"){
                camera.transform.position = startPosition;
                camera.orthographic = false;
                // camera.transform.position = Vector3.Lerp(camera.transform.position, new Vector3 (1.32f, 0.67f, -4.91f), speed * Time.deltaTime);
            }
            else if (this.gameObject.name == "Boxs2"){
                camera.transform.position = startPosition;
                camera.orthographic = false;
            }
            Vector3 rotate = camera.transform.localEulerAngles;
                rotate.x = 0;
                camera.transform.localRotation = Quaternion.Euler(rotate);
        }
        else{
            if (this.gameObject.name == "Boxs1"){
                camera.transform.position = positionCameraWithBox1;

                Vector3 rotate = camera.transform.localEulerAngles;
                rotate.x = 25;
                camera.transform.localRotation = Quaternion.Euler(rotate);

                // camera.transform.position = Vector3.Lerp(camera.transform.position, new Vector3 (1.32f, 0.67f, -4.91f), speed * Time.deltaTime);
            }
            else if (this.gameObject.name == "Boxs2"){
                camera.transform.position = positionCameraWithBox2;

                Vector3 rotate = camera.transform.localEulerAngles;
                rotate.x = -15;
                camera.transform.localRotation = Quaternion.Euler(rotate);
            }
            camera.orthographic = true;
            camera.orthographicSize = 0.6f;
        }
        isOpen = !isOpen;
    }
}
