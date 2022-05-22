using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleBala : MonoBehaviour
{
    public float Velocidade = 20;

    private Rigidbody compRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        compRigidBody = GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        compRigidBody.MovePosition
            (compRigidBody.position +
            transform.forward * Velocidade * Time.deltaTime);

    }

    void OnTriggerEnter(Collider objetoDeColisao)
    {
        if(objetoDeColisao.tag == "Inimigo")
        {
            Destroy(objetoDeColisao.gameObject);
        }
         Destroy(gameObject);
    }    
}
