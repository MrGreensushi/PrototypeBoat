using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proiettile : MonoBehaviour
{
    public int danni;
    public float velocity;
    public Vector3 direzione;
    private Rigidbody2D rb;
    private float tempo;
    protected void Start()
    {
        tempo = Time.time;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direzione * velocity;

    }
    private void FixedUpdate()
    {
       
        //rb.velocity = -direzione *velocity* ((-Mathf.Atan(Time.time -tempo))/Mathf.PI/2);
        if (rb.velocity.magnitude < 0.1f)
            Destroy(gameObject);

    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Corpo>() != null)
        {
            collision.gameObject.GetComponent<Corpo>().SubiscoDanni(danni);
            Destroy(gameObject);

        }
            
    }
}
