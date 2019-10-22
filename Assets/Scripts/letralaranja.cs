using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class letralaranja : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		// atualiza a posição da letra do card laranja genérico,
		// centralizando-a no card
        this.transform.localPosition = new Vector3(0, 0, 0);
    }
}
