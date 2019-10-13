﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject painel;
    private Controler cont;

    public Button btnMinhaConta;
    public Image imgMinhaConta;
    public Image tampaMinhaConta;
    public Sprite user;
    public Sprite xuser;

    // Start is called before the first frame update
    void Start()
    {
        painel = GameObject.Find("Panel");
        cont = painel.GetComponent<Controler>();

        btnMinhaConta = GameObject.Find("btnMinhaConta").GetComponent<Button>();
        imgMinhaConta = GameObject.Find("imgMinhaConta").GetComponent<Image>();
        tampaMinhaConta = GameObject.Find("tampaMinhaConta").GetComponent<Image>();

        if(Login.ID == "1" || Login.ID == null)
        {
            btnMinhaConta.interactable = false;
            tampaMinhaConta.transform.localPosition = new Vector2(45, 15);
        }
        else
        {
            btnMinhaConta.interactable = true;
            tampaMinhaConta.transform.localPosition = new Vector2(10000, 0);
        }

        
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
