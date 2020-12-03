using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : Collectible
{
    [SerializeField]
    private int value = 1;

    public int GetValue()
    {
        if (!collected)
        {
            collected = true;
            return value;
        }
        return 0;
    }

}
