using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimentonuovo : Corpo
{
   
    public float driftingreduction;
    public float maxspeedCorsa;
    public float maxSpeedRemi;
    public float rotazione;
    public float distanzaAncora;
    public GameObject ancoraPrefab;
    public float velocitaattuale;

    
    private PolygonCollider2D pc2d;
    private Camera maincamera;
    protected bool velaattivata;
    private Vector3 forzaFrontale;
    private float velocitaX;
    private float angoloInRadianti;
    private GameObject ancora;
    private Sparare cannone;


   
    protected override void Start()
    {
        cannone = this.GetComponent<Sparare>();
        rb2d = this.GetComponent<Rigidbody2D>();
        pc2d = this.GetComponent<PolygonCollider2D>();
        maincamera = GameObject.FindObjectOfType<Camera>();
        base.Start();
    }

   
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            velaattivata = !velaattivata;

        if (Input.GetKeyDown(KeyCode.E))
        {
            InizializzaAncora(1);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            InizializzaAncora(-1);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            cannone.Spara(transform.up, transform.position+transform.up*2);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            cannone.Spara(-transform.up, transform.position - transform.up * 2);
        }

    }

    private void FixedUpdate()
    {
        //Aggiorno la posizione della telecamera sul protagonista
        maincamera.transform.position = new Vector3(transform.position.x, transform.position.y, maincamera.transform.position.z);

        //Timone
        //se ho premuto il tasto A
        if (Input.GetKey(KeyCode.A))
        {
          
            rb2d.rotation += rotazione;

        }
        else
        {//altrimenti se ho premuto il tasto D
            if (Input.GetKey(KeyCode.D))
            {
                
                rb2d.rotation -= rotazione;
            }
        }

        forzaFrontale = new Vector3(0, 0, 0);

        if (velaattivata)
        {

            forzaFrontale=-transform.right * maxspeedCorsa;
        }
        else if (Input.GetKey(KeyCode.W) && rb2d.velocity.magnitude <= maxSpeedRemi)
        {

            rb2d.AddForce(-transform.right * maxSpeedRemi);

        }
        //Applica una forza di accelerazione solo se la velocità attuale è minore di quella massima
        if (velocitaattuale <= maxspeedCorsa)
            rb2d.AddForce(forzaFrontale);

        velocitaattuale = rb2d.velocity.magnitude;

        //angolo tra asse x e velocità
        angoloInRadianti = Vector3.Angle(transform.up, rb2d.velocity) * Mathf.PI / 180;
       
        //componente su asse x= vett.velocita x cos(angolo tra asse x e velocita)
        velocitaX = velocitaattuale * Mathf.Cos(angoloInRadianti);
      
        //Se il drifting (velocicàX) è diverso da zero applica una forza che serve a smorzarlo
        if (Mathf.Abs(velocitaX) >=0.1f)
        {
            rb2d.AddForce(-Mathf.Sign(velocitaX) * transform.up * driftingreduction);
        }
   
    }


    private void InizializzaAncora(int adestra)
    {
       
        if (ancora == null)
        {

            ancora = Instantiate(ancoraPrefab, adestra * rb2d.transform.up * distanzaAncora + rb2d.transform.position, Quaternion.identity);
            ancora.GetComponent<Ancora>().barca=rb2d;
            rb2d.drag /= 2;
            ancora.GetComponent<Ancora>().adestra = adestra;
        }
        else
        {
            if (ancora != null)
            {
                rb2d.drag *= 2;
                Destroy(ancora.gameObject);
            }
        }



    }

    protected override void ComeSubiscoDanni(int danni)
    {
        vita -= danni;
    }
}
