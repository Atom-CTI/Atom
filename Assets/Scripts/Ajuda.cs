using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ajuda : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
	
    void Update()
    {
        
    }
	
	// retorna à cena inicial
    public void Voltar()
    {
        SceneManager.LoadScene("Inicio");
    }
}
