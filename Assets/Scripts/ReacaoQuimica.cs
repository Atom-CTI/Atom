﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ReacaoQuimica
{
	// indica qual a ligação entre os atomos
    public enum TipoLigacao { SIMPLES, DUPLA, TRIPLA, DATIVA, ERRO }

	// estrutura para ligar dois atomos
    public struct Ligacao
    {
        public Atomos primeiro;
        public Atomos segundo;

        public TipoLigacao tipo;
    }

	// cria uma ligação entre dois atomos (primeiro e segundo) da "Atomos[] lista"
    public static Ligacao criarLigacao(Atomos[] lista, int primeiro, int segundo)
    {
        Ligacao novaLigacao = new Ligacao();

        int mudanca = 0;

        if ((lista[primeiro].eletronsDisponiveis == 0 || lista[segundo].eletronsDisponiveis == 0) ||
            (lista[primeiro].eletronsAtuais >= 8 || lista[segundo].eletronsAtuais >= 8 ||
             lista[primeiro].eletronsAtuais <= 0 || lista[segundo].eletronsAtuais <= 0))
        {
            // erro
            novaLigacao.tipo = TipoLigacao.ERRO;
            return novaLigacao;
        }
		// ligação tripla
        else if (lista[primeiro].eletronsDisponiveis >= 3 && lista[segundo].eletronsDisponiveis >= 3 &&
                (lista[primeiro].eletronsAtuais <= 5 && lista[segundo].eletronsAtuais <= 5))
        {
            novaLigacao.tipo = TipoLigacao.TRIPLA;
            mudanca = 3;
        }
		// ligação dupla
        else if (lista[primeiro].eletronsDisponiveis >= 2 && lista[segundo].eletronsDisponiveis >= 2 &&
                (lista[primeiro].eletronsAtuais <= 6 && lista[segundo].eletronsAtuais <= 6))
        {
            novaLigacao.tipo = TipoLigacao.DUPLA;
            mudanca = 2;
        }
		// ligação simples
        else if (lista[primeiro].eletronsDisponiveis >= 1 && lista[segundo].eletronsDisponiveis >= 1 &&
                (lista[primeiro].eletronsAtuais <= 7 && lista[segundo].eletronsAtuais <= 7))
        {
            novaLigacao.tipo = TipoLigacao.SIMPLES;
            mudanca = 1;
        }

		// atualiza as informações para a ligação
        lista[primeiro].eletronsAtuais += mudanca;
        lista[segundo].eletronsAtuais += mudanca;

        lista[primeiro].eletronsDisponiveis -= mudanca;
        lista[segundo].eletronsDisponiveis -= mudanca;
		
		// colocq informações em "novaLigação"
        novaLigacao.primeiro = lista[primeiro];
        novaLigacao.segundo = lista[segundo];

        novaLigacao.primeiro.ligado++;
        novaLigacao.segundo.ligado++;

        return novaLigacao;
    }

    public static bool reacao(Atomos[] lista, int tamanho)
    {
        int separador = 0;

        // se existirem dois grupos de elementos
        if (lista[0].nome != lista[tamanho - 1].nome)
        {
            // varre o array e encontra o início do segundo grupo
            for (separador = 0; ; separador++)
            {
                if (!lista[separador].nome.Equals(lista[0].nome)) { break; }
            }
        }
        // separador = inicio do segundo grupo, ou zero se não houver

        int sucessos = 0;
        int fracassos = 0;

        Ligacao[] listaLigacao = new Ligacao[20];
        Ligacao novaLigacao = new Ligacao();

        // se existirem dois grupos
        if (separador != 0)
        {
            int grupo1_atual = 0;
            int grupo2_atual = separador;

            while ((lista[separador - 1].eletronsAtuais != 8 || lista[separador - 1].eletronsAtuais != 2) && (lista[tamanho - 1].eletronsAtuais != 8 || lista[tamanho - 1].eletronsAtuais != 2) && fracassos < 10)
            {
                novaLigacao = criarLigacao(lista, grupo1_atual, grupo2_atual);
                //Debug.Log(novaLigacao.primeiro.eletronsAtuais);
                //Debug.Log(novaLigacao.segundo.eletronsDisponiveis);
                if (novaLigacao.tipo != TipoLigacao.ERRO)
                {
                    listaLigacao[sucessos] = novaLigacao;
                    sucessos++;
                }
                else
                {
                    fracassos++;
                }

                // checa se os indices são maiores que seus limites
                // se sim, retornar ao valor original
                if ((lista[grupo1_atual].eletronsAtuais == 8 || lista[grupo1_atual].eletronsAtuais == 2)
                   || lista[grupo1_atual].ligado == 2)
                {
                    ++grupo1_atual;
                }
                if (grupo1_atual >= separador) { grupo1_atual = 0; }

                if ((lista[grupo2_atual].eletronsAtuais == 8 || lista[grupo2_atual].eletronsAtuais == 2)
                   || lista[grupo1_atual].ligado == 2)
                {
                    ++grupo2_atual;
                }
                if (grupo2_atual >= tamanho) { grupo2_atual = separador; }
            }
        }

        // dativa

        if (separador != 0 && (!lista[0].nome.Equals("H") && !lista[tamanho - 1].nome.Equals("H")))
        {
			// checa a existência de elementos não ligados
            Atomos[] resto = new Atomos[10];
            int indiceResto = 0;
            for (int i = 0; i < tamanho; i++)
            {
                if (lista[i].eletronsAtuais != 8 && lista[i].eletronsAtuais != 2)
                {
                    resto[indiceResto] = lista[i];
                    indiceResto++;
                }
            }
			
            // dativa com o primeiro recebendo
            if (indiceResto != 0 && ((lista[0].eletroNeg > lista[tamanho - 1].eletroNeg && resto[0].nome == lista[0].nome) ||
                (lista[0].nome.Equals("C") && lista[0].eletronsDisponiveis >= 2)))
            {
                int recebeAtual = 0;
                int daAtual = separador;
                
                while ((resto[recebeAtual].ligado == 0 || resto[recebeAtual].eletronsAtuais != 8) && lista[daAtual].eletronsDisponiveis >= 2)
                {

                    resto[recebeAtual].eletronsAtuais += 2;
                    lista[daAtual].eletronsDisponiveis -= 2;
					
					resto[recebeAtual].ligado++;
					lista[daAtual].ligado++;
					
                    listaLigacao[sucessos].primeiro = resto[recebeAtual];
                    listaLigacao[sucessos].segundo = lista[daAtual];
                    listaLigacao[sucessos].tipo = TipoLigacao.DATIVA;

                    sucessos++;

                    // checa se os indices são maiores que seus limites
                    // se sim, retornar ao valor original
                    if (daAtual < tamanho) { daAtual++; }
                    if (daAtual >= tamanho) { daAtual = separador; }

                    if (recebeAtual < indiceResto) { recebeAtual++; }
                    if (recebeAtual >= indiceResto) { recebeAtual = 0; }
                }
            }
            // dativa com o segundo recebendo
            if (indiceResto != 0 && ((lista[0].eletroNeg < lista[tamanho - 1].eletroNeg && resto[0].nome == lista[tamanho - 1].nome) ||
                (lista[tamanho - 1].nome.Equals("C") && lista[0].eletronsDisponiveis >= 2)))
            {
                int recebeAtual = 0;
                int daAtual = 0;
                while ((lista[recebeAtual].ligado == 0 || lista[recebeAtual].eletronsAtuais != 8) && resto[daAtual].eletronsDisponiveis >= 2)
                {
                    resto[daAtual].eletronsAtuais -= 2;
                    lista[recebeAtual].eletronsDisponiveis += 2;

                    resto[daAtual].ligado++;
                    lista[recebeAtual].ligado++;

                    listaLigacao[sucessos].primeiro = resto[daAtual];
                    listaLigacao[sucessos].segundo = lista[recebeAtual];
                    listaLigacao[sucessos].tipo = TipoLigacao.DATIVA;

                    sucessos++;

                    // checa se os indices são maiores que seus limites
                    // se sim, retornar ao valor original
                    if (daAtual < indiceResto) { daAtual++; }
                    if (daAtual >= indiceResto) { daAtual = 0; }

                    if (recebeAtual < separador) { recebeAtual++; }
                    if (recebeAtual >= separador) { recebeAtual = 0; }
                }
            }
        }
		
		int grupo = -1;
        int[] naoBalanceado = new int[4];
        naoBalanceado[0] = -1;
        naoBalanceado[1] = -1;
        naoBalanceado[2] = -1;
        naoBalanceado[3] = -1;

        // se existe apenas um grupo de elementos
        if (separador == 0)
        {
            for (int i = 0, j = 0; i < tamanho; i++)
            {
                if (lista[i].eletronsDisponiveis > 0 && lista[i].eletronsAtuais != 8)
                {
                    grupo = 0;
                    naoBalanceado[j] = i;
                    j++;
                }
            }
        }
        // se um grupo já está balanceado, varrer ambos os grupos, encontrando
        // quaisquer elementos não balanceados, e adicionando eles a um array
        else
        {
            int j = 0;
            for (int i = 0; i < separador; i++)
            {
                if (lista[i].eletronsDisponiveis > 0 && lista[i].eletronsAtuais != 8 && lista[i].eletronsAtuais != 2)
                {
                    grupo = 0;
                    naoBalanceado[j] = i;
                    j++;
                }
            }

            if (grupo == -1)
            {
                j = 0;
                for (int i = separador; i < tamanho; i++)
                {
                    if (lista[i].eletronsDisponiveis > 0 && lista[i].eletronsAtuais != 8 && lista[i].eletronsAtuais != 2)
                    {
                        grupo = separador;
                        naoBalanceado[j] = i;
                        j++;
                    }
                }
            }
        }
		
		// criar uma ligação entre os elementos de mesmo tipo se necessário
        if (grupo != -1 && naoBalanceado[0] != -1 && naoBalanceado[1] != -1
             && lista[naoBalanceado[0]].ligado == 0 && lista[naoBalanceado[1]].ligado == 0)
        {
            novaLigacao = criarLigacao(lista, grupo + naoBalanceado[0], grupo + naoBalanceado[1]);
            if (novaLigacao.tipo != TipoLigacao.ERRO)
            {
                listaLigacao[sucessos++] = novaLigacao;
            }
        }

        int numeroUm = separador;
        int numeroDois = tamanho - separador;
        int numeroLig = sucessos;

        int div;
        for (div = 6; div > 1; div--)
        {
            if (separador % div == 0 && (tamanho - separador) % div == 0 && sucessos % div == 0)
            {
                numeroUm = numeroUm / div;
                numeroDois = numeroDois / div;
                numeroLig = numeroLig / div;
                break;
            }
        }

        int numeroDativa = 0;
        for (int i = 0; i < sucessos; i++)
        {
            if (listaLigacao[i].tipo == TipoLigacao.DATIVA)
            {
                numeroDativa++;
            }
        }

        Ligacao[] listaLigacaoFinal = new Ligacao[20];

        int k = 0;
        for (int i = 0; i < numeroLig - numeroDativa; i++)
        {
            listaLigacaoFinal[k] = listaLigacao[i];
            k++;
        }
        for (int i = numeroLig - numeroDativa; i < numeroLig - (numeroDativa / div); i++)
        {
            listaLigacaoFinal[k] = listaLigacao[i];
            k++;
        }

        for (int i = 0; i < tamanho; i++)
        {
            if ((lista[i].eletronsDisponiveis != 0 &&
                lista[i].eletronsAtuais < 8) ||
                lista[i].ligado == 0)
            {
                return false;
            }
        }
        return true;
    }


}
