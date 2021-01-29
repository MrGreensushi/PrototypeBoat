using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barili : Corpo
{
    public GameObject onda;
    public float forzaesplosione;
    public int raggioesplosione;
   
    protected override void ComeSubiscoDanni(int danni)
    {
        Esplosione();
    }

    private void Esplosione()
    {
        Instantiate(onda,transform.position,Quaternion.identity);
        Morte();
    }

   
}
