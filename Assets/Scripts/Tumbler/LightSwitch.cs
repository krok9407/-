using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    
    [SerializeField] private Texture2D cursorDefault;
    [SerializeField] private Texture2D cursorHand;
    private bool _enabled = false;

    private EnablingLight _enablingLight;
    void Start()
    {
        _enablingLight = GetComponent<EnablingLight>();    
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
        Vector3 rotate;
        if (_enabled)
        {
            rotate = this.transform.eulerAngles;
            rotate.x = -5;
            this.transform.rotation = Quaternion.Euler(rotate);
            _enabled = false;
            _enablingLight.AdditionalCheckFalse();
        }
        else
        {
            rotate = this.transform.eulerAngles;
            rotate.x = 5;
            this.transform.rotation = Quaternion.Euler(rotate);
            _enabled = true;
            _enablingLight.AdditionalCheckTrue();
        }
    }
}