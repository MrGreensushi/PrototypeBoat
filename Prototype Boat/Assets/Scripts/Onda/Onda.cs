using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Onda : MonoBehaviour
{
    public float distanzaprimadimorire;
    public float velOnda;
    public float forzaOnda;
    protected Vector3 dirOnda;
    protected List<Rigidbody2D> rb2d;
    protected float spazio_percorso;
    protected int i;
  
    // Start is called before the first frame update
    protected virtual void  Start()
    {
        rb2d = new List<Rigidbody2D>();
        dirOnda = transform.up;
        StartCoroutine(Movimento());        
    }


    protected void FixedUpdate()
    {
        if (rb2d.Count != 0)
        {
            i = 0;
            foreach (Rigidbody2D el in rb2d)
            {
                CalcoloDirezione();
                el.AddForce(dirOnda * forzaOnda);
                i++;
            }
        }
    }

    protected virtual void  OnTriggerEnter2D(Collider2D collision)
    {
        rb2d.Add( collision.gameObject.GetComponent<Rigidbody2D>());
    }



    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        rb2d.Remove(collision.gameObject.GetComponent<Rigidbody2D>());
    }

    protected IEnumerator Movimento()
    {
        while (spazio_percorso < distanzaprimadimorire)
        {
            yield return new WaitForFixedUpdate();
            spazio_percorso += velOnda * Time.deltaTime;
            ComeMiMuovo();
        }
        Destroy(gameObject);
    }

    protected abstract void ComeMiMuovo();

    protected abstract void CalcoloDirezione();

    public void SetDirezione(float dir)
    {
        transform.rotation.Set(0,0,dir,0);
    }

}
