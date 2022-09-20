using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControleJogador : MonoBehaviour
{
    public LayerMask MascaraChao;
    public GameObject TextoGameOver;
    private Vector3 direcao;
    public ControleInterface scriptControleInterface;
    public AudioClip SomDeDano;
    private MovimentoJogador meuMovimentoJogador;
    private AnimacaoPersonagem meuAnimacaoJogador;
    public Status StatusJogador;

    private void Start()
    {
        Time.timeScale = 1; 
        meuMovimentoJogador = GetComponent<MovimentoJogador>();
        meuAnimacaoJogador = GetComponent<AnimacaoPersonagem>();
        StatusJogador = GetComponent<Status>();
    }

    // Update is called once per frame
    void Update()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);

        if (direcao != Vector3.zero)
            direcao.Normalize();

        meuAnimacaoJogador.Movimentar(direcao.magnitude);

        if (StatusJogador.Vida <= 0)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                SceneManager.LoadScene("game");
            }
        }
    }

    void FixedUpdate()
    {
            meuMovimentoJogador.Movimentar(direcao, StatusJogador.Velocidade);
            meuMovimentoJogador.RotacaoJogador(MascaraChao);
    }

    public void TomarDano(int _dano)
    {
        StatusJogador.Vida -= _dano;
        scriptControleInterface.AtualizaVidaJogador();
        ControleAudio.instancia.PlayOneShot(SomDeDano);

        if (StatusJogador.Vida <= 0)
        {
            Time.timeScale = 0;
            TextoGameOver.SetActive(true);
        }



    }
}
