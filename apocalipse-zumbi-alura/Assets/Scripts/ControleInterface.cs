using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControleInterface : MonoBehaviour
{
    private ControleJogador scriptControleJogador;
    public Slider SliderVidaJogador;

    // Start is called before the first frame update
    void Start()
    {
        scriptControleJogador = GameObject.FindWithTag("Jogador").GetComponent<ControleJogador>();

        SliderVidaJogador.maxValue = scriptControleJogador.StatusJogador.Vida;
        AtualizaVidaJogador();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AtualizaVidaJogador()
    {
        SliderVidaJogador.value = scriptControleJogador.StatusJogador.Vida;
    }
}
