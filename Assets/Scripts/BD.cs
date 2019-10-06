using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.UI;
using System.IO;
using System.Globalization;

public class BD : MonoBehaviour
{
    public static string dados = "166,H,cyan,0.56,1,1,ametal,0.1\n" +
                    "167,Li,yellow,0.855,1,1,metal,1\n" +
                    "168,Na,yellow,0.915,1,1,metal,0.9\n" +
                    "169,K,yellow,1.052,1,1,metal,0.8\n" +
                    "170,Rb,yellow,1.109,1,1,metal,0.8\n" +
                    "171,Cs,yellow,1.2,1,1,metal,0.7\n" +
                    "172,Fr,yellow,1.2,1,1,metal,0.7\n" +
                    "173,Be,yellow,0.713,2,2,metal,1.5\n" +
                    "174,Mg,yellow,0.798,2,2,metal,1\n" +
                    "175,Ca,yellow,0.925,2,2,metal,1\n" +
                    "176,Sr,yellow,0.99,2,2,metal,1\n" +
                    "177,Ba,yellow,1.078,2,2,metal,0.9\n" +
                    "178,Ra,yellow,1.1,2,2,metal\n,0.9" +
                    "179,Sc,magenta,0.899,2,0,transicao,0\n" +
                    "180,Y,magenta,0.972,2,0,transicao,0\n" +
                    "181,Ti,magenta,0.879,2,0,transicao,0\n" +
                    "182,Zr,magenta,0.956,2,0,transicao,0\n" +
                    "183,Hf,magenta,0.961,2,0,transicao,0\n" +
                    "184,Rf,magenta,0.99,2,0,transicao,0\n" +
                    "185,V,magenta,0.866,2,0,transicao,0\n" +
                    "186,Nb,magenta,0.936,1,0,transicao,0\n" +
                    "187,Ta,magenta,0.941,2,0,transicao,0\n" +
                    "188,Db,magenta,0.96,2,0,transicao,0\n" +
                    "189,Cr,magenta,0.853,1,0,transicao,0\n" +
                    "190,Mo,magenta,0.915,1,0,transicao,0\n" +
                    "191,W,magenta,0.923,2,0,transicao,0\n" +
                    "192,Sg,magenta,0.93,2,0,transicao,0\n" +
                    "193,Mn,magenta,0.84,2,0,transicao,0\n" +
                    "194,Tc,magenta,0.897,2,0,transicao,0\n" +
                    "195,Re,magenta,0.91,2,0,transicao,0\n" +
                    "196,Bh,magenta,0.92,2,0,transicao,0\n" +
                    "197,Fe,magenta,0.827,2,0,transicao,0\n" +
                    "198,Ru,magenta,0.884,1,0,transicao,0\n" +
                    "199,Os,magenta,0.902,2,0,transicao,0\n" +
                    "200,Hs,magenta,0.91,2,0,transicao,0\n" +
                    "201,Co,magenta,0.816,2,0,transicao,0\n" +
                    "202,Rh,magenta,0.871,1,0,transicao,0\n" +
                    "203,Ir,magenta,0.889,2,0,transicao,0\n" +
                    "204,Ni,magenta,0.809,1,0,transicao,0\n" +
                    "205,Pd,magenta,0.86,18,0,transicao,0\n" +
                    "206,Pt,magenta,0.881,1,0,transicao,0\n" +
                    "207,Ds,magenta,0.89,2,0,transicao,0\n" +
                    "208,Cu,magenta,0.798,1,0,transicao,0\n" +
                    "209,Ag,magenta,0.85,1,0,transicao,0\n" +
                    "210,Au,magenta,0.873,1,0,transicao,0\n" +
                    "211,Rg,magenta,0.88,2,0,transicao,0\n" +
                    "212,Zn,magenta,0.79,2,0,transicao,0\n" +
                    "213,Cd,magenta,0.84,2,0,transicao,0\n" +
                    "214,Hg,magenta,0.866,2,0,transicao,0\n" +
                    "215,Cn,magenta,0.87,2,0,transicao,0\n" +
                    "216,B,green,0.648,3,3,semimetal,2\n" +
                    "217,Al,yellow,0.728,3,3,metal,1.5\n" +
                    "218,Ga,yellow,0.775,3,3,metal,1.6\n" +
                    "219,In,yellow,0.827,3,3,metal,1.7\n" +
                    "220,Tl,yellow,0.827,3,3,metal,1.8\n" +
                    "221,C,red,0.596,4,4,ametal,2.5\n" +
                    "222,Si,green,0.71,4,4,semimetal,1.8\n" +
                    "223,Ge,green,0.746,4,4,semimetal,1.8\n" +
                    "224,Sn,yellow,0.798,4,4,metal,1.8\n" +
                    "225,Pb,yellow,0.821,4,4,metal,1.9\n" +
                    "226,N,red,0.567,5,-3,ametal,3\n" +
                    "227,P,red,0.676,5,-3,ametal,2.1\n" +
                    "228,As,green,0.718,5,-3,semimetal,2\n" + 
                    "229,Sb,green,0.767,5,-3,semimetal,1.9\n" +
                    "230,Bi,yellow,0.739,5,-3,metal,1.9\n" +
                    "231,O,red,0.547,6,-2,ametal,3.5\n" +
                    "232,S,red,0.65,6,-2,ametal,2.5\n" +
                    "233,Se,red,0.689,6,-2,ametal,2.4\n" +
                    "234,Te,green,0.741,6,-2,semimetal,2.1\n" +
                    "235,Po,green,0.772,6,-2,semimetal,2\n" +
                    "236,F,red,0.531,7,-1,ametal,4\n" +
                    "237,Cl,red,0.627,7,-1,ametal,3\n" +
                    "238,Br,red,0.666,7,-1,ametal,3\n" +
                    "239,I,red,0.72,7,-1,ametal,2.5\n" +
                    "240,At,red,0.728,7,-1,ametal,2.2\n" +
                    "241,He,blue,0.5,2,0,nobre,0\n" +
                    "242,Ne,blue,0.521,8,0,nobre,0\n" +
                    "243,Ar,blue,0.6,8,0,nobre,0\n" +
                    "244,Kr,blue,0.65,8,0,nobre,0\n" +
                    "245,Xe,blue,0.7,8,0,nobre,0\n" +
                    "246,Rn,blue,0.733,8,0,nobre,0\n";

    public static string arq;
    public static StreamWriter salva;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public static int Banco(string btn, List<Atomos> atm)
    {
        string nomeBD;
        string corBD;
        float tamanhoBD;
        int valenciaBD;
        int noxBD;
        string tipoBD;
        float eletronegBD;

        arq = Application.persistentDataPath + "/Atom.csv";

        if (File.Exists(arq))
        {
            File.Delete(arq);
        }

        salva = new StreamWriter(arq, true);

        Debug.Log(arq);

        salva.Write(dados);
        salva.Flush();
        salva.Close();

        string ler = File.ReadAllText(arq);
        string[] linhas = ler.Split("\n"[0]);
        for (var i = 0; i < linhas.Length; i++)
        {
            string[] partes = linhas[i].Split(","[0]);

            if(partes[1] == btn)
            {
                nomeBD = partes[1];
                corBD = partes[2];
                tamanhoBD = float.Parse(partes[3], CultureInfo.InvariantCulture.NumberFormat);
                valenciaBD = int.Parse(partes[4]);
                noxBD = int.Parse(partes[5]);
                tipoBD = partes[6];
                eletronegBD = float.Parse(partes[7], CultureInfo.InvariantCulture.NumberFormat);

                int posicao;
                int tamanhoLista = atm.Count;

                if (tamanhoLista == 0 || (posicao = atm.FindIndex(x => x.nome == nomeBD)) == -1)
                {
                    atm.Add(new Atomos()
                    {
                        nome = nomeBD,
                        cor = corBD,
                        tamanho = tamanhoBD,
                        valencia = valenciaBD,
                        nox = noxBD,
                        tipo = tipoBD,
                        eletroNeg = eletronegBD
                    });

                    return tamanhoLista;
                }
                else
                {
                    return posicao;
                }
            }
            else
            {
                continue;
            }
        }
        return 0;
    }
}
