using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sobre : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
	
	// retorna à cena inicial
    public void Voltar()
    {
        SceneManager.LoadScene("Inicio");
    }
}
