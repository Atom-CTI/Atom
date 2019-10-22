using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;


public class PDF : MonoBehaviour
{
    string verif;
    string email;

    readonly string ID = Login.ID;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
	
	// retorna à cena de inicio
    public void Voltar()
    {
        SceneManager.LoadScene("Inicio");
    }
	
	// função para fazer o download do PDF com os QRs
    public void Download()
    {
        StartCoroutine(BtnDownload());
    }
	
	// evento do botão de download dos QRs
    public IEnumerator BtnDownload()
    {
		// url do script php
        string param_url = "http://200.145.153.172/atom/enviarEmail.php?" + "id=" + ID;
		
        using (UnityWebRequest url = UnityWebRequest.Get(param_url))
        {
			// executa o script
            yield return url.Send();
            string[] szSplited = url.downloadHandler.text.Split(',');

            verif = szSplited[0];
            email = szSplited[1];
			
			// checa se o e-mail foi enviado corretamente
            if (verif == "0")
            {
				
            }
            else
            {
                SceneManager.LoadScene("Inicio");
            }
        }
    }
}
