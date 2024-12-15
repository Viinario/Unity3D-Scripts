using System.Collections;
using UnityEngine;

public class PortaNave : MonoBehaviour
{
    [System.Serializable]
    public class Porta
    {
        public GameObject door; // Objeto da porta
        public float anguloRotacao = 90f; // Ângulo de rotação no eixo Z
        public float anguloOriginal = 0f; // Ângulo inicial da porta
        public KeyCode teclaAtivadora = KeyCode.E; // Tecla para ativar a rotação da porta
    }

    public Porta[] portas; // Array de portas (com seus próprios dados)

    void Update()
    {
        // Verifica se alguma das teclas foi pressionada
        foreach (var porta in portas)
        {
            if (Input.GetKeyDown(porta.teclaAtivadora)) // Verifica a tecla de cada porta
            {
                // Alterna entre abrir ou fechar a porta
                if (Mathf.Abs(NormalizarAngulo(porta.door.transform.eulerAngles.z) - porta.anguloOriginal) < 0.1f)
                {
                    RotacionarPorta(porta, porta.anguloRotacao);
                }
                else
                {
                    RotacionarPorta(porta, -porta.anguloRotacao);
                }
            }
        }
    }

    void RotacionarPorta(Porta porta, float angulo)
    {
        RotacionarInstantaneo(porta.door, angulo);
    }

    // Função para rotacionar a porta instantaneamente
    void RotacionarInstantaneo(GameObject porta, float angulo)
    {
        // Normaliza o ângulo atual e aplica a rotação instantaneamente
        float anguloFinal = NormalizarAngulo(porta.transform.eulerAngles.z) + angulo;
        porta.transform.eulerAngles = new Vector3(porta.transform.eulerAngles.x, porta.transform.eulerAngles.y, anguloFinal);
    }

    // Função para normalizar o ângulo para o intervalo de 0 a 360 graus
    float NormalizarAngulo(float angulo)
    {
        // Normaliza os ângulos para o intervalo de 0 a 360 graus
        angulo = (angulo < 0) ? 360 + angulo : angulo;
        angulo = (angulo >= 360) ? angulo - 360 : angulo;
        return angulo;
    }
}
