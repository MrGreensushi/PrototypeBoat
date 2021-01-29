using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerraFerma : Corpo
{
    protected override void Start()
    {
        base.Start();
        possosubire_danni = false;
        dannicorpoacorpo = 1;
    }
    protected override void ComeSubiscoDanni(int danni)
    {
       
    }
}
