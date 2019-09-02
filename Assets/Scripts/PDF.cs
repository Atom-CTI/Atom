using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PDF : MonoBehaviour
{
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

    public void apertar()
    {
        Debug.Log("thiago");
    }
}
