using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControleJogador : MonoBehaviour
{
    public float Velocidade = 10;

    public bool Vivo = true;    

    public LayerMask MascaraChao;

    public GameObject TextoGameOver;

    private Vector3 direcao;

    private Animator compAnimator;

    private Rigidbody compRigidBody;

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

        if(direcao != Vector3.zero)
        {
            compAnimator.SetBool("Movendo", true);
        }     
        else
        {
           compAnimator.SetBool("Movendo", false);
        }

        if(Vivo ==false)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                SceneManager.LoadScene("game");
            }
        }
    }

    void FixedUpdate()
    {
        compRigidBody.MovePosition
            (compRigidBody.position +
            direcao * Velocidade * Time.deltaTime);

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
}
