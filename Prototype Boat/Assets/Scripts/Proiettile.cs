using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proiettile : MonoBehaviour
{
    public int danni;
    public float velocity;
    public Vector3 direzione;
    private Rigidbody rb;
    private float tempo;
    public float TempoPrimaDiMorire;
    protected void Start()
    {
        tempo = Time.time;
        rb = GetComponent<Rigidbody>();
        rb.velocity = direzione * velocity;

    }
    private void FixedUpdate()
    {
       
        //rb.velocity = -direzione *velocity* ((-Mathf.Atan(Time.time -tempo))/Mathf.PI/2);
        if (rb.velocity.magnitude < 0.1f)
        {
            Debug.Log("Morto perché troppo lento");
            Destroy(gameObject);
        }
            
        if ((Time.time - tempo)>= TempoPrimaDiMorire)
        {
            rb.useGravity = true;
           
        }
        if ((Time.time - tempo) >= TempoPrimaDiMorire*1.2f)
        {
            Debug.Log("Morto perché + scaduto il tempo");
            Destroy(gameObject);
        }

    }
    
    protected void OnTriggerEnter(Collider collision)
    {
        if (!collision.isTrigger) {
            if (collision.gameObject.GetComponent<Corpo>() != null )
            {
                collision.gameObject.GetComponent<Corpo>().SubiscoDanni(danni);
                Debug.Log("Ho colpito: " + collision.gameObject);
                Destroy(gameObject);

            }
            if (collision.transform.parent != null)
                if (collision.transform.parent.GetComponent<Corpo>() != null)
                {
                    collision.transform.parent.GetComponent<Corpo>().SubiscoDanni(danni);
                    Debug.Log("Ho colpito: " + collision.gameObject);
                    Destroy(gameObject);
                }
        }
    }
}
