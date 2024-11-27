using UnityEngine;

// Classe que controla os movimentos básicos de uma nave espacial, incluindo movimentos para frente, rotação e deslocamento vertical
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

    // Função chamada a cada frame, usada para atualizar a lógica de movimento da nave
    void Update()
    {
        // Inicializa a velocidade atual da nave com o valor padrão
        float currentSpeed = speed;

        // Verifica se a tecla de espaço está pressionada para aplicar o boost de velocidade
        if (Input.GetKey(KeyCode.Space))
        {
            currentSpeed *= boostMultiplier; // Multiplica a velocidade normal pelo fator de boost
        }

        // Captura o input para movimento para frente ou para trás (teclas W e S)
        // Multiplica pela velocidade atual e Time.deltaTime para suavizar o movimento e torná-lo independente da taxa de quadros
        float move = Input.GetAxis("Vertical") * currentSpeed * Time.deltaTime;

        // Captura o input para rotação horizontal da nave (teclas A e D)
        // Multiplica pela velocidade de rotação e Time.deltaTime
        float rotate = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;

        // Inicializa o movimento vertical com valor neutro (sem deslocamento vertical)
        float verticalMove = 0f;

        // Verifica se as teclas Shift (esquerda ou direita) estão pressionadas para mover a nave para cima
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            verticalMove = verticalSpeed * Time.deltaTime; // Define movimento vertical positivo (subida)
        }
        // Verifica se as teclas Ctrl (esquerda ou direita) estão pressionadas para mover a nave para baixo
        else if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            verticalMove = -verticalSpeed * Time.deltaTime; // Define movimento vertical negativo (descida)
        }

        // Aplica o movimento para frente ou para trás, em relação ao eixo Z local da nave
        transform.Translate(Vector3.forward * move);

        // Aplica a rotação da nave ao redor do eixo Y local (gira para a esquerda ou direita)
        transform.Rotate(Vector3.up * rotate);

        // Aplica o movimento vertical, em relação ao eixo Y local da nave
        transform.Translate(Vector3.up * verticalMove);
    }
}
