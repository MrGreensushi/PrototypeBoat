using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OndaEsplosione : Onda
{
    public int danni;
    private SphereCollider c2d;
    protected override void Start()
    {
        c2d = GetComponent<SphereCollider>();
        base.Start();
        ps=this.GetComponent<ParticleSystem>();
  
    }
    protected override void ComeMiMuovo()
    {
        transform.localScale = Vector3.one * (spazio_percorso * 2);
    }

    protected override void OnTriggerEnter(Collider collision)
    {
        if (!collision.isTrigger)
        {
            base.OnTriggerEnter(collision);
            if (collision.GetComponent<Corpo>() != null)
            {
                collision.GetComponent<Corpo>().SubiscoDanni(danni);
            }
            if (collision.transform.parent != null)
            {
                if (collision.transform.parent.GetComponent<Corpo>() != null)
                {
                    collision.transform.parent.GetComponent<Corpo>().SubiscoDanni(danni);
                }
            }
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
