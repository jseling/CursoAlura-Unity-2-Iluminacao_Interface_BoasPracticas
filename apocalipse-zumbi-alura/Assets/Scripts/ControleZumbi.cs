using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleZumbi : MonoBehaviour
{
    public GameObject Jogador;
    private Status statusInimigo;

    private ControleJogador compControleJogador;

    private MovimentoPersonagem movimentaInimigo;
    private AnimacaoPersonagem animacaoInimigo;
    
    // Start is called before the first frame update
    void Start()
    {
        Jogador = GameObject.FindWithTag("Jogador");
        movimentaInimigo = GetComponent<MovimentoPersonagem>();
        animacaoInimigo = GetComponent<AnimacaoPersonagem>();
        statusInimigo = GetComponent<Status>();
        AleatorizarZumbi();
    }

    void FixedUpdate()
    {
        float distancia = Vector3.Distance(transform.position, Jogador.transform.position);
        
        Vector3 direcao = Jogador.transform.position - transform.position;
        direcao.Normalize();

        movimentaInimigo.Rotacionar(direcao);

        if (distancia > 2.5)
        {
            movimentaInimigo.Movimentar(direcao, statusInimigo.Velocidade);
            animacaoInimigo.Atacar(false);
        }  
        else
        {
            animacaoInimigo.Atacar(true);
        }  
    }

    void AtacaJogador()
    {
        int dano = Random.Range(20, 30);
        compControleJogador.TomarDano(dano);
    }

    void AleatorizarZumbi()
    {
        int tipoZumbi = Random.Range(1, 28);
        transform.GetChild(tipoZumbi).gameObject.SetActive(true);
        compControleJogador = Jogador.GetComponent<ControleJogador>();
    }

}
