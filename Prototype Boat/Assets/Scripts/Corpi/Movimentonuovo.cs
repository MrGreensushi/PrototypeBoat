using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimentonuovo : Corpo
{
    public float[] costantiTelecamera;
    public float driftingreduction;
    public float maxspeedCorsa;
    public float maxSpeedRemi;
    public float rotazione;
    public float distanzaAncora;
    public GameObject ancoraPrefab;
    public float velocitaattuale;

    
    private MeshCollider pc2d;
    private Camera maincamera;
    protected bool velaattivata;
    private Vector3 forzaFrontale;
    private float velocitaX;
    private float angoloInRadianti;
    private GameObject ancora;
    private Sparare cannone;
    private bool possoAttivareVela;
    private ParticleSystem particelleDanni;

   
    protected override void Start()
    {
        possoAttivareVela = true;
        cannone = this.GetComponent<Sparare>();
        rb2d = this.GetComponent<Rigidbody>();
        pc2d = this.GetComponent<MeshCollider>();
        maincamera = GameObject.FindObjectOfType<Camera>();
        base.Start();
        particelleDanni = GetComponentInChildren<ParticleSystem>();
    }

   
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && possoAttivareVela)
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
            cannone.Spara(transform.right, transform.position+transform.right*2 + transform.up * 0.5f);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            cannone.Spara(-transform.right, transform.position - transform.right * 2 + transform.up * 0.5f);
        }

    }

    private void FixedUpdate()
    {
        //Aggiorno la posizione della telecamera sul protagonista
        // maincamera.transform.position = new Vector3(transform.position.x + costantiTelecamera[0], maincamera.transform.position.y, costantiTelecamera[2]+transform.position.z);
        aggiornoPosTelecamera();
        //Timone
        //se ho premuto il tasto A
        if (Input.GetKey(KeyCode.A))
        {
          
           rb2d.MoveRotation(Quaternion.Euler(transform.rotation.x, rb2d.rotation.eulerAngles.y - rotazione, transform.rotation.z)) ;
   
        }
        else
        {//altrimenti se ho premuto il tasto D
            if (Input.GetKey(KeyCode.D))
            {

                rb2d.MoveRotation(Quaternion.Euler(transform.rotation.x, rb2d.rotation.eulerAngles.y + rotazione, transform.rotation.z));
               // rb2d.rotation -= rotazione;
            }
        }

        forzaFrontale = new Vector3(0, 0, 0);

        if (velaattivata)
        {

            forzaFrontale=transform.forward * maxspeedCorsa;
        }
        else if (Input.GetKey(KeyCode.W) && rb2d.velocity.magnitude <= maxSpeedRemi)
        {

            rb2d.AddForce(transform.forward * maxSpeedRemi);

        }
        //Applica una forza di accelerazione solo se la velocità attuale è minore di quella massima
        if (velocitaattuale <= maxspeedCorsa)
            rb2d.AddForce(forzaFrontale);

        velocitaattuale = rb2d.velocity.magnitude;

        //angolo tra asse x e velocità
        angoloInRadianti = Vector3.Angle(transform.right, rb2d.velocity) * Mathf.PI / 180;
       
        //componente su asse x= vett.velocita x cos(angolo tra asse x e velocita)
        velocitaX = velocitaattuale * Mathf.Cos(angoloInRadianti);
      
        //Se il drifting (velocicàX) è diverso da zero applica una forza che serve a smorzarlo
        if (Mathf.Abs(velocitaX) >=0.1f)
        {
            rb2d.AddForce(-Mathf.Sign(velocitaX) * transform.right * driftingreduction);
        }
        transform.position =new Vector3(transform.position.x, 0,transform.position.z);
    }

    public Vector3 GetDriftingReduction()
    {
        return (-Mathf.Sign(velocitaX) * transform.right * driftingreduction);
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
        particelleDanni.Play();
        vita -= danni;
    }

    public void BloccoAlleVelePerXSecondi(float tempo)
    {
        StartCoroutine(AspettaXSecondi(tempo));
    }

    IEnumerator AspettaXSecondi(float x)
    {
        possoAttivareVela = false;
        velaattivata = false;
        yield return new WaitForSeconds(x);
        possoAttivareVela = true;
    }

    private void aggiornoPosTelecamera()
    {
        /*maincamera.transform.position =transform.position+ this.transform.forward * costantiTelecamera[2] + transform.right * costantiTelecamera[0] + transform.up * costantiTelecamera[1];
        maincamera.transform.rotation = Quaternion.Euler(maincamera.transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);*/
        maincamera.transform.position =transform.position-new Vector3(0,0,1) * 31.75f + new Vector3(0,1,0) * 55;
    }
}
