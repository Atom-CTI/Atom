using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;


public class PDF : MonoBehaviour
{
    string verif;
    string email;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Voltar()
    {
        SceneManager.LoadScene("Inicio");
    }

    public void Download()
    {
        Debug.Log("thiago1");
        StartCoroutine(BtnDownload());
    }

    public IEnumerator BtnDownload()
    {
        Debug.Log("thiago");

        string param_url = "http://200.145.153.172/atom/enviarEmail.php?";

        Debug.Log(param_url);

        using (UnityWebRequest url = UnityWebRequest.Get(param_url))
        {
            yield return url.Send();
            string[] szSplited = url.downloadHandler.text.Split(',');

            verif = szSplited[0];
            email = szSplited[1];

            if (verif == "0")
            {
                Debug.Log(email);
            }
            else
            {
                Debug.Log(email);
                SceneManager.LoadScene("Inicio");
            }
        }
    }
}
