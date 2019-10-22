using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Text;
using System.Security.Cryptography;
using System.Globalization;

public class Cadastro : MonoBehaviour
{
	// campos dos dados
    InputField txtNome;
    InputField txtUsuario;
    InputField txtSenha;
    InputField txtConfirmaSenha;
    InputField txtEmail;
    InputField txtDiaNasc;
    InputField txtMesNasc;
    InputField txtAnoNasc;
    Dropdown dpdEscolaridade;
    Text txtErro;
	
	// variáveis para as
	// informações do usuário
    public static string ID = null;
    string nome = null;
    string usuario = null;
    string senha = null;
    string senha2 = null;
    string email = null;
    string datanasc = null;
    string escolaridade = null;
	
	// data de nascimento
    int dia = 0;
    int mes = 0;
    int ano = 0;
	
	// conteúdo da caixa de escolaridade
    readonly List<string> escolaridades = new List<string> {"Escolaridade",
                                                  "Ensino fundamental",
                                                  "Ensino médio",
                                                  "Ensino superior" };
	
    void Start()
    {
		// pega todos os campos de texto dos objetos
		// usados para a entrada de dados
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
		
		// organiza a combo box da escolaridade
        dpdEscolaridade.ClearOptions();
        dpdEscolaridade.AddOptions(escolaridades);
    }

    void Update()
    {
    }
	
	// função para cadastrar um usuário com os dados inseridos
    public void Cadastrar()
    {
        txtErro.text = "";
		
		// data de nascimento
        if(txtDiaNasc.text == "") { dia = 0; }
        else { dia = int.Parse(txtDiaNasc.text); }

        if(txtMesNasc.text == "") { mes = 0; }
        else { mes = int.Parse(txtMesNasc.text); }

        if(txtAnoNasc.text == "") { ano = 0; }
        else { ano = int.Parse(txtAnoNasc.text); }
		
		// dados de texto
        nome = txtNome.text;
        usuario = txtUsuario.text;
        senha = ConverteSenha(txtSenha.text);
        senha2 = ConverteSenha(txtConfirmaSenha.text);
        email = txtEmail.text;
        datanasc = ano.ToString() + "-" + mes.ToString() + "-" + dia.ToString();
		
		// combo box da escolaridade
        if (dpdEscolaridade.value == 0)
            escolaridade = "Escolaridade";
        if (dpdEscolaridade.value == 1)
            escolaridade = "Ensino Fundamental";
        if (dpdEscolaridade.value == 2)
            escolaridade = "Ensino Médio";
        if (dpdEscolaridade.value == 3)
            escolaridade = "Ensino Superior";
		
		// função para consistência de dados
        StartCoroutine(Consistencia(nome, usuario, senha, senha2, email, datanasc, escolaridade));
    }
	
	// retorna à cena de login
    public void Voltar()
    {
        SceneManager.LoadScene("Login");
    }

	// função que chama um script php externo para
	// inserir os dados no banco
    [System.Obsolete]
    public static IEnumerator Cadastra(string nome, string usuario, string senha, string email, string datanasc, string escolaridade)
    {
		// url do script com os parâmetros do usuário
        string param_url = "http://200.145.153.172/atom/cadastro.php?" + "nome=" + nome + 
                                                                   "&usuario=" + usuario + 
                                                                   "&senha=" + senha + 
                                                                   "&email=" + email + 
                                                                   "&datanasc=" + datanasc + 
                                                                   "&escolaridade=" + escolaridade;
		
		// executa o script php
        using (UnityWebRequest url = UnityWebRequest.Get(param_url))
        {
            yield return url.Send();

            ID = url.downloadHandler.text;
        }
		
		// retorna à cena de login
        SceneManager.LoadScene("Login");
    }

	// função para a consistência dos dados do usuário
    [System.Obsolete]
    public IEnumerator Consistencia(string nome, string usuario, string senha, string senha2, string email, string datanasc, string escolaridade)
    {
		// script php para checar se o usuário já existe
        string param_url = "http://200.145.153.172/atom/consistencia.php?" + "usuario=" + usuario;
		
		// executa o script
        using (UnityWebRequest url = UnityWebRequest.Get(param_url))
        {
            yield return url.Send();
			
            string consist = url.downloadHandler.text;
            if (consist == "0")
            {
                txtErro.text = "Usuario já existente";
            }
			// outras consistências
            else if (TestaData(dia, mes, ano))
            {
                txtErro.text = "Data de nascimento invalida";
            }
            else if (TestaCampos(nome, usuario, senha, email, datanasc, escolaridade))
            {
                txtErro.text = "Todos os campos devem ser preenchidos";
            }
            else if (TestaEmail(email))
            {
                txtErro.text = "Formato de email incorreto";
            }
            else if (TestaSenhas(txtSenha.text, txtConfirmaSenha.text))
            {
                txtErro.text = "As senhas não são correspondentes";
            }
			// se não houver nenhum erro, cadastra
            else
            {
                StartCoroutine(Cadastra(nome, usuario, senha, email, datanasc, escolaridade));
            }
            
        }
    }
	
	// função para criptografar a senha
    public static string ConverteSenha(string value)
    {
        UnicodeEncoding encoding = new UnicodeEncoding();
        byte[] hashBytes;
        using (HashAlgorithm hash = SHA1.Create())
            hashBytes = hash.ComputeHash(encoding.GetBytes(value));

        StringBuilder hashValue = new StringBuilder(hashBytes.Length * 2);
        foreach (byte b in hashBytes)
        {
            hashValue.AppendFormat(CultureInfo.InvariantCulture, "{0:X2}", b);
        }

        return hashValue.ToString();
    }
	
	// função para checar a data de nascimento
    public static bool TestaData(int dia, int mes, int ano)
    {
        if (dia <= 0 || dia > 31)
        {
            return true;
        }
        if (mes <= 0 || mes > 12)
        {
            return true;
        }
        if (ano < 1950 || ano > 2018)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
	
	// função para checar se os campos foram preenchidos
    public static bool TestaCampos(string nome, string usuario, string senha, string email, string datanasc, string escolaridade)
    {
        if(nome == "")
        {
            return true;
        }
        if (usuario == "")
        {
            return true;
        }
        if (senha == "")
        {
            return true;
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
	
	// função para checar se o e-mail é válido
    public static bool TestaEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return false;
        }
        catch
        {
            return true;
        }
    }
	
	// função para testar se ambas as
	// senhas são iguais
    public static bool TestaSenhas(string senha, string senha2)
    {
        if(senha == senha2)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
