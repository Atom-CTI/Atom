using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controler : MonoBehaviour
{
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        
    }
	
	// chama animação para abrir o menu
    public void anima()
    {
        anim.SetInteger("Move", 1);
    }
	
	// chama animação para fechar o menu
    public void animas()
    {
        anim.SetInteger("Move2", 1);
    }
}
