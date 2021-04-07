using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Onda : MonoBehaviour
{
    public float distanzaprimadimorire;
    public float velOnda;
    public float forzaOnda;
    protected Vector3 dirOnda;
    protected List<Rigidbody> rb2d;
    protected float spazio_percorso;
    protected int i;
    protected ParticleSystem ps;
    protected bool devoMorire;
  
    // Start is called before the first frame update
    protected virtual void  Start()
    {
        devoMorire = false;
        rb2d = new List<Rigidbody>();
        dirOnda = transform.forward;
        StartCoroutine(Movimento());        
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
                    CalcoloDirezione();
                    el.AddForce(dirOnda * forzaOnda);
                    i++;
                }
            }
        }
    }

    protected virtual void  OnTriggerEnter(Collider collision)
    {
        if (!collision.isTrigger)
        {
            if (collision.gameObject.GetComponent<Rigidbody>() != null)
            {
                rb2d.Add(collision.gameObject.GetComponent<Rigidbody>());
            }
            else if (collision.gameObject.GetComponentInParent<Rigidbody>() != null)
            {
                rb2d.Add(collision.gameObject.GetComponentInParent<Rigidbody>());
            }
            else if (collision.gameObject.GetComponentInChildren<Rigidbody>() != null)
            {
                rb2d.Add(collision.gameObject.GetComponentInChildren<Rigidbody>());
            }
        }
        
    }



    protected virtual void OnTriggerExit(Collider collision)
    {
        if(!collision.isTrigger)
        rb2d.Remove(collision.gameObject.GetComponent<Rigidbody>());
    }

    protected IEnumerator Movimento()
    {
        while (spazio_percorso < distanzaprimadimorire)
        {
            yield return new WaitForFixedUpdate();
            spazio_percorso += velOnda * Time.deltaTime;
            ComeMiMuovo();
        }
        devoMorire = true;
        if (ps==null)
            Destroy(gameObject);
        else
        {
            while (ps.isPlaying) {
                yield return new WaitForFixedUpdate();

            }
            Destroy(gameObject);
        }
            


    }

    protected abstract void ComeMiMuovo();

    protected abstract void CalcoloDirezione();

    public void SetDirezione(float dir)
    {
        transform.rotation.Set(0,0,dir,0);
    }

}
