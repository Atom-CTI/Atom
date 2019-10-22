using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using Vuforia;

public class GameMannager : MonoBehaviour
{


    public GameObject aviso;


    public GameObject btnvolta;
    public GameObject btncadastra;
    public GameObject tabela;
    public GameObject btnTabela;
	public GameObject naoReage;

    public static bool modocadastro;

    public static List<Atomos> atomos;
    public static List<QRs> qrs;

    public static int contadorQR = 0;
	
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
	
	// sprites das moléculas
    public Sprite PLAGCL;
    public Sprite PLALALOOO;
    public Sprite PLALALSSS;
    public Sprite PLALCLCLCL;
    public Sprite PLATAT;
    public Sprite PLBACLCL;
    public Sprite PLBRBR;
    public Sprite PLBRBRMG;
    public Sprite PLBRH;
    public Sprite PLCACLCL;
    public Sprite PLCAFF;
    public Sprite PLCAO;
    public Sprite PLCHHHH;
    public Sprite PLCLCLMG;
    public Sprite PLCLCLRA;
    public Sprite PLCLCLSR;
    public Sprite PLCLH;
    public Sprite PLCLK;
    public Sprite PLCLLI;
    public Sprite PLCO;
    public Sprite PLCOO;
    public Sprite PLFECLCLCL;
    public Sprite PLFEFEOOO;
    public Sprite PLFEO;
    public Sprite PLFF;
    public Sprite PLFFFFS;
    public Sprite PLFH;
    public Sprite PLFSI;
    public Sprite PLHH;
    public Sprite PLHHHN;
    public Sprite PLII;
    public Sprite PLKKS;
    public Sprite PLMGO;
    public Sprite PLNOZ;
    public Sprite PLOOOS;
    public Sprite PLOOS;
    public Sprite PLOOSI;
    public Sprite PLOOSS;
    public Sprite PLSESE;
    public Sprite PLHHO;
    public Sprite PLOO;
    public Sprite PLCLNA;
    public Sprite PLNN;
	
	// sprite genérico
    public Sprite cardlaranja;

    public static string qrName;

    public static List<string> reacao;

    string novocard = "";
    string nomeproduto = "";

    int qtdQR;

    GameObject representacao;
    GameObject produto;

    public static bool podefazerreacao;
	
    void Start()
    {
		// inicialização de variáveis de controle e
		// posicionamento dos elementos da interface
        podefazerreacao = true;
        reacao = new List<string>();

        btnvolta = GameObject.Find("btnVoltar");
        btnvolta.transform.localPosition = new Vector2(0, 10000);

        btncadastra = GameObject.Find("btnCadastrar");
        btncadastra.transform.localPosition = new Vector2(-30, 550);

        aviso = GameObject.Find("aviso");
        aviso.transform.localPosition = new Vector2(0, 10000);

        tabela = GameObject.Find("ScrollTabela");
        tabela.transform.localPosition = new Vector2(0, 10000);

        modocadastro = false;

        qtdQR = GameObject.FindGameObjectsWithTag("QR").Length;

        atomos = new List<Atomos>();
        qrs = new List<QRs>();
		
		// cria o loop para cheque de update
        InvokeRepeating("colisaoupdate", 0, (float)1.0);
    }
	
	// lista com os nomes dos QRs que participam da reação
    public List<string> elementosreacao = new List<string>();
	
	// lista indicando o estado anterior da "elementosreacao"
	public List<string> elementosreacaoaux = new List<string>();
	
	// guarda os objetos (Atomos) usados na reação
    public List<Atomos> atomosdareacao;
	
	// função repetida todo segundo, que checa se dois ou mais QRs
	// entraram em colisão para ocorrer a reação
    private void colisaoupdate()
    {
        bool existenoelementosreacao = false;
        bool podeaddlista = false;
		
		// atravessa todos os QRs cadastrados
        for (int a = 0; a < qrs.Count; a++)
        {
            if (qrs[a].card == null)
            {
                continue;
            }
			// se o QR tem um átomo atrelado a ele
            else
            {
				// se o QR estiver fora de cena, remove da lista
                if (!qrs[a].card.GetComponent<SpriteRenderer>().enabled)
                {
                    elementosreacao.Remove("PL" + qrs[a].qrName);
                    continue;
                }
				
				// pega todos os colisores próximos ao QR de indice "a",
				// num raio definido pelo "Vector3"
                Collider[] hitColliders = Physics.OverlapBox(qrs[a].card.transform.position, new Vector3((float)90, (float)90, (float)90));
				
				// se o próprio QR for o único elemento da lista, remove-o
				// pare impedir uma reação desnecessária
                if (hitColliders.Length == 1)
                {
                    bool tanalista = false;
                    int b = 0;
                    for (; b < elementosreacao.Count; b++)
                    {
                        if (hitColliders[0].gameObject.name.Equals(elementosreacao[b]))
                        {
                            tanalista = true;
                            break;
                        }
                    }
                    if (tanalista)
                        elementosreacao.RemoveAt(b);
                }
				
				// atravessa o array com todos os colisores pegos
                for (int i = 0; i < hitColliders.Length; i++)
                {
					// se o colisor pertencer a um card (PL*) dentro de cena
                    if (hitColliders[i].gameObject.name.Contains("PL") && hitColliders[i].gameObject.activeInHierarchy && qrs[a].card.activeInHierarchy)
                    {
						// se for o primeiro elemento da lista,
						// simplesmente adicioná-lo
                        if (elementosreacao.Count == 0)
                        {
                            elementosreacao.Add("PL" + qrs[a].qrName);
                        }
						// se não for o único elemento, checa se esse QR
						// já está presente na lista antes de adicioná-lo
                        else
                        {
                            for (int n = 0; n < elementosreacao.Count; n++)
                            {
                                for (int j = 0; j < hitColliders.Length; j++)
                                {
                                    if (hitColliders[j].gameObject.name.Contains(elementosreacao[n]))
                                    {
                                        podeaddlista = true;
                                        break;
                                    }
                                }
                                if (podeaddlista)
                                {
                                    break;
                                }
                            }
                            if (!podeaddlista)
                            {
                                continue;
                            }

                            podeaddlista = false;
							
                            for (int x = 0; x < elementosreacao.Count; x++)
                            {
                                if (hitColliders[i].gameObject.name.Contains(elementosreacao[x]))
                                {
                                    existenoelementosreacao = true;
                                    break;
                                }
                            }
                            if (existenoelementosreacao)
                            {
                                existenoelementosreacao = false;
                                continue;
                            }
							
							// adiciona
                            elementosreacao.Add(hitColliders[i].gameObject.name);
                        }

                    }

                }

            }

        }
		
		// se não houve alteração na lista "elementosreacao",
		// ou se ela tiver apenas um elemento, retornar
        if (elementosreacao.Equals(elementosreacaoaux) || elementosreacao.Count == 1)
        {
            return;
        }
        else
        {
			// inicializa a lista dos átomos
            atomosdareacao = new List<Atomos>();
			
			// pega os objetos (cards) identificados pelos nomes
			// na lista "elementosreacao"
            for (int b = 0; b < elementosreacao.Count; b++)
            {
                for (int n = 0; n < qrs.Count; n++)
                {
                    if (qrs[n].card.name.Contains(elementosreacao[b]))
                    {
						// cada átomo guardado no objeto de um QR
						// que participa da reação é colocado em um novo objeto
						// e salvo na lista "atomosdareacao"
                        for (int j = 0; j < qrs[n].atomo.Count; j++)
                        {
                            Atomos novoAtomo = new Atomos();
                            novoAtomo.nome = qrs[n].atomo[j].nome;
                            novoAtomo.cor = qrs[n].atomo[j].cor;
                            novoAtomo.tamanho = qrs[n].atomo[j].tamanho;
                            novoAtomo.valencia = qrs[n].atomo[j].valencia;
                            novoAtomo.nox = qrs[n].atomo[j].nox;
                            novoAtomo.tipo = qrs[n].atomo[j].tipo;
                            novoAtomo.eletroNeg = qrs[n].atomo[j].eletroNeg;
                            novoAtomo.eletronsAtuais = qrs[n].atomo[j].valencia;
                            novoAtomo.eletronsDisponiveis = qrs[n].atomo[j].valencia;
                            atomosdareacao.Add(novoAtomo);
                        }


                    }
                }
            }
			
			// se não houverem ao menos dois átomos para reagir, retornar
            if (atomosdareacao.Count <= 1)
                return;

            bool fazreacao = true;
			
			// checa se existem no máximo dois tipos
			// de atomos diferentes para a reação
            string atomo1 = "", atomo2 = "";
            for (int a = 0; a < atomosdareacao.Count; a++)
            {
                if (a == 0)
                {
                    atomo1 = atomosdareacao[0].nome;
                }
                else if (!atomosdareacao[a].nome.Equals(atomo1))
                {
                    if (atomo2.Equals(""))
                    {
                        atomo2 = atomosdareacao[a].nome;
                    }
                    else if (!atomosdareacao[a].nome.Equals(atomo2))
                    {
                        fazreacao = false;
                    }

                }
            }
			
			// array equivalente à lista "atomosdareacao"
            Atomos[] atomosreaction;
			
			// se houverem apenas dois tipos de átomo,
			// executar a reação
            if (fazreacao)
            {
				// organiza a "atomosdareacao" em ordem alfabética
				// e a converte em array
                atomosdareacao.Sort((x, y) => string.Compare(x.nome, y.nome, System.StringComparison.Ordinal));
                atomosreaction = atomosdareacao.ToArray();
				
				// executa a reação
                bool reagiu = ReacaoQuimica.reacao(atomosreaction, atomosdareacao.Count);
				
				// se a reação for bem sucedida
                if (reagiu)
                {
                    int diferenca = 0;
                    string eleanterior = "";
                    nomeproduto = "";
					
					// guarda os membros do "atomosdareacao"
					// na string "nomeproduto" (em ordem alfabética)
                    for (int i = 0; i < atomosdareacao.Count; i++)
                    {
                        nomeproduto = nomeproduto + atomosdareacao[i].nome;
                    }
					
					
                    QRs final = qrs.Find(x => elementosreacao[0].Contains(x.qrName));
                    novocard = elementosreacao[0];
                    elementosreacao.RemoveAt(0);

                    List<int> indexremover = new List<int>();
					
                    // indica quais QRs serão limpos
                    for (int a = 0; a < qrs.Count; a++)
                    {
                        for (int b = 0; b < elementosreacao.Count; b++)
                        {
                            if (qrs[a].qrName != null && elementosreacao[b].Contains(qrs[a].qrName))
                            {
                                for (int m = 0; m < qrs[a].atomo.Count; m++)
                                {
                                    qrs.Find(x => novocard.Contains(x.qrName)).atomo.Add(qrs[a].atomo[m]);
                                }
                                indexremover.Add(a);
                            }
                        }
                    }
					
					// organiza os QRs a limpar
                    indexremover.Sort();
					
					// limpa os QRs
                    for (int i = indexremover.Count - 1; i >= 0; i--)
                    {
						// indica que o QR pode ser usado para uma nova reação
                        GameObject qrLivreReacao = GameObject.Find("IT" + qrs[indexremover[i]].qrName);
                        qrLivreReacao.GetComponent<DefaultTrackableEventHandler>().qrlivre = true;
						
						// corrige os filhos de cada QR
                        foreach (Transform child in GameObject.Find("IT" + qrs[indexremover[i]].qrName).transform)
                        {
							// apaga o modelo e o botão invisível
                            if (child.gameObject.name.Contains("GM") || child.gameObject.name.Equals("botao"))
                                Destroy(child.gameObject);
							// limpa o texto e o sprite
                            else if(child.gameObject.name.Contains("PL"))
                            {
                                GameObject.Find(child.gameObject.name).GetComponent<SpriteRenderer>().sprite = null;
                                GameObject.Find(child.gameObject.name).GetComponentInChildren<TextMeshPro>().text = "";
                            }
                                


                        }
						
						// remove da lista de QRs cadastrados
                        qrs.RemoveAt(indexremover[i]);

                    }
					
					// limpa a lista e adiciona o QR do produto a ela
                    elementosreacao.Clear();
                    elementosreacao.Add(novocard);

                    nomeproduto = nomeproduto.ToUpper();
					
					// carrega o modelo da molécula
                    if (produto = Resources.Load("GM" + nomeproduto) as GameObject)
                    {
						// apaga o modelo (GM*) e o botão do card
                        foreach (Transform child in GameObject.Find("IT" + novocard.Remove(0, 2)).transform)
                        {
                            if (child.gameObject.name.Contains("GM"))
                            {
                                Destroy(child.gameObject);
                            }
                            if (child.gameObject.name.Contains("botao"))
                            {
                                Destroy(child.gameObject);
                            }
							if(child.gameObject.name.Contains("RP"))
                            {
                                Destroy(child.gameObject);
                            }
                        }
						
						// instancia o modelo da molécula como filho
						// do QR e corrige sua posição
                        qrs.Find(x => novocard.Contains(x.qrName)).gm = produto;
                        GameObject gm = Instantiate(produto) as GameObject;
                        gm.name = "GM" + nomeproduto;
                        gm.transform.parent = GameObject.Find("IT" + novocard.Remove(0, 2)).transform;
                        gm.transform.localPosition = new Vector3(0, 1, 0);
                        gm.transform.localScale = new Vector3((float)1, (float)1, (float)1);
						
						// carrega o sprite com o nome do elemento
						// em cima do QR
                        System.Reflection.FieldInfo campo = this.GetType().GetField("PL" + nomeproduto);
                        Sprite result = (Sprite)campo.GetValue(this);
                        GameObject.Find(novocard).GetComponent<SpriteRenderer>().sprite = result;
                        GameObject.Find(novocard).transform.localScale = new Vector3((float)0.15, (float)0.15, (float)0.15);
                        qrs.Find(x => novocard.Contains(x.qrName)).card = GameObject.Find(novocard);
						
						// carrega a representação gráfica da molécula
						// e a mostra temporáriamente
                        if(GameObject.Find("RP"+nomeproduto) == null)
                        {
                            if (representacao = Resources.Load("RP" + nomeproduto) as GameObject)
                            {
                                StartCoroutine("Wait");
                            }
                        }
                        
                        // cria um botão invisível para registrar
						// o clique que abre o cartão com informações
						// da molécula
                        GameObject botao = new GameObject();
                        botao.name = "botao";
                        botao.transform.parent = gm.transform.parent;
                        botao.transform.rotation = new Quaternion(0, 0, 0, 0);
                        botao.transform.localRotation = new Quaternion(0, 0, 0, 0);
                        botao.transform.localPosition = new Vector3(0, (float)0.3, 0);
                        botao.transform.localScale = new Vector3(1, 1, 1);
                        botao.AddComponent<InfoElemento>();

                        BoxCollider botaoCollider = botao.AddComponent<BoxCollider>();
                        botaoCollider.size = new Vector3(1, (float)0.4, 1);
                        
                    }
					// se não houver uma representação gráfica da molécula
                    else
                    {
                        Wait3(3);
                        string ultimo = "";
						
                        if (atomosdareacao[0].eletroNeg > atomosdareacao[diferenca].eletroNeg)
                        {
                            int val = atomosdareacao.Count - diferenca;
                            ultimo = atomosdareacao[diferenca].nome + val + atomosdareacao[0].nome + diferenca;
                        }
                        else if (atomosdareacao[0].eletroNeg < atomosdareacao[diferenca].eletroNeg)
                        {
                            int val = atomosdareacao.Count - diferenca;
                            ultimo = atomosdareacao[0].nome + diferenca + atomosdareacao[diferenca].nome + val;
                        }
                        else if (diferenca == 0)
                        {
                            ultimo = atomosdareacao[0].nome + atomosdareacao.Count;
                        }

                        foreach (Transform child in GameObject.Find("IT" + novocard.Remove(0, 2)).transform)
                        {
                            if (child.gameObject.name.Contains("GM"))
                            {
                                Destroy(child.gameObject);
                                break;
                            }
                        }

                        
                        // cria card genérico
                        System.Reflection.FieldInfo camp = this.GetType().GetField("cardlaranja");
                        Sprite cardorange = (Sprite)camp.GetValue(this);
                        GameObject.Find(novocard).GetComponent<SpriteRenderer>().sprite = cardorange;
                        GameObject.Find(novocard).transform.localScale = new Vector3((float)0.15, (float)0.15, (float)0.15);
                        qrs.Find(x => novocard.Contains(x.qrName)).card = GameObject.Find(novocard);

                        // escreve o nome da molécula acima do card criado
                        foreach(Transform child in GameObject.Find(novocard).transform)
                        {
                            if(child.gameObject.name.Contains("Text"))
                            {
                                child.gameObject.GetComponent<TextMeshPro>().text = ultimo;

                                return;
                            }
                        }
                    }
                }
				else
				{
					// se não reagiu
					naoReage.transform.localPosition = new Vector2(0, 0);
				}
            }
        }
    }
	
	// função que espara dois segundos após a reação
	// e cria a representação alternativa da molécula
    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        Destroy(GameObject.Find("GM" + nomeproduto));
        GameObject gm = Instantiate(representacao) as GameObject;
        gm.name = "RP" + nomeproduto;
        gm.transform.parent = GameObject.Find("IT" + novocard.Remove(0, 2)).transform;
        gm.transform.localPosition = new Vector3(0, (float)1.5, (float)-0.5);
        gm.transform.localScale = new Vector3((float)0.04, (float)0.04, (float)0.04);

        StartCoroutine("Wait2");

    }
	
	// função que reverte a ação de "Wait"
    public IEnumerator Wait2()
    {

        yield return new WaitForSeconds(2);
        Destroy(GameObject.Find("RP" + nomeproduto));
        GameObject gm = Instantiate(produto) as GameObject;
        gm.name = "GM" + nomeproduto;
        gm.transform.parent = GameObject.Find("IT" + novocard.Remove(0, 2)).transform;
        gm.transform.localPosition = new Vector3(0, 1, 0);
        gm.transform.localScale = new Vector3((float)1, (float)1, (float)1);
    }
	
	// função para esperar um determinado número de segundos
    public IEnumerator Wait3(int tempo)
    {
        yield return new WaitForSeconds(tempo);
    }


    void Update()
    {
        
    }

    
	// função para sair do modo de cadastro de QR
    public void voltar()
    {
        btnvolta.transform.localPosition = new Vector2(0, 10000);

        btncadastra.transform.localPosition = new Vector2(-30, 550);

        aviso.transform.localPosition = new Vector2(0, 10000);

        modocadastro = false;
        DefaultTrackableEventHandler.modocadastrar = false;
        DefaultTrackableEventHandler.cadastro = false;
    }
	
	// função para entrar no modo de cadastro de QR
    public void CadastrarQr()
    {
        modocadastro = true;
        DefaultTrackableEventHandler.modocadastrar = true;
        DefaultTrackableEventHandler.cadastro = true;

        aviso.transform.localPosition = new Vector2(0, 0);

        btnvolta.transform.localPosition = new Vector2(0, 550);

        btncadastra.transform.localPosition = new Vector2(0, 10000);
    }
	
	// função que atribui um elemento da tabela periódica
	// ao card selecionado
    public void AtribuiBotao(string btn)
    {
        int indice = BD.Banco(btn, atomos);
        Atomos atm = atomos[indice];
        QRs qr = new QRs(atm);
        qrs.Add(qr);

        System.Reflection.FieldInfo campo = this.GetType().GetField(btn);
        Sprite result = (Sprite)campo.GetValue(this);

        qr.Cadastrar(btn, result);

        voltar();
        tabela.transform.localPosition = new Vector2(0, 10000);
    }
	
	// função que limpa todos os QRs com elementos cadastrados
    public void Lixeira()
    {
        GameObject[] apagaQrs = GameObject.FindGameObjectsWithTag("QR");

        for(int a = 0; a < qtdQR; a++)
        {
            apagaQrs[a].GetComponent<DefaultTrackableEventHandler>().qrlivre = true;
            
            foreach(Transform child in GameObject.Find(apagaQrs[a].name).transform)
            {
                if (child.gameObject.name.Contains("GM"))
                    Destroy(child.gameObject);
                else if (child.gameObject.name.Contains("PL"))
                {
                    GameObject.Find(child.gameObject.name).GetComponent<SpriteRenderer>().sprite = null;
                    GameObject.Find(child.gameObject.name).GetComponentInChildren<TextMeshPro>().text = "";
                }
				
				if (child.gameObject.name.Equals("botao"))
                    Destroy(child.gameObject);
                if (child.gameObject.name.Contains("RP"))
                    Destroy(child.gameObject);
            }
        }

        for (int i = 0; i < qrs.Count-1; i++)
        {
            qrs.RemoveAt(i);
        }
    }
}