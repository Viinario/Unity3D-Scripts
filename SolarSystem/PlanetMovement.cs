using UnityEngine;

// Classe que controla o movimento de um planeta, incluindo sua rotação própria e sua órbita ao redor de um centro
public class PlanetMovement : MonoBehaviour
{
    // Referência ao objeto que será o centro da órbita (exemplo: o Sol)
    public Transform center;

    // Velocidade de rotação do planeta em torno de seu próprio eixo (auto-rotação)
    public float rotationSpeed = 10f;

    // Velocidade de translação (órbita) do planeta ao redor do centro
    public float orbitSpeed = 5f;

    // Distância entre o planeta e o centro da órbita
    public float orbitRadius = 20f;

    // Define o eixo de rotação da translação (padrão: eixo Y, para órbitas horizontais)
    private Vector3 orbitAxis = Vector3.up;

    // Update é chamado uma vez por frame para atualizar o comportamento do objeto
    void Update()
    {
        // Faz o planeta girar em torno de seu próprio eixo
        // Vector3.up indica que a rotação ocorre ao redor do eixo Y (para cima)
        // rotationSpeed é multiplicada por Time.deltaTime para garantir uma rotação suave e independente da taxa de quadros
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Se há um centro definido, realiza a translação (órbita) ao redor dele
        if (center != null)
        {
            // RotateAround move o objeto em torno de um ponto (center.position), em um eixo especificado (orbitAxis)
            // A velocidade de translação (orbitSpeed) é ajustada usando Time.deltaTime para manter suavidade
            transform.RotateAround(center.position, orbitAxis, orbitSpeed * Time.deltaTime);
        }
    }
}
