using UnityEngine;
using TMPro; 

public class Ugui : MonoBehaviour
{
    private TextMeshProUGUI textoMoedas;

    private void Awake()
    {
        textoMoedas = GetComponent<TextMeshProUGUI>();
    }

    
    private void OnEnable()
    {
        PlayerOM.OnCoinCountChanged += AtualizarTextoMoedas;
    }

   
    private void OnDisable()
    {
        PlayerOM.OnCoinCountChanged -= AtualizarTextoMoedas;
    }


    private void AtualizarTextoMoedas(int totalAtual)
    {
        textoMoedas.text = "Moedas: " + totalAtual;
    }
}