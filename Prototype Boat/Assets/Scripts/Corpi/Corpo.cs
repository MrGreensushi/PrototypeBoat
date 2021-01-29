using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Corpo : MonoBehaviour
{
    public int vita;
    protected Rigidbody2D rb2d;
    public float tempoinvulnerabile;
    public bool possosubire_danni=true;
    public int dannicorpoacorpo;

    protected virtual void Start()
    {
        possosubire_danni = true;
    }
    public void SubiscoDanni(int damage)
    {
        
        if (possosubire_danni)
        {
            if(damage>0)
            StartCoroutine(Invulnerabile(tempoinvulnerabile));
            ComeSubiscoDanni(damage);
            if (vita <= 0)
                Morte();
        }
    }

    protected abstract void ComeSubiscoDanni(int danni);

    public virtual void Morte()
    {
        Destroy(this.gameObject);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Corpo>() != null)
        {
            SubiscoDanni(collision.gameObject.GetComponent<Corpo>().dannicorpoacorpo);
        }
        
    }

    protected IEnumerator Invulnerabile(float tempo)
    {
        possosubire_danni = false;
        yield return new WaitForSeconds(tempo);
        possosubire_danni = true;

    }
}
