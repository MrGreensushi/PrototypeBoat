using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricata : Corpo
{
    
    protected override void ComeSubiscoDanni(int danni)
    {
        vita -= 1;
    }
}
