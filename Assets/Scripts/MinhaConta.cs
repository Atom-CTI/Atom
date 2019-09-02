using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Text;
using System.Security.Cryptography;
using System.Globalization;
using System;

public class MinhaConta : MonoBehaviour
{
    Text txtNome;
    Text txtUsuario;
    Text txtSenha;
    Text txtEmail;
    Text txtDataNasc;
    Text txtEscolaridade;


    // Start is called before the first frame update
    void Start()
    {
        txtNome = GameObject.Find("txtNome").GetComponent<Text>();
        txtUsuario = GameObject.Find("txtUsuario").GetComponent<Text>();
        txtSenha = GameObject.Find("txtSenha").GetComponent<Text>();
        txtEmail = GameObject.Find("txtEmail").GetComponent<Text>();
        txtDataNasc = GameObject.Find("txtDataNasc").GetComponent<Text>();
        txtEscolaridade = GameObject.Find("txtEscolaridade").GetComponent<Text>();

        if(Login.ID != "1")
        {
            StartCoroutine(CompletaCampos(Login.ID));
        }
        else
        {
            txtUsuario.text = "Convidado";
            txtSenha.text = "*****";
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Voltar()
    {
        SceneManager.LoadScene("Inicio");
    }

    public void Alterar()
    {
        SceneManager.LoadScene("Alterar");
    }

    public void Sair()
    {
        Cadastro.ID = null;
        Login.ID = null;

        SceneManager.LoadScene("Login");
    }

    public IEnumerator CompletaCampos(string loginID)
    {
        string param_url = "http://200.145.153.172/atom/minhaConta.php?" + "id=" + loginID;

        using (UnityWebRequest url = UnityWebRequest.Get(param_url))
        {
            yield return url.Send();

            string[] szSplited = url.downloadHandler.text.Split(',');

            txtNome.text = szSplited[0];
            txtUsuario.text = szSplited[1];
            txtSenha.text = "*******";
            txtEmail.text = szSplited[3];

            string data = szSplited[4];

            txtDataNasc.text = ConverteData(data);
            txtEscolaridade.text = szSplited[5];
        }
    }

    public static string ConverteData(string data)
    {
        string dia = data[8] + "" + data[9];
        string mes = data[5] + "" + data[6];
        string ano = data[0] + "" + data[1] + "" + data[2] + "" + data[3];

        string dataconvertida = dia + "/" + mes + "/" + ano;

        return dataconvertida;
    }
}
