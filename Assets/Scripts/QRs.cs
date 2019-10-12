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

    public void Cadastrar(string btn, Sprite r)
    {
        qrName = DefaultTrackableEventHandler.namee2;
        GameMannager.contadorQR++;
        //BTN = ELEMENTO QUIMICO
        // QRNAME = NOME DO QR QUE RECEBERÁ O ELEMENTO

        //CARD
        string cardNome = "PL" + qrName;
        card = GameObject.Find(cardNome);
        //nomesocial = new List<string>();
        //nomesocial.Add(atomo.nome);

        /*
        List<string> lista = new List<string>
        {
            atomo.nome
        };
        posicaolista = GameMannager.nomesocial.Count;
        GameMannager.nomesocial.Add(lista);
        */

        //card.GetComponent<DefaultTrackableEventHandler>().qrlivre = false;

        card.GetComponent<Renderer>().enabled = true;
        card.GetComponent<SpriteRenderer>().sprite = r;
        //CARD

        string ITnome = "IT" + qrName;

        qrit = GameObject.Find(ITnome);
        qrit.GetComponent<DefaultTrackableEventHandler>().qrlivre = false;

        string nomeAtomo = "atomo_" + atomo[0].valencia + "e";
        GameObject objPrefab = Resources.Load(nomeAtomo) as GameObject;
        GameObject gm = Instantiate(objPrefab) as GameObject;
        gm.name = "GM" + atomo[0].nome + GameMannager.contadorQR;

        //gm.gameObject.tag = "ARObject";

        gm.transform.parent = GameObject.Find(ITnome).transform;
        //posicao
        gm.transform.localPosition = new Vector3(0, 1, 0);
        // tamanho
        gm.transform.localScale = new Vector3((float)atomo[0].tamanho, (float)atomo[0].tamanho, (float)atomo[0].tamanho);
        //cor
        string cor = atomo[0].cor;
        //Debug.Log((Color)typeof(Color).GetProperty(cor.ToLowerInvariant()).GetValue(null, null));
        gm.GetComponent<Renderer>().material.color = (Color)typeof(Color).GetProperty(cor.ToLowerInvariant()).GetValue(null, null);
        //letra
        GameObject letra = Resources.Load(atomo[0].nome) as GameObject;
        GameObject GMletra = Instantiate(letra) as GameObject;
        GMletra.transform.parent = GameObject.Find(gm.name).transform;
        GMletra.name = "Letra" + atomo[0].nome + GameMannager.contadorQR;
        GMletra.transform.localPosition = new Vector3(-(((float)0.5 - (float)atomo[0].tamanho) * (float)0.25 + (float)0.10), 0, (float)-(GameObject.Find(gm.name).GetComponent<SphereCollider>().radius));
        GMletra.transform.localRotation = new Quaternion((float)0, (float)0, (float)0, 0);
        GMletra.transform.Rotate(0, 180, 0);
        //GMletra.transform.localScale = new Vector3((float)atomo.tamanho*(float)0.03, (float)atomo.tamanho * (float)0.03, (float)atomo.tamanho * (float)0.03);
        GMletra.transform.localScale = new Vector3((float)0.03, (float)0.03, (float)0.03);
    }

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
