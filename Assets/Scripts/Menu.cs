using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject painel;
    private Controler cont;

    // Start is called before the first frame update
    void Start()
    {
        painel = GameObject.Find("Panel");
        cont = painel.GetComponent<Controler>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Abrir()
    {
        cont.anima();
    }

    public void Fechar()
    {
        cont.animas();
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
    public void Sobre()
    {
        SceneManager.LoadScene("Sobre");
    }
    public void Ajuda()
    {
        SceneManager.LoadScene("Ajuda");
    }
    public void Sair()
    {
        SceneManager.LoadScene("Sair");
    }
}
