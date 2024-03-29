using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorZumbis : MonoBehaviour
{
    public GameObject Zumbi;

    float contadorTempo = 0;

    public float TempoGerarZumbi = 1;
    public LayerMask LayerZumbi;

    private float distanciaDeGeracao = 3;

    public float DistanciaDoJogadorParaGeracao = 20;

    private GameObject jogador;

    private void Start()
    {
        jogador = GameObject.FindWithTag("Jogador");
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, jogador.transform.position) > DistanciaDoJogadorParaGeracao)
        {
            contadorTempo += Time.deltaTime;

            if (contadorTempo >= TempoGerarZumbi)
            {
                StartCoroutine(GerarNovoZumbi());
                contadorTempo = 0;
            }
        }    
    }

    IEnumerator GerarNovoZumbi()
    {
        Vector3 posicaoDeCriacao = AleatorizarPosicao();
        Collider[] colisores = Physics.OverlapSphere(posicaoDeCriacao, 1, LayerZumbi);

        while (colisores.Length > 0)
        {
            posicaoDeCriacao = AleatorizarPosicao();
            colisores = Physics.OverlapSphere(posicaoDeCriacao, 1, LayerZumbi);
            yield return null;
        }
        Instantiate(Zumbi, posicaoDeCriacao, transform.rotation);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanciaDeGeracao);
    }

    Vector3 AleatorizarPosicao()
    {
        Vector3 posicao = Random.insideUnitSphere * distanciaDeGeracao;
        posicao += transform.position;
        posicao.y = 0;

        return posicao;
    }
}
