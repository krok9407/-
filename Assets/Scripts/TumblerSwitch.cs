using UnityEngine;

public class TumblerSwitch : MonoBehaviour
{
    [SerializeField] private Texture2D cursorDefault;
    [SerializeField] private Texture2D cursorHand;

    [HideInInspector]public bool _enabled = false;

    [SerializeField] private EnablingLight _enablingLight1;
    [SerializeField] private EnablingLight _enablingLight2;


       
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
            Vector3 rotate = this.transform.eulerAngles;
            rotate.z = -65;
            this.transform.rotation = Quaternion.Euler(rotate);
            _enabled = false;
            _enablingLight1.AdditionalCheckFalse();
            _enablingLight2.AdditionalCheckFalse(); 
        }
        else
        {
            Vector3 rotate = this.transform.eulerAngles;
            rotate.z = 60;
            this.transform.rotation = Quaternion.Euler(rotate);
            _enabled = true;
            _enablingLight1.AdditionalCheckTrue();
            _enablingLight2.AdditionalCheckTrue();
        }
    }
}
