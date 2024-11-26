using UnityEngine;

// Classe que controla o movimento da Lua, incluindo sua rotação própria e sua órbita ao redor da Terra
public class MoonMovement : MonoBehaviour
{
    // Referência ao objeto ao redor do qual a Lua irá orbitar (exemplo: a Terra)
    public Transform earth;

    // Velocidade de rotação da Lua em torno de seu próprio eixo (auto-rotação)
    public float rotationSpeed = 15f;

    // Velocidade da translação (órbita) da Lua ao redor da Terra
    public float orbitSpeed = 10f;

    // Distância fixa entre a Lua e a Terra
    public float orbitRadius = 5f;

    // Eixo de rotação da translação; por padrão, o eixo Y (órbita horizontal)
    private Vector3 orbitAxis = Vector3.up;

    // Vetor que armazena o deslocamento inicial da Lua em relação à Terra
    private Vector3 offset;

    // Função chamada no início da execução, usada para inicializar valores
    void Start()
    {
        // Verifica se a Terra está definida para evitar erros de referência nula
        if (earth != null)
        {
            // Calcula o deslocamento inicial da Lua em relação à Terra, normalizado e ajustado pelo raio da órbita
            offset = (transform.position - earth.position).normalized * orbitRadius;
        }
    }

    // Função chamada a cada frame, responsável por atualizar a posição e a rotação da Lua
    void Update()
    {
        if (earth != null) // Verifica se a Terra está definida
        {
            // Mantém a posição da Lua a uma distância fixa (orbitRadius) da Terra
            transform.position = earth.position + offset;

            // Faz a Lua transladar ao redor da Terra, usando a posição da Terra como centro
            // A velocidade da translação é ajustada por orbitSpeed, multiplicada por Time.deltaTime para suavidade
            transform.RotateAround(earth.position, orbitAxis, orbitSpeed * Time.deltaTime);

            // Atualiza o deslocamento com base na nova posição da Lua, para manter o raio fixo
            offset = (transform.position - earth.position).normalized * orbitRadius;
        }

        // Faz a Lua girar em torno de seu próprio eixo (auto-rotação)
        // Vector3.up indica rotação ao redor do eixo Y, com velocidade definida por rotationSpeed
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    // Função pública para alterar dinamicamente o raio da órbita
    // Permite ajustar a distância da Lua à Terra em tempo de execução
    public void SetOrbitRadius(float newRadius)
    {
        // Atualiza o valor do raio da órbita
        orbitRadius = newRadius;

        // Recalcula o deslocamento para refletir o novo raio
        if (earth != null)
        {
            offset = (transform.position - earth.position).normalized * orbitRadius;
        }
    }
}
