using UnityEngine;

public class BlockClemm : MonoBehaviour
{
    public int blackLine;
    public int blueLine;

    Transform[] _allChildren;

    Clemma clemma;

    public void CheckingTheNumberOfColors()
    {
        _allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in _allChildren)
        {
            clemma = child.gameObject.GetComponent<Clemma>();
            if (clemma != null && clemma.isSet && !clemma.isOpen && !clemma.isCheck)
            {
                if (clemma.typeWire == "black"){
                    blackLine += 1;
                }
                else if (clemma.typeWire == "blue"){
                    blueLine += 1;
                }
                clemma.isCheck = true;
            }
        }
    }
}
