using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControleJogador : MonoBehaviour
{
    public float Velocidade = 10;
    public LayerMask MascaraChao;
    public GameObject TextoGameOver;
    private Vector3 direcao;
    private Animator compAnimator;
    private Rigidbody compRigidBody;
    public int Vida = 100;
    public ControleInterface scriptControleInterface;
    public AudioClip SomDeDano;

    private void Start()
    {
        Time.timeScale = 1; 

        compAnimator = GetComponent<Animator>(); 
        compRigidBody = GetComponent<Rigidbody>();      
    }

    // Update is called once per frame
    void Update()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);
        direcao.Normalize();

        if (direcao != Vector3.zero)
        {
            compAnimator.SetBool("Movendo", true);
        }     
        else
        {
           compAnimator.SetBool("Movendo", false);
        }

        if(Vida <= 0)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                SceneManager.LoadScene("game");
            }
        }
    }

    void FixedUpdate()
    {
        if (direcao != Vector3.zero)
        {
            compRigidBody.MovePosition
            (compRigidBody.position +
            Time.deltaTime * Velocidade * direcao);
        }

        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(raio.origin, raio.direction * 100, Color.red);

        RaycastHit impacto;

        if(Physics.Raycast(raio, out impacto, 100, MascaraChao))
        {
            Vector3 posicaoMiraJogador = impacto.point - transform.position;

            posicaoMiraJogador.y = transform.position.y;

            Quaternion novaRotacao = Quaternion.LookRotation(posicaoMiraJogador);

            compRigidBody.MoveRotation(novaRotacao);
        }

    }

    public void TomarDano(int _dano)
    {
        Vida -= _dano;
        scriptControleInterface.AtualizaVidaJogador();
        ControleAudio.instancia.PlayOneShot(SomDeDano);

        if (Vida <= 0)
        {
            Time.timeScale = 0;
            TextoGameOver.SetActive(true);
        }



    }
}
