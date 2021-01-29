using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sparare : MonoBehaviour
{
    public GameObject proiettile;
    public int numeroDiProiettili;
    public float tempoRicarica;
    public float rinculo;

    private bool carico;
    private GameObject oggettoInstanziato;

    private void Start()
    {
        carico = true;
    }

    public void Spara(Vector3 direzione, Vector3 puntoDiPartenza)
    {
        if (carico)
        {
            proiettile.GetComponent<Proiettile>().direzione = direzione;
            oggettoInstanziato = Instantiate(proiettile, puntoDiPartenza, Quaternion.identity);
            gameObject.GetComponent<Rigidbody2D>().AddForce(-rinculo * direzione, ForceMode2D.Impulse);
            carico = false;
            StartCoroutine(Ricarica());
        }
    }

    private IEnumerator Ricarica()
    {
        yield return new WaitForSeconds(tempoRicarica);
        carico = true;
    }

}
