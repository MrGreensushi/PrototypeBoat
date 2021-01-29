using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OndaDacqua : Onda
{
    private PolygonCollider2D pc2d;

    protected override void Start()
    {
        pc2d = GetComponent<PolygonCollider2D>();
        base.Start();
    }
    protected override void ComeMiMuovo()
    {
        
        transform.position += dirOnda * velOnda * Time.deltaTime;
    }

    protected override void CalcoloDirezione()
    {
        
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.gameObject.GetComponent<TerraFerma>() != null)
        {
           
            Destroy(gameObject);
        }
        base.OnTriggerEnter2D(collision);
       
    }
}
