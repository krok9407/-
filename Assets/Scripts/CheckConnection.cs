using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckConnection : MonoBehaviour
{
    [SerializeField] List<BlockClemm> blockClemms = new List<BlockClemm>();

    public bool CheckConnectionBlockk1()
    {
        for (int i = 0; i < blockClemms.Count; i++)
        {
            blockClemms[i].CheckingTheNumberOfColors();
        }
        if (blockClemms[2].blueLine == 3){
            if (blockClemms[3].blueLine == 1 && blockClemms[3].blackLine == 1 && blockClemms[4].blueLine == 1 
                && blockClemms[4].blackLine == 1 && blockClemms[5].blackLine == 2)
            {
                return true;
            }
            else if (blockClemms[3].blueLine == 1 && blockClemms[3].blackLine == 1 && blockClemms[5].blueLine == 1 
                && blockClemms[5].blackLine == 1 && blockClemms[4].blackLine == 2)
            {
                return true;
            }
            else if (blockClemms[4].blueLine == 1 && blockClemms[4].blackLine == 1 && blockClemms[5].blueLine == 1 
                && blockClemms[5].blackLine == 1 && blockClemms[3].blackLine == 2)
            {
                return true;
            }
        }

        return false;
    }

    public bool CheckConnectionBlockk2()
    {
        for (int i = 0; i < blockClemms.Count; i++)
        {
            blockClemms[i].CheckingTheNumberOfColors();
        }
        if (blockClemms[0].blackLine == 3 && blockClemms[1].blueLine == 3 || blockClemms[1].blackLine == 3 && blockClemms[0].blueLine == 3) 
        {
            return true;
        }
        return false;

    }
}
