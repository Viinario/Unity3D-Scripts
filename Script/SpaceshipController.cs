using UnityEngine;

// Classe que controla os movimentos básicos de uma nave espacial, com teclas configuráveis
public class SpaceshipController : MonoBehaviour
{
    // Velocidade normal da nave ao mover-se para frente ou para trás
    public float speed = 10f;

    // Multiplicador de velocidade para o modo "boost" (quando o jogador pressiona a tecla de boost)
    public float boostMultiplier = 2f;

    // Velocidade de rotação da nave (em graus por segundo)
    public float rotationSpeed = 100f;

    // Velocidade de movimento vertical (para subir ou descer)
    public float verticalSpeed = 5f;

    // Teclas configuráveis para os movimentos
    public KeyCode moveForwardKey = KeyCode.W;
    public KeyCode moveBackwardKey = KeyCode.S;
    public KeyCode rotateLeftKey = KeyCode.A;
    public KeyCode rotateRightKey = KeyCode.D;
    public KeyCode moveUpKey = KeyCode.LeftShift;
    public KeyCode moveDownKey = KeyCode.LeftControl;
    public KeyCode boostKey = KeyCode.Space;

    // Função chamada a cada frame, usada para atualizar a lógica de movimento da nave
    void Update()
    {
        // Inicializa a velocidade atual da nave com o valor padrão
        float currentSpeed = speed;

        // Verifica se a tecla de boost está pressionada para aplicar o boost de velocidade
        if (Input.GetKey(boostKey))
        {
            currentSpeed *= boostMultiplier; // Multiplica a velocidade normal pelo fator de boost
        }

        // Movimentos para frente e para trás
        float move = 0f;
        if (Input.GetKey(moveForwardKey)) // Se a tecla de mover para frente está pressionada
        {
            move = currentSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(moveBackwardKey)) // Se a tecla de mover para trás está pressionada
        {
            move = -currentSpeed * Time.deltaTime;
        }

        // Rotação para esquerda e direita
        float rotate = 0f;
        if (Input.GetKey(rotateLeftKey)) // Se a tecla de girar para a esquerda está pressionada
        {
            rotate = -rotationSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(rotateRightKey)) // Se a tecla de girar para a direita está pressionada
        {
            rotate = rotationSpeed * Time.deltaTime;
        }

        // Movimentos verticais (subir e descer)
        float verticalMove = 0f;
        if (Input.GetKey(moveUpKey)) // Se a tecla de subir está pressionada
        {
            verticalMove = verticalSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(moveDownKey)) // Se a tecla de descer está pressionada
        {
            verticalMove = -verticalSpeed * Time.deltaTime;
        }

        // Aplica o movimento para frente ou para trás, em relação ao eixo Z local da nave
        transform.Translate(Vector3.forward * move);

        // Aplica a rotação da nave ao redor do eixo Y local (gira para a esquerda ou direita)
        transform.Rotate(Vector3.up * rotate);

        // Aplica o movimento vertical, em relação ao eixo Y local da nave
        transform.Translate(Vector3.up * verticalMove);
    }
}
