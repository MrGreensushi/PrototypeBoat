using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OndaEsplosione : Onda
{
    public int danni;
    private CircleCollider2D c2d;
    protected override void Start()
    {
        c2d = GetComponent<CircleCollider2D>();
        base.Start();
       
    }
    protected override void ComeMiMuovo()
    {
        transform.localScale = Vector3.one * (spazio_percorso * 2);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if(collision.GetComponent<Corpo>()!= null)
        {
            collision.GetComponent<Corpo>().SubiscoDanni(danni);
        }
    }

    protected override void CalcoloDirezione()
    {
        if (rb2d[i] != null)
        {
            dirOnda = (rb2d[i].transform.position - this.transform.position).normalized;
            if (dirOnda.magnitude == 0)
                dirOnda = -rb2d[i].transform.right;
        }
    }
}
