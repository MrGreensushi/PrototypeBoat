using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CaricaNuovoLivello : MonoBehaviour
{
    public string livelloDaCaricare;
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(livelloDaCaricare);
    }
}
