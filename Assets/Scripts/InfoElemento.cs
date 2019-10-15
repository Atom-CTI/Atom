using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoElemento : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void OnMouseDown()
	{
        Debug.Log("click");
		Transform pai = gameObject.transform.parent;
		
		QRs thisQr = null;
		string nomeproduto = null;
		for(int i = 0; i < GameMannager.qrs.Count; i++)
		{
			if(pai.name.Contains(GameMannager.qrs[i].qrName))
			{
				thisQr = GameMannager.qrs[i];
				for(int j = 0; j < thisQr.atomo.Count; j++)
				{
					nomeproduto += thisQr.atomo[j].nome;
				}
				break;
			}
		}
		
		if(GameObject.Find("infoCard") == null)
		{
            Sprite spriteCard = Resources.Load<Sprite>("IN" + nomeproduto);
            if (spriteCard == null)
            {
                return;
            }
            GameObject infoCard = new GameObject("infoCard");
			infoCard.AddComponent<SpriteRenderer>();
            infoCard.GetComponent<SpriteRenderer>().sprite = spriteCard;
			infoCard.transform.position = new Vector3(330, 655, -1999);
			infoCard.transform.localScale = new Vector3((float)0.05, (float)0.05, (float)0.05);
			infoCard.AddComponent<BoxCollider>();
			
			infoCard.AddComponent<InfoCard>();
		}
	}

    
}
