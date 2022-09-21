using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleZumbi : MonoBehaviour, IMatavel
{
    public GameObject Jogador;
    private StatusInimigo statusInimigo;

    private ControleJogador compControleJogador;

    private MovimentoPersonagem movimentaInimigo;
    private AnimacaoPersonagem animacaoInimigo;

    public AudioClip SomDeMorte;

    private Vector3 posicaoAleatoria;
    private Vector3 direcao;

    private float contadorVagar;
    public float DistanciaVagar = 10;

    // Start is called before the first frame update
    void Start()
    {
        Jogador = GameObject.FindWithTag("Jogador");
        movimentaInimigo = GetComponent<MovimentoPersonagem>();
        animacaoInimigo = GetComponent<AnimacaoPersonagem>();
        statusInimigo = GetComponent<StatusInimigo>();
        AleatorizarZumbi();
    }

    void FixedUpdate()
    {
        float distancia = Vector3.Distance(transform.position, Jogador.transform.position);

        movimentaInimigo.Rotacionar(direcao);
        animacaoInimigo.Movimentar(direcao.magnitude);

        if (distancia > 15)
        {
            Vagar();
        }
        else if (distancia > 2.5)
        {
            Perseguir();
        }  
        else
        {
            animacaoInimigo.Atacar(true);
        }  
    }

    void Perseguir()
    {
        direcao = Jogador.transform.position - transform.position;
        movimentaInimigo.Movimentar(direcao, statusInimigo.Velocidade);
        animacaoInimigo.Atacar(false);
    }

    void Vagar()
    {
        contadorVagar -= Time.deltaTime;
        if (contadorVagar <= 0)
        {
            posicaoAleatoria = AleatorizarPosicao();
            contadorVagar += statusInimigo.TempoEntrePosicoesVagar;
        }

        bool ficouPertoOSuficiente = Vector3.Distance(transform.position, posicaoAleatoria) <= 0.05;
        if (ficouPertoOSuficiente == false)
        {
            direcao = posicaoAleatoria - transform.position;
            movimentaInimigo.Movimentar(direcao, statusInimigo.Velocidade);
        }
    }

    Vector3 AleatorizarPosicao()
    {
        Vector3 posicao = Random.insideUnitSphere * DistanciaVagar;
        posicao += transform.position;
        posicao.y = transform.position.y;

        return posicao;
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

    public void TomarDano(int dano)
    {
        statusInimigo.Vida -= dano;
        if (statusInimigo.Vida <= 0)
        {
            Morrer();
        }

    }

    public void Morrer()
    {
        Destroy(gameObject);
        ControleAudio.instancia.PlayOneShot(SomDeMorte);
    }
}
