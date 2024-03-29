﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Text;
using System.Security.Cryptography;
using System.Globalization;
using System;

public class Esqueci : MonoBehaviour
{
	// campos dos dados
    InputField txtUsuario;
    InputField txtSenha;
    InputField txtConfirmaSenha;
    Button btnConfirmaUsuario;
    Button btnAltera;

    Text txtErro;
	
	// variáveis para as
	// informações do usuário
    string usuario = null;
    string senha = null;
    string senha2 = null;

    // Start is called before the first frame update
    void Start()
    {
		// pega todos os campos de texto dos objetos
		// usados para a entrada de dados
        txtUsuario = GameObject.Find("txtUsuario").GetComponent<InputField>();
        txtSenha = GameObject.Find("txtSenha").GetComponent<InputField>();
        txtConfirmaSenha = GameObject.Find("txtConfirmaSenha").GetComponent<InputField>();
        btnConfirmaUsuario = GameObject.Find("btnConfirmaUsuario").GetComponent<Button>();
        btnAltera = GameObject.Find("btnAltera").GetComponent<Button>();

        txtErro = GameObject.Find("txtErro").GetComponent<Text>();

        btnAltera.interactable = false;
        txtSenha.interactable = false;
        txtConfirmaSenha.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
		
    }
	
	// retorna à cena de login
    public void Voltar()
    {
        SceneManager.LoadScene("Login");
    }
	
	// função para chamar a consistência de usuário
    public void ConfirmaUsuario()
    {
        usuario = txtUsuario.text;

        StartCoroutine(ConfereUsuario(usuario));
    }
	
	// função que checa se os campos estão preenchidos
	// antes de chamar a função para alteração de senha
    public void Alterar()
    {
        senha = Cadastro.ConverteSenha(txtSenha.text);
        senha2 = Cadastro.ConverteSenha(txtConfirmaSenha.text);

        if(senha == "" || senha2 == "")
        {
            txtErro.text = "Todos os campos devem ser preenchidos";
        }
        else if(senha != senha2)
        {
            txtErro.text = "As senhas não são correspondentes";
        }
        else
        {
            StartCoroutine(AlteraSenha(usuario, senha));
        }
    }
	
	// função para confirmar se o usuário existe
    public IEnumerator ConfereUsuario(string usuario)
    {
		// url do script php de conferir usuário
        string param_url = "http://200.145.153.172/atom/confereUsuario.php?" + "usuario=" + usuario;

        using (UnityWebRequest url = UnityWebRequest.Get(param_url))
        {
            yield return url.Send();

            string conf = url.downloadHandler.text;
			
			// checa se o usuário existe. Se sim,
			// permite acessar a senha
            if(conf == "1")
            {
                btnAltera.interactable = true;
                txtSenha.interactable = true;
                txtConfirmaSenha.interactable = true;
                btnConfirmaUsuario.interactable = false;
                txtUsuario.interactable = false;
                txtErro.text = "";
            }
            else
            {
                txtErro.text = "Usuario não existe";
            }
        }
    }
	
	// função que chama o script php para alteração de senha
    public IEnumerator AlteraSenha(string usuario, string senha)
    {
		// url do script php
        string param_url = "http://200.145.153.172/atom/alteraSenha.php?" + "usuario=" + usuario +
                                                                            "&senha=" + senha;
		
		// executa o script
        using (UnityWebRequest url = UnityWebRequest.Get(param_url))
        {
            yield return url.Send();

            Cadastro.ID = url.downloadHandler.text;

            SceneManager.LoadScene("Login");
        }
    }
}
