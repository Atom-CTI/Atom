using UnityEngine;
using System.Collections;

public class StatusBarra : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public float PegarTamanhoBarra(float _minValor, float _maxValor)
    {
        return _minValor / _maxValor;
    }

    public int PegarPorcentagemBarra(float _minValor, float _maxValor, int _fator)
    {
        return Mathf.RoundToInt(PegarTamanhoBarra(_minValor, _maxValor) * _fator);
    }
}
