using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoCard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	// destrói o card quando o colisor do botão for clicado
	void OnMouseDown()
	{
		Destroy(gameObject);
	}
}
