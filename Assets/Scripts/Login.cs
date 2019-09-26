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
    // Start is called before the first frame update

    InputField txtUsuario;
    InputField txtSenha;
    Text txtErro;

    public static string ID = null;
    string usuario;
    string senha;

    void Start()
    {
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

    public void Cadastrar()
    {
        SceneManager.LoadScene("Cadastrar");
    }

    public void Esqueci()
    {
        SceneManager.LoadScene("Esqueci");
    }

    public void Convidado()
    {
        ID = "1";

        SceneManager.LoadScene("Inicio");
    }

    public IEnumerator CompletaCampos(string cadastroID)
    {
        string param_url = "http://200.145.153.172/atom/completaCampos.php?" + "id=" + cadastroID;

        using (UnityWebRequest url = UnityWebRequest.Get(param_url))
        {
            yield return url.Send();

            string[] szSplited = url.downloadHandler.text.Split(',');
            usuario = szSplited[0];
            senha = szSplited[1];

            txtUsuario.text = usuario;
            //txtSenha.text = senha;
        }
    }

    public IEnumerator Logar(string usuario, string senha)
    {
        string param_url = "http://200.145.153.172/atom/login.php?" + "usuario=" + usuario +
                                                                "&senha=" + senha;

        using (UnityWebRequest url = UnityWebRequest.Get(param_url))
        {
            yield return url.Send();

            string[] szSplited = url.downloadHandler.text.Split(',');

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
