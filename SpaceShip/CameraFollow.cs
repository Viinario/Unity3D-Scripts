using UnityEngine;

// Classe que controla o comportamento de uma câmera que segue e gira junto com um alvo (como uma nave)
public class CameraFollowAndRotate : MonoBehaviour
{
    // Objeto que será seguido pela câmera (como uma nave ou jogador)
    public Transform target; 
    
    // Vetor que define o deslocamento fixo da câmera em relação ao alvo
    public Vector3 offset;

    // Velocidade de suavização para o movimento e rotação da câmera, quanto menor, mais suave será
    public float smoothSpeed = 0.125f;

    // LateUpdate é chamado depois de todos os cálculos de física no frame, ideal para movimentos de câmera
    void LateUpdate()
    {
        // Verifica se há um alvo definido para evitar erros de referência nula
        if (target != null)
        {
            // Calcula a posição desejada da câmera com base na posição do alvo e o deslocamento especificado
            Vector3 desiredPosition = target.position + target.TransformDirection(offset);
            
            // Suavemente interpola (Lerp) a posição atual da câmera para a posição desejada
            // Isso cria um movimento fluido e evita saltos bruscos
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Suavemente interpola (Lerp) a rotação atual da câmera para a rotação do alvo
            // Isso faz com que a câmera acompanhe as rotações do alvo de forma fluida
            transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, smoothSpeed);
        }
    }
}
