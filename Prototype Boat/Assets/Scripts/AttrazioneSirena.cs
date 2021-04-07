using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttrazioneSirena : MonoBehaviour
{

    public float forzaOnda;
    protected Vector3 dirOnda;
    protected List<Rigidbody> rb2d;
    protected int i;
    protected ParticleSystem ps;
    protected bool devoMorire;
    private SphereCollider sc;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        ps = GetComponentInChildren<ParticleSystem>();
        devoMorire = false;
        rb2d = new List<Rigidbody>();
        sc = GetComponent<SphereCollider>();
    }


    protected void FixedUpdate()
    {
        if (!devoMorire)
        {
            if (rb2d.Count != 0)
            {
                i = 0;
                foreach (Rigidbody el in rb2d)
                {
                    if (el != null)
                    {
                        dirOnda = Posizione() - el.transform.position;
                        el.AddForce(dirOnda.normalized * forzaOnda);
                        Debug.DrawLine(el.transform.position, el.transform.position + dirOnda, Color.red, Time.fixedDeltaTime);
                    }
                    i++;
                }
            }
        }
    }

    protected virtual void OnTriggerEnter(Collider collision)
    {
        if (!collision.isTrigger && collision.tag=="Creature")
            rb2d.Add(collision.gameObject.GetComponent<Rigidbody>());
    }



    protected virtual void OnTriggerExit(Collider collision)
    {
        if (!collision.isTrigger && collision.tag == "Creature")
            rb2d.Remove(collision.gameObject.GetComponent<Rigidbody>());
    }

   
   private Vector3 Posizione()
    {
        return new Vector3(transform.position.x, 0, transform.position.z);
    }
   

}
