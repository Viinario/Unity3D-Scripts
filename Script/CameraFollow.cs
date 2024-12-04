using UnityEngine;

// Classe que controla o comportamento de uma câmera que segue e gira junto com um alvo
// Adiciona modos de câmera configuráveis
public class CameraFollowAndRotate : MonoBehaviour
{
    public Transform target; // Objeto que será seguido pela câmera

    // Configurações do Modo 1
    public Vector3 mode1Offset = new Vector3(0, 5, -10); // Deslocamento da câmera no modo 1
    public float mode1FOV = 60f; // Campo de visão da câmera no modo 1
    public float mode1FogStart = 20f; // Início do fog no modo 1
    public float mode1FogEnd = 100f; // Fim do fog no modo 1

    // Configurações do Modo 2
    public Vector3 mode2Offset = new Vector3(0, 10, -20); // Deslocamento da câmera no modo 2
    public float mode2FOV = 40f; // Campo de visão da câmera no modo 2
    public float mode2FogStart = 50f; // Início do fog no modo 2
    public float mode2FogEnd = 200f; // Fim do fog no modo 2

    // Teclas para alternar entre os modos
    public KeyCode mode1Key = KeyCode.Alpha1; // Tecla para ativar o modo 1
    public KeyCode mode2Key = KeyCode.Alpha2; // Tecla para ativar o modo 2

    public float smoothSpeed = 0.125f; // Velocidade de suavização para movimento e rotação

    private Vector3 currentOffset; // Deslocamento atual da câmera
    private float currentFOV; // FOV atual da câmera
    private Camera cameraComponent; // Componente de câmera para ajustar o FOV

    private void Start()
    {
        cameraComponent = GetComponent<Camera>(); // Obtém o componente de câmera
        SwitchToMode1(); // Define o modo inicial como o modo 1
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Calcula a posição desejada da câmera com base no deslocamento atual
            Vector3 desiredPosition = target.position + target.TransformDirection(currentOffset);

            // Suaviza o movimento e rotação da câmera
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, smoothSpeed);
        }

        HandleCameraModeSwitch(); // Verifica as entradas do jogador para alternar o modo
    }

    void HandleCameraModeSwitch()
    {
        // Alterna para o modo 1 ao pressionar a tecla configurada para o modo 1
        if (Input.GetKeyDown(mode1Key))
        {
            SwitchToMode1();
        }

        // Alterna para o modo 2 ao pressionar a tecla configurada para o modo 2
        if (Input.GetKeyDown(mode2Key))
        {
            SwitchToMode2();
        }
    }

    void SwitchToMode1()
    {
        // Atualiza as configurações da câmera para o modo 1
        currentOffset = mode1Offset;
        currentFOV = mode1FOV;
        cameraComponent.fieldOfView = currentFOV;

        // Atualiza os valores de fog para o modo 1
        RenderSettings.fogStartDistance = mode1FogStart;
        RenderSettings.fogEndDistance = mode1FogEnd;
    }

    void SwitchToMode2()
    {
        // Atualiza as configurações da câmera para o modo 2
        currentOffset = mode2Offset;
        currentFOV = mode2FOV;
        cameraComponent.fieldOfView = currentFOV;

        // Atualiza os valores de fog para o modo 2
        RenderSettings.fogStartDistance = mode2FogStart;
        RenderSettings.fogEndDistance = mode2FogEnd;
    }
}
