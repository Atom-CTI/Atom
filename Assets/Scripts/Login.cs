using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Text;
using System.Security.Cryptography;
using System.Globalization;

public class Login : MonoBehaviour
{
	// campos dos dados
    InputField txtUsuario;
    InputField txtSenha;
    Text txtErro;

    public static string ID = null;
    string usuario;
    string senha;

    void Start()
    {
		// pega todos os campos de texto dos objetos
		// usados para a entrada de dados
        txtUsuario = GameObject.Find("txtUsuario").GetComponent<InputField>();
        txtSenha = GameObject.Find("txtSenha").GetComponent<InputField>();
        txtErro = GameObject.Find("txtErro").GetComponent<Text>();
		
		
        if(Cadastro.ID != null)
        {
            StartCoroutine(CompletaCampos(Cadastro.ID));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	// função para checar se o usuário é
	// administrador antes de logar
    public void Logar()
    {
        txtErro.text = "";
		
        if(txtUsuario.text == "admin" && txtSenha.text == "admin")
        {
            ID = "0";

            SceneManager.LoadScene("Inicio");
        }
        else
        {
            StartCoroutine(Logar(txtUsuario.text, Cadastro.ConverteSenha(txtSenha.text)));
        }

    }
	
	// função para carregar a cena de cadastro
    public void Cadastrar()
    {
        SceneManager.LoadScene("Cadastrar");
    }

	// função para carregar a cena de recuperação de senha
    public void Esqueci()
    {
        SceneManager.LoadScene("Esqueci");
    }
	
	// função para entrar como convidado
    public void Convidado()
    {
        ID = "1";

        SceneManager.LoadScene("Inicio");
    }
	
	
    public IEnumerator CompletaCampos(string cadastroID)
    {
		// url do script php
        string param_url = "http://200.145.153.172/atom/completaCampos.php?" + "id=" + cadastroID;

        using (UnityWebRequest url = UnityWebRequest.Get(param_url))
        {
            yield return url.Send();

            string[] szSplited = url.downloadHandler.text.Split(',');
            usuario = szSplited[0];
            senha = szSplited[1];

            txtUsuario.text = usuario;
        }
    }

	// função para efetuar o login
    public IEnumerator Logar(string usuario, string senha)
    {
		// url do script php de login
        string param_url = "http://200.145.153.172/atom/login.php?" + "usuario=" + usuario +
                                                                "&senha=" + senha;
		
		// executa o script
        using (UnityWebRequest url = UnityWebRequest.Get(param_url))
        {
            yield return url.Send();

            string[] szSplited = url.downloadHandler.text.Split(',');
			
			// checa se o usuário e senha existem no banco
            if (szSplited[0] == "1")
            {
                ID = szSplited[1];
                Debug.Log(ID);

                SceneManager.LoadScene("Inicio");
            }
            else
            {
                txtErro.text = "Usuario ou senha não existe";
            }
        }
    }
}
