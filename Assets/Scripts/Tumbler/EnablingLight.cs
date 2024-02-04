
using UnityEngine;

public class EnablingLight : MonoBehaviour
{
   
    private bool _enabled = false;
    [SerializeField] private TumblerSwitch tumblerSwitch_1; 
    [SerializeField] private CheckConnection checkConnection;
    [SerializeField] private MeshRenderer _lamp;
    private GameObject _light;
    private Material defaultMat;
    [SerializeField] private Material lightMat;

    private void Start() {
        defaultMat = _lamp.material;
        _light = _lamp.gameObject.transform.GetChild(0).gameObject;
    }

     public void AdditionalCheckTrue()
    {
        if (checkConnection.CheckConnectionBlockk1() && tumblerSwitch_1._enabled)
            {
                    _light.SetActive(true);
                    _lamp.material = lightMat;
            }
    }

    public void AdditionalCheckFalse()
    {
        _enabled = false;
            _light.SetActive(false);
            _lamp.material = defaultMat;
    }
}
