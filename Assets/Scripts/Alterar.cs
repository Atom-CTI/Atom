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

public class Alterar : MonoBehaviour
{
    InputField txtNome;
    InputField txtUsuario;
    InputField txtSenha;
    InputField txtConfirmaSenha;
    InputField txtEmail;
    InputField txtDiaNasc;
    InputField txtMesNasc;
    InputField txtAnoNasc;
    Dropdown dpdEscolaridade;

    Button btnAlteraSenha;

    Text txtErro;

    string nome = null;
    string usuario = null;
    string senha = null;
    string senha2 = null;
    string email = null;
    string datanasc = null;
    string escolaridade = null;

    int dia = 0;
    int mes = 0;
    int ano = 0;

    readonly string ID = Login.ID;

    readonly List<string> escolaridades = new List<string> {"Escolaridade",
                                                            "Ensino fundamental",
                                                            "Ensino médio",
                                                            "Ensino superior" };

    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        txtNome = GameObject.Find("txtNome").GetComponent<InputField>();
        txtUsuario = GameObject.Find("txtUsuario").GetComponent<InputField>();
        txtSenha = GameObject.Find("txtSenha").GetComponent<InputField>();
        txtConfirmaSenha = GameObject.Find("txtConfirmaSenha").GetComponent<InputField>();
        txtEmail = GameObject.Find("txtEmail").GetComponent<InputField>();
        txtDiaNasc = GameObject.Find("txtDiaNasc").GetComponent<InputField>();
        txtMesNasc = GameObject.Find("txtMesNasc").GetComponent<InputField>();
        txtAnoNasc = GameObject.Find("txtAnoNasc").GetComponent<InputField>();
        dpdEscolaridade = GameObject.Find("dpdEscolaridade").GetComponent<Dropdown>();
        txtErro = GameObject.Find("txtErro").GetComponent<Text>();

        btnAlteraSenha = GameObject.Find("btnAlteraSenha").GetComponent<Button>();

        dpdEscolaridade.ClearOptions();
        dpdEscolaridade.AddOptions(escolaridades);

        txtSenha.interactable = false;

        StartCoroutine(CompletaCampos(ID));
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Voltar()
    {
        SceneManager.LoadScene("MinhaConta");
    }

    public void AlterarDados()
    {
        Debug.Log(senha);
        txtErro.text = "";

        if (txtDiaNasc.text == "") { dia = 0; }
        else { dia = int.Parse(txtDiaNasc.text); }

        if (txtMesNasc.text == "") { mes = 0; }
        else { mes = int.Parse(txtMesNasc.text); }

        if (txtAnoNasc.text == "") { ano = 0; }
        else { ano = int.Parse(txtAnoNasc.text); }

        nome = txtNome.text;
        usuario = txtUsuario.text;

        if (txtSenha.readOnly == false)
            senha = Cadastro.ConverteSenha(txtSenha.text);
        else
            StartCoroutine(RetornaSenha(ID));

        senha2 = Cadastro.ConverteSenha(txtConfirmaSenha.text);
        email = txtEmail.text;
        datanasc = ano.ToString() + "-" + mes.ToString() + "-" + dia.ToString();
        if (dpdEscolaridade.value == 0)
            escolaridade = "Escolaridade";
        if (dpdEscolaridade.value == 1)
            escolaridade = "Ensino Fundamental";
        if (dpdEscolaridade.value == 2)
            escolaridade = "Ensino Médio";
        if (dpdEscolaridade.value == 3)
            escolaridade = "Ensino Superior";

        StartCoroutine(Consistencia(ID, nome, usuario, senha, senha2, email, datanasc, escolaridade));
    }

    public void AlterarSenha()
    {
        txtSenha.interactable = true;
        txtSenha.text = "";
        btnAlteraSenha.interactable = false;
    }

    [System.Obsolete]
    public IEnumerator CompletaCampos(string id)
    {
        string param_url = "http://200.145.153.172/atom/minhaConta.php?" +  "id=" + id;
        using (UnityWebRequest url = UnityWebRequest.Get(param_url))
        {
            yield return url.Send();

            string[] szSplited = url.downloadHandler.text.Split(',');

            txtNome.text = szSplited[0];
            txtUsuario.text = szSplited[1];
            txtSenha.text = "*******";

            txtSenha.readOnly = true;

            senha = szSplited[2];

            txtEmail.text = szSplited[3];

            string data = szSplited[4];

            txtDiaNasc.text = MinhaConta.ConverteData(data)[0] + "" + MinhaConta.ConverteData(data)[1];
            txtMesNasc.text = MinhaConta.ConverteData(data)[3] + "" + MinhaConta.ConverteData(data)[4];
            txtAnoNasc.text = MinhaConta.ConverteData(data)[6] + "" + MinhaConta.ConverteData(data)[7] + "" + MinhaConta.ConverteData(data)[8] + "" + MinhaConta.ConverteData(data)[9];

            string escolaridade = szSplited[5];

            if(escolaridade == "Ensino Fundamental")
            {
                dpdEscolaridade.value = 1;
            }
            if (escolaridade == "Ensino Médio")
            {
                dpdEscolaridade.value = 2;
            }
            if (escolaridade == "Ensino Superior")
            {
                dpdEscolaridade.value = 3;
            }
        }
    }

    public IEnumerator Consistencia(string id, string nome, string usuario, string senha, string senha2, string email, string datanasc, string escolaridade)
    {
        string param_url = "http://200.145.153.172/atom/consistencia.php?" + "usuario=" + usuario;

        using (UnityWebRequest url = UnityWebRequest.Get(param_url))
        {
            yield return url.Send();

            if (Cadastro.TestaData(dia, mes, ano))
            {
                txtErro.text = "Data de nascimento invalida";
            }
            else if (Cadastro.TestaCampos(nome, usuario, senha, email, datanasc, escolaridade))
            {
                txtErro.text = "Todos os campos devem ser preenchidos";
            }
            else if (Cadastro.TestaEmail(email))
            {
                txtErro.text = "Formato de email incorreto";
            }
            else if (Cadastro.TestaSenhas(senha, senha2))
            {
                txtErro.text = "As senhas não são correspondentes";
            }
            else
            {
                StartCoroutine(AlterarDados(id, nome, usuario, senha, email, datanasc, escolaridade));
            }

        }
    }


    public IEnumerator AlterarDados(string id, string nome, string usuario, string senha, string email, string datanasc, string escolaridade)
    {
        string param_url = "http://200.145.153.172/atom/altera.php?" + "id=" + id +
                                                                       "&nome=" + nome +
                                                                       "&usuario=" + usuario +
                                                                       "&senha=" + senha +
                                                                       "&email=" + email +
                                                                       "&datanasc=" + datanasc +
                                                                       "&escolaridade=" + escolaridade;

        using (UnityWebRequest url = UnityWebRequest.Get(param_url))
        {
            yield return url.Send();

            Cadastro.ID = ID;
            Login.ID = null;

            SceneManager.LoadScene("Login");
        }
    }

    public IEnumerator RetornaSenha(string id)
    {
        string param_url = "http://200.145.153.172/atom/retornaSenha.php?" + "id=" + id;
            
        using (UnityWebRequest url = UnityWebRequest.Get(param_url))
        {
            yield return url.Send();

            senha = url.downloadHandler.text;
        }
    }

    bool TestaCampos(string nome, string usuario, string senha, string email, string datanasc, string escolaridade)
    {
        if (nome == "")
        {
            return true;
        }
        if (usuario == "")
        {
            return true;
        }
        if(txtSenha.readOnly == false)
        {
            if (senha == "")
            {
                return true;
            }
        }
        if (email == "")
        {
            return true;
        }
        if (datanasc == "0-0-0")
        {
            return true;
        }
        if (escolaridade == "Escolaridade")
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
