using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QRs : MonoBehaviour
{
    public List<Atomos> atomo;
    public string qrName;
    public GameObject card;
    public GameObject qrit;
    public GameObject gm;

    public int posicaolista = -1;
	
	// cria um 
    public void Cadastrar(string btn, Sprite r)
    {
		// pega o qr identificado pelo vuforia
        qrName = DefaultTrackableEventHandler.namee2;
        GameMannager.contadorQR++;

        // cria o sprite do átomo que
		// fica acima do QR (PL*)
        string cardNome = "PL" + qrName;
        card = GameObject.Find(cardNome);
        card.GetComponent<Renderer>().enabled = true;
        card.GetComponent<SpriteRenderer>().sprite = r;
		
		// marca o QR atual como não livre,
		// para que não possa ser cadastrado novamente
        string ITnome = "IT" + qrName;
        qrit = GameObject.Find(ITnome);
        qrit.GetComponent<DefaultTrackableEventHandler>().qrlivre = false;
		
		// carrega o prefab do átomo com o numero correto de eletrons na
		// camada de valência, o instancia na cena como filho do QR,
		// e define sua cor
        string nomeAtomo = "atomo_" + atomo[0].valencia + "e";
        GameObject objPrefab = Resources.Load(nomeAtomo) as GameObject;
        GameObject gm = Instantiate(objPrefab) as GameObject;
        gm.name = "GM" + atomo[0].nome + GameMannager.contadorQR;
		
        gm.transform.parent = GameObject.Find(ITnome).transform;
        gm.transform.localPosition = new Vector3(0, 1, 0);
        gm.transform.localScale = new Vector3((float)atomo[0].tamanho, (float)atomo[0].tamanho, (float)atomo[0].tamanho);
		
        string cor = atomo[0].cor;
        gm.GetComponent<Renderer>().material.color = (Color)typeof(Color).GetProperty(cor.ToLowerInvariant()).GetValue(null, null);
		
        // carrega o modelo da letra referente ao átomo atual,
		// e o instancia como filho do átomo (GM*)
        GameObject letra = Resources.Load(atomo[0].nome) as GameObject;
        GameObject GMletra = Instantiate(letra) as GameObject;
        GMletra.transform.parent = GameObject.Find(gm.name).transform;
        GMletra.name = "Letra" + atomo[0].nome + GameMannager.contadorQR;
        GMletra.transform.localPosition = new Vector3(-(((float)0.5 - (float)atomo[0].tamanho) * (float)0.25 + (float)0.10), 0, (float)-(GameObject.Find(gm.name).GetComponent<SphereCollider>().radius));
        GMletra.transform.localRotation = new Quaternion((float)0, (float)0, (float)0, 0);
        GMletra.transform.Rotate(0, 180, 0);
        GMletra.transform.localScale = new Vector3((float)0.03, (float)0.03, (float)0.03);
    }
	
	// construtor da classe, inicializa a lista de atomos
    public QRs(Atomos atm)
    {
        atomo = new List<Atomos>();
        atomo.Add(atm);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
