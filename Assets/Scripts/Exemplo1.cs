using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Exemplo1 : MonoBehaviour
{
    GameObject imgAproxQR;
    GameObject imgTabela;
    GameObject imgElemento;
    GameObject imgMolecula;
    GameObject imgQROculto;
    GameObject imgInfos;

    // Start is called before the first frame update
    void Start()
    {
        imgAproxQR = GameObject.Find("imgAproxQR");
        imgTabela = GameObject.Find("imgTabela");
        imgElemento = GameObject.Find("imgElemento");
        imgMolecula = GameObject.Find("imgMolecula");
        imgQROculto = GameObject.Find("imgQROculto");

        imgAproxQR.transform.position = new Vector3(10000, 0, 0);
        imgTabela.transform.position = new Vector3(10000, 0, 0);
        imgElemento.transform.position = new Vector3(10000, 0, 0);
        imgMolecula.transform.position = new Vector3(10000, 0, 0);
        imgQROculto.transform.position = new Vector3(10000, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AproxQR()
    {
        imgAproxQR.transform.position = new Vector3(720, 1400, 0);
    }

    public void Tabela()
    {
        imgTabela.transform.position = new Vector3(720, 1400, 0);
    }

    public void Elemento()
    {
        imgElemento.transform.position = new Vector3(720, 1400, 0);
    }

    public void Molecula()
    {
        imgMolecula.transform.position = new Vector3(720, 1400, 0);
    }

    public void QROculto()
    {
        imgQROculto.transform.position = new Vector3(720, 1400, 0);
    }

    public void voltar()
    {
        imgAproxQR.transform.position = new Vector3(10000,0,0);
        imgTabela.transform.position = new Vector3(10000, 0, 0);
        imgElemento.transform.position = new Vector3(10000, 0, 0);
        imgMolecula.transform.position = new Vector3(10000, 0, 0);
        imgQROculto.transform.position = new Vector3(10000, 0, 0);
    }

    public void PDF()
    {
        SceneManager.LoadScene("PDF");
    }

    public void Consulta()
    {
        SceneManager.LoadScene("Consulta");
    }
    public void MinhaConta()
    {
        SceneManager.LoadScene("MinhaConta");
    }
}
