using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameMannager : MonoBehaviour
{


    public GameObject aviso;


    public GameObject btnvolta;
    public GameObject btncadastra;
    public GameObject tabela;
    public GameObject btnTabela;

    public static bool modocadastro;

    public static List<Atomos> atomos;
    public static List<QRs> qrs;

    public static int contadorQR = 0;

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

    //criar sprite dos 20 produtos

    public static string qrName;

    public static List<string> reacao;

    string novocard = "";
    string nomeproduto = "";

    GameObject representacao;
    GameObject produto;

    //public static List<List<string>> nomesocial;

    public static bool podefazerreacao;
    void Start()
    {


        podefazerreacao = true;
        reacao = new List<string>();
        //nomesocial = new List<List<string>>();

        btnvolta = GameObject.Find("btnVoltar");
        btnvolta.transform.localPosition = new Vector2(0, 10000);

        btncadastra = GameObject.Find("btnCadastrar");
        btncadastra.transform.localPosition = new Vector2(-50, 635);

        aviso = GameObject.Find("aviso");
        aviso.transform.localPosition = new Vector2(0, 10000);

        tabela = GameObject.Find("ScrollTabela");
        tabela.transform.localPosition = new Vector2(0, 10000);

        modocadastro = false;

        atomos = new List<Atomos>();
        qrs = new List<QRs>();

        InvokeRepeating("colisaoupdate", 0, (float)1.0);
    }

    public List<string> elementosreacao = new List<string>();
    public List<Atomos> atomosdareacao;
    public List<string> elementosreacaoaux = new List<string>();
    private void colisaoupdate()
    {
        bool existenoelementosreacao = false;
        bool podeaddlista = false;

        for (int a = 0; a < qrs.Count; a++)
        {
            if (qrs[a].card == null)
            {
                continue;
            }
            else
            {
                if (!qrs[a].card.GetComponent<SpriteRenderer>().enabled)
                {
                    elementosreacao.Remove("PL" + qrs[a].qrName); // remove da lista se qr sair de cena
                    continue;
                }

                Collider[] hitColliders = Physics.OverlapBox(qrs[a].card.transform.position, new Vector3((float)90, (float)90, (float)90));

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



                for (int i = 0; i < hitColliders.Length; i++)
                {
                    if (hitColliders[i].gameObject.name.Contains("PL") && hitColliders[i].gameObject.activeInHierarchy && qrs[a].card.activeInHierarchy)
                    {
                        //Debug.Log("collider " + qrs[a].qrName + ": " + hitColliders.Length);
                        Debug.Log(qrs[a].qrName + ": " + hitColliders[i]);
                        if (elementosreacao.Count == 0)
                        {
                            elementosreacao.Add("PL" + qrs[a].qrName);
                        }
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
                            elementosreacao.Add(hitColliders[i].gameObject.name);
                        }

                    }

                }

            }

        }

        Debug.Log("sahuashusahushushusauhashusauh");
        Debug.Log(elementosreacao.Count);
        Debug.Log(elementosreacaoaux.Count);
        if (elementosreacao.Equals(elementosreacaoaux) || elementosreacao.Count == 1)
        {
            return;
        }
        else
        {

            /*for(int z=0;z<elementosreacao.Count;z++)
            {

            //Debug.Log("Final:   " +elementosreacao[z]);
            }
            //chama programa
            elementosreacaoaux = new List<string>(elementosreacao);*/
            atomosdareacao = new List<Atomos>();

            /*for(int i = 0; i < qrs.Count; i++)
            {
                Debug.Log("atm: " + qrs[i].atomo.nome);
            }*/

            for (int b = 0; b < elementosreacao.Count; b++)
            {
                for (int n = 0; n < qrs.Count; n++)
                {
                    Debug.Log("1111111 " + qrs.Count);
                    //Debug.Log("22222222" + elementosreacao[b]);
                    if (qrs[n].card.name.Contains(elementosreacao[b]))
                    {
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
                //Debug.Log(qrs.Find(x => x.card.name.Contains(elementosreacao[b])).atomo.nome);
                //atomosdareacao.Add(qrs.Find(x => x.card.name.Contains(elementosreacao[b])).atomo);
            }

            if (atomosdareacao.Count <= 1)
                return;

            bool fazreacao = true;

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
            Atomos[] atomosreaction;
            if (fazreacao)
            {
                atomosdareacao.Sort((x, y) => string.Compare(x.nome, y.nome, System.StringComparison.Ordinal));
                atomosreaction = atomosdareacao.ToArray();
                for (int i = 0; i < atomosdareacao.Count; i++)
                {
                    Debug.Log("debug final:   " + atomosreaction[i].nome);
                }

                bool reagiu = ReacaoQuimica.reacao(atomosreaction, atomosdareacao.Count);

                if (reagiu)
                {
                    nomeproduto = "";
                    for (int i = 0; i < atomosdareacao.Count; i++)
                    {
                        nomeproduto = nomeproduto + atomosdareacao[i].nome;

                    }
                    Debug.Log(nomeproduto);

                    //GameObject produto;
                    QRs final = qrs.Find(x => elementosreacao[0].Contains(x.qrName));
                    novocard = elementosreacao[0];
                    elementosreacao.RemoveAt(0);

                    List<int> indexremover = new List<int>();
                    //limpar qrs e colocar produto em um deles
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
                                //qrs[a].qrName = null;
                                //qrs[a].card = null;
                                //qrs[a].atomo = null;
                                indexremover.Add(a);
                                //qrs.Remove(qrs[a]); 
                            }
                        }
                    }
                    indexremover.Sort();

                    for (int i = indexremover.Count - 1; i >= 0; i--)
                    {
                        Debug.Log("IT" + qrs[indexremover[i]].qrName);
                        foreach (Transform child in GameObject.Find("IT" + qrs[indexremover[i]].qrName).transform)
                        {
                            if (child.gameObject.name.Contains("GM"))
                                Destroy(child.gameObject);
                            else
                                GameObject.Find(child.gameObject.name).GetComponent<SpriteRenderer>().sprite = null;
                        }

                        qrs.RemoveAt(indexremover[i]);

                    }

                    elementosreacao.Clear();
                    elementosreacao.Add(novocard);

                    for (int i = 0; i < final.atomo.Count; i++)
                    {
                        Debug.Log("--------------Final " + final.atomo[i].nome);
                    }

                    nomeproduto = nomeproduto.ToUpper();



                    if (produto = Resources.Load("GM" + nomeproduto) as GameObject)
                    {
                        Debug.Log("produto final " + nomeproduto);

                        //apagar gm que tava - child do it que contem PL no nome
                        foreach (Transform child in GameObject.Find("IT" + novocard.Remove(0, 2)).transform)
                        {
                            if (child.gameObject.name.Contains("GM"))
                            {
                                Destroy(child.gameObject);
                                break;
                            }
                        }

                        //produto = Resources.Load("GM" + nomeproduto) as GameObject;
                        qrs.Find(x => novocard.Contains(x.qrName)).gm = produto;
                        GameObject gm = Instantiate(produto) as GameObject;
                        gm.name = "GM" + nomeproduto;
                        gm.transform.parent = GameObject.Find("IT" + novocard.Remove(0, 2)).transform;
                        //posicao
                        gm.transform.localPosition = new Vector3(-1, (float)1.5, 0);
                        // tamanho
                        gm.transform.localScale = new Vector3((float)1, (float)1, (float)1);


                        System.Reflection.FieldInfo campo = this.GetType().GetField("PL" + nomeproduto);
                        //        Debug.Log(campo);
                        Sprite result = (Sprite)campo.GetValue(this);
                        GameObject.Find(novocard).GetComponent<SpriteRenderer>().sprite = result;
                        GameObject.Find(novocard).transform.localScale = new Vector3((float)0.15, (float)0.15, (float)0.15);
                        qrs.Find(x => novocard.Contains(x.qrName)).card = GameObject.Find(novocard);

                        if (representacao = Resources.Load("RP" + nomeproduto) as GameObject)
                        {
                            StartCoroutine("Wait");
                        }
                    } //if
                    else
                    {
                        Debug.Log(nomeproduto);//printar tela produto sem molecula
                    }
                    //GameObject produto = Resources.Load(nomeproduto) as GameObject;
                    //GameObject GMletra = Instantiate(letra) as GameObject;
                    //procurar nomeproduto na pasta
                    //se achar, printar molecula
                    //se nao achar, printar na tela o produto com nome certinho
                }
                else
                {
                    Debug.Log("Nao reagiu");
                    //debug nao reagui na tela
                }

                // alterar essa funcao para retornar corretamente o objeto
                // objeto contendo lista de ligacoes e string com o nome da molecula
            }

        }


    }


    public IEnumerator Wait()
    {
        representacao = Resources.Load("RP" + nomeproduto) as GameObject;

        yield return new WaitForSeconds(2);
        Destroy(GameObject.Find("GM" + nomeproduto));
        GameObject gm = Instantiate(representacao) as GameObject;
        gm.name = "RP" + nomeproduto;
        gm.transform.parent = GameObject.Find("IT" + novocard.Remove(0, 2)).transform;
        //posicao
        gm.transform.localPosition = new Vector3(0, (float)1.5, (float)-0.5);
        // tamanho
        gm.transform.localScale = new Vector3((float)0.04, (float)0.04, (float)0.04);

        StartCoroutine("Wait2");

    }

    public IEnumerator Wait2()
    {

        yield return new WaitForSeconds(2);
        Destroy(GameObject.Find("RP" + nomeproduto));
        GameObject gm = Instantiate(produto) as GameObject;
        gm.name = "GM" + nomeproduto;
        gm.transform.parent = GameObject.Find("IT" + novocard.Remove(0, 2)).transform;
        //posicao
        gm.transform.localPosition = new Vector3(-1, (float)1.5, 0);
        // tamanho
        gm.transform.localScale = new Vector3((float)1, (float)1, (float)1);
    }




    void Update()
    {
    }





    public void voltar()
    {
        btnvolta.transform.localPosition = new Vector2(0, 10000);

        btncadastra.transform.localPosition = new Vector2(-50, 635);

        aviso.transform.localPosition = new Vector2(0, 10000);

        modocadastro = false;
        DefaultTrackableEventHandler.modocadastrar = false;
        DefaultTrackableEventHandler.cadastro = false;
    }


    public void CadastrarQr()
    {
        modocadastro = true;
        DefaultTrackableEventHandler.modocadastrar = true;
        DefaultTrackableEventHandler.cadastro = true;

        aviso.transform.localPosition = new Vector2(0, 0);

        btnvolta.transform.localPosition = new Vector2(-50, 635);

        btncadastra.transform.localPosition = new Vector2(0, 10000);
    }


    public void AtribuiBotao(string btn)
    {
        int indice = BD.Banco(btn, atomos);
        Atomos atm = atomos[indice];
        QRs qr = new QRs(atm);
        qrs.Add(qr);

        System.Reflection.FieldInfo campo = this.GetType().GetField(btn);
        //        Debug.Log(campo);
        Sprite result = (Sprite)campo.GetValue(this);
        //        Debug.Log(result);

        qr.Cadastrar(btn, result);

        voltar();
        tabela.transform.localPosition = new Vector2(0, 10000);
    }

    public void Lixeira()
    {


        for (int i = 0; i <= qrs.Count; i++)
        {
            Debug.Log("IT" + qrs[i].qrName);
            foreach (Transform child in GameObject.Find("IT" + qrs[i].qrName).transform)
            {
                if (child.gameObject.name.Contains("GM"))
                    Destroy(child.gameObject);
                else
                    GameObject.Find(child.gameObject.name).GetComponent<SpriteRenderer>().sprite = null;
            }

            qrs.RemoveAt(i);

        }
    }

}