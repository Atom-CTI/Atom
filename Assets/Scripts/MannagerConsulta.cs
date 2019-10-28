using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MannagerConsulta : MonoBehaviour
{

    public GameObject tabela;
    public GameObject btnLixo;
	
	// sprites dos átomos
    public Sprite H;
    public Sprite Li;
    public Sprite Be;
    public Sprite Mg;
    public Sprite Na;
    public Sprite K;
    public Sprite Ca;
    public Sprite Sc;
    public Sprite Ti;
    public Sprite V;
    public Sprite Cr;
    public Sprite Mn;
    public Sprite Fe;
    public Sprite Co;
    public Sprite Ni;
    public Sprite Cu;
    public Sprite Zn;
    public Sprite B;
    public Sprite Al;
    public Sprite Ga;
    public Sprite C;
    public Sprite Si;
    public Sprite Ge;
    public Sprite N;
    public Sprite P;
    public Sprite As;
    public Sprite O;
    public Sprite S;
    public Sprite Se;
    public Sprite F;
    public Sprite Cl;
    public Sprite Br;
    public Sprite He;
    public Sprite Ne;
    public Sprite Ar;
    public Sprite Kr;
    public Sprite Rb;
    public Sprite Sr;
    public Sprite Y;
    public Sprite Zr;
    public Sprite Nb;
    public Sprite Mo;
    public Sprite Tc;
    public Sprite Ru;
    public Sprite Rh;
    public Sprite Pd;
    public Sprite Ag;
    public Sprite Cd;
    public Sprite In;
    public Sprite Sn;
    public Sprite Sb;
    public Sprite Te;
    public Sprite I;
    public Sprite Xe;
    public Sprite Cs;
    public Sprite Ba;
    public Sprite Hf;
    public Sprite Ta;
    public Sprite W;
    public Sprite Re;
    public Sprite Os;
    public Sprite Ir;
    public Sprite Pt;
    public Sprite Au;
    public Sprite Hg;
    public Sprite Tl;
    public Sprite Pb;
    public Sprite Bi;
    public Sprite Po;
    public Sprite At;
    public Sprite Rn;
    public Sprite Fr;
    public Sprite Ra;
    public Sprite Rf;
    public Sprite Db;
    public Sprite Sg;
    public Sprite Bh;
    public Sprite Hs;
    public Sprite Mt;
    public Sprite Ds;
    public Sprite Rg;
    public Sprite Cn;
    public Sprite Nh;
    public Sprite Fl;
    public Sprite Mc;
    public Sprite Lv;
    public Sprite Ts;
    public Sprite Og;

    public bool molecula = false;

    public static List<Atomos> atomos;
	
	// inicia a lista de atomos, e posiciona
	// a tabela periódica na tela
    void Start()
    {
        atomos = new List<Atomos>();
        tabela = GameObject.Find("ScrollTabela");
        btnLixo = GameObject.Find("btnLixeira");
        tabela.transform.localPosition = new Vector2(45, 0);
        btnLixo.transform.localPosition = new Vector2(0, 10000);
    }

    private void Update()
    {
        if(molecula)
        {

        }
    }

    string btn1 = "";
    string nomeAtomo = "";
	
	// função que seleciona um elemento da tabela periódica
	// para ser representado na tela
    public void AtribuiBotao(string btn)
    {
		// pega o nome do elemento selecionado
        btn1 = btn;
        int indice = BD.Banco(btn, atomos);
        Atomos atomo = atomos[indice];
		
		// pega o sprite do átomo
        System.Reflection.FieldInfo campo = this.GetType().GetField(btn);
        Sprite result = (Sprite)campo.GetValue(this);
		
		// atribui o sprite ao plano
        GameObject card = GameObject.Find("Plano");
        card.GetComponent<Renderer>().enabled = true;
        card.GetComponent<SpriteRenderer>().sprite = result;

		// gera o modelo (GM*) do átomo
        nomeAtomo = "atomo_" + atomo.valencia + "e";
        GameObject objPrefab = Resources.Load(nomeAtomo) as GameObject;
        GameObject gm = Instantiate(objPrefab) as GameObject;
        gm.name = "GMConsulta";
        gm.transform.position = new Vector3(315, 803, -450);
        gm.transform.localScale = new Vector3(500*(float)atomo.tamanho, 500*(float)atomo.tamanho, 500* (float)atomo.tamanho);
        string cor = atomo.cor;
        gm.GetComponent<Renderer>().material.color = (Color)typeof(Color).GetProperty(cor.ToLowerInvariant()).GetValue(null, null);

        // carrega modelo da letra do átomo
        molecula = true;
        GameObject letra = Resources.Load(atomo.nome) as GameObject;
        GameObject GMletra = Instantiate(letra) as GameObject;
		
        GMletra.transform.parent = GameObject.Find(gm.name).transform;
        GMletra.name = "Letra" + atomo.nome;
        GMletra.transform.localPosition = new Vector3(-(((float)0.5 - (float)atomo.tamanho) * (float)0.25 + (float)0.10), 0, (float)-(GameObject.Find(gm.name).GetComponent<SphereCollider>().radius));
        GMletra.transform.localRotation = new Quaternion((float)0, (float)0, (float)0, 0);
        GMletra.transform.Rotate(0, 180, 0);
        GMletra.transform.localScale = new Vector3((float)0.03, (float)0.03, (float)0.03);


        // coloca na tela o botão invisível para mostrar
		// as informações do átomo
        GameObject bt = GameObject.Find("btnInfo");
        bt.transform.localPosition = new Vector3((float)0, (float)-1500, (float)0);

        tabela.transform.localPosition = new Vector2(0, 10000);
        btnLixo.transform.localPosition = new Vector2((float)1.2, 550);
    }
	
	// função para mostrar as informações do átomo
    public void clicou()
    {
        // pega o nome elemento
        string nomeproduto = btn1;
		
		// cria o objeto do card de informações
		// do atomo selecionado
        if (GameObject.Find("infoCard") == null)
        {
            Sprite spriteCard = Resources.Load<Sprite>("IN" + nomeproduto);
            if (spriteCard == null)
            {
                return;
            }
            GameObject infoCard = new GameObject("infoCard");
            infoCard.AddComponent<SpriteRenderer>();
            infoCard.GetComponent<SpriteRenderer>().sprite = spriteCard;
            infoCard.transform.position = new Vector3(330, 655, -1998);
            infoCard.transform.localScale = new Vector3((float)0.05, (float)0.05, (float)0.05);
            infoCard.AddComponent<BoxCollider>();

            infoCard.AddComponent<InfoCard>();
        }
    }
	
	// função para apagar o modelo e card do átomo
    public void Lixeira()
    {
		// apaga o sprite do plano
        GameObject card = GameObject.Find("Plano");
        card.GetComponent<Renderer>().enabled = false;
        card.GetComponent<SpriteRenderer>().sprite = null;
		
		// remove o botão invisível
        GameObject bt = GameObject.Find("btnInfo");
        bt.transform.localPosition = new Vector3((float)1000, (float)-430, (float)0);
		
		// apaga o modelo do átomo
        Destroy(GameObject.Find("GMConsulta"));
        molecula = false;
        tabela.transform.localPosition = new Vector2(45, 0);
        btnLixo.transform.localPosition = new Vector2(0, 10000);
    }
	
	// retorna à cena inicial
    public void Volta()
    {
        SceneManager.LoadScene("Inicio");
    }

}
