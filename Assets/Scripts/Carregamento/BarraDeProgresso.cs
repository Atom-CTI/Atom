using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Threading;
using UnityEngine.SceneManagement;

public class BarraDeProgresso : MonoBehaviour
{

    public StatusBarra _statusBarra;

    public GameObject _barraDeProgesso;
    public Text _textoProgresso;
    public float _maxProgresso;
    public float _progressoAtual;

    // Use this for initialization
    void Start()
    {
        _statusBarra = this.gameObject.GetComponent<StatusBarra>();
    }

    // Update is called once per frame
    void Update()
    {
        _barraDeProgesso.transform.localScale = new Vector3(_statusBarra.PegarTamanhoBarra(_progressoAtual, _maxProgresso), _barraDeProgesso.transform.localScale.y, _barraDeProgesso.transform.localScale.z);
        _textoProgresso.text = _statusBarra.PegarPorcentagemBarra(_progressoAtual, _maxProgresso, 100) + "%";

        if (_progressoAtual < _maxProgresso)
        {
            _progressoAtual += Time.deltaTime * 25;
        }
        else
        {
            _textoProgresso.text = "100%";
            Thread.Sleep(1500);
            SceneManager.LoadScene("Login");
        }
    }
}
