using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OndaDacqua : Onda
{
    private MeshCollider pc2d;

    protected override void Start()
    {
       // pc2d = GetComponent<MeshCollider>();
        base.Start();
    }
    protected override void ComeMiMuovo()
    {
        
        transform.position += dirOnda * velOnda * Time.deltaTime;
    }

    protected override void CalcoloDirezione()
    {
        
    }

    protected override void OnTriggerEnter(Collider collision)
    {
       
        if (collision.gameObject.GetComponent<TerraFerma>() != null)
        {
           
            Destroy(gameObject);
        }
        base.OnTriggerEnter(collision);
       
    }
}
