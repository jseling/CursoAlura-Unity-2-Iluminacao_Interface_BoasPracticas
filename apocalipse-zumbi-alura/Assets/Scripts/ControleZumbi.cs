using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleZumbi : MonoBehaviour
{
    public GameObject Jogador;
    public float Velocidade = 5;

    private Rigidbody compRigidBody;
    private Animator compAnimator;
    private ControleJogador compControleJogador;
    
    // Start is called before the first frame update
    void Start()
    {
       Jogador = GameObject.FindWithTag("Jogador");
       int tipoZumbi = Random.Range(1, 28);
       transform.GetChild(tipoZumbi).gameObject.SetActive(true);

       compRigidBody = GetComponent<Rigidbody>();
       compAnimator = GetComponent<Animator>();
       compControleJogador = Jogador.GetComponent<ControleJogador>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        float distancia = Vector3.Distance(transform.position, Jogador.transform.position);
        
        Vector3 direcao = Jogador.transform.position - transform.position;
        Quaternion novaRotacao = Quaternion.LookRotation(direcao); 
        compRigidBody.MoveRotation(novaRotacao);         

        if(distancia > 2.5)
        {
            compRigidBody.MovePosition
                (compRigidBody.position + 
                direcao.normalized * Velocidade * Time.deltaTime);  

            compAnimator.SetBool("Atacando", false);          
        }  
        else
        {
            compAnimator.SetBool("Atacando", true); 
        }  
    }

    void AtacaJogador()
    {
        Time.timeScale = 0;
        compControleJogador.TextoGameOver.SetActive(true);
        compControleJogador.Vivo = false;
    }
}
