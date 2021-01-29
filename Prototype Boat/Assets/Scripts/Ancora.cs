using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ancora : MonoBehaviour
{
    public float forzadiRottura;
    public float distanza;
   // public Rigidbody2D barca;
   public Rigidbody2D barca;
    [HideInInspector] public int adestra;
    private float disattuale;
    private float angolo;
    private float velperpendicolarecerchio;
    private float velparallela;


    private void FixedUpdate()
    {
        //distanza attuale tra nave e ancora
        disattuale = Mathf.Abs((barca.transform.position - transform.position).magnitude);
        if (disattuale >= distanza)
        {
            //direzione normale tra la barca e l'ancora
            Vector2 direzione = barca.transform.position - transform.position;
            direzione = direzione.normalized;
            //calcolo la componente della velocità, della barca, perpendicolare  e  parallela rispetto al cerchio
            angolo = Vector2.Angle(direzione, barca.velocity.normalized) * Mathf.PI / 180;
            velperpendicolarecerchio = barca.velocity.magnitude * Mathf.Sin(angolo);
            velparallela = barca.velocity.magnitude * Mathf.Cos(angolo);
            //accellerazione moto circolare uniformememnte accelerato
            barca.AddForce(-direzione * velperpendicolarecerchio * velperpendicolarecerchio / distanza);
            //elimino la componente parallela
            barca.AddForce(-direzione * velparallela * 1.1f, ForceMode2D.Impulse);
            //aggiugno una forza torcete rispetto alla direzione della barca
            barca.AddTorque(-Mathf.Sign(velperpendicolarecerchio) * 3*adestra);

        }
    }
}
