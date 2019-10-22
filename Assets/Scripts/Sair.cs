using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sair : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
	
	// retornar a cena início 
    public void Voltar()
    {
        SceneManager.LoadScene("Inicio");
    }
	
	// sair da conta e retornar à tela login
    public void Sim()
    {
        Cadastro.ID = null;
        Login.ID = null;

        SceneManager.LoadScene("Login");
    }
}
