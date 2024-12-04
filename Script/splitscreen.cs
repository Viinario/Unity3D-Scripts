using UnityEngine;

// Classe que controla o comportamento de câmeras e navegação para split-screen dinâmico
public class SplitScreenManager : MonoBehaviour
{
    public Transform ship1; // Transform da primeira nave
    public Transform ship2; // Transform da segunda nave

    public Camera camera1; // Primeira câmera (principal)
    public Camera camera2; // Segunda câmera (secundária)

    public KeyCode splitScreenKey = KeyCode.F1; // Tecla para ativar o split-screen
    public KeyCode fullScreenKey = KeyCode.F2; // Tecla para voltar ao modo tela cheia

    private bool isSplitScreen = false; // Indica se o modo split-screen está ativo

    void Update()
    {
        if (ship1 != null && ship2 != null && camera1 != null && camera2 != null)
        {
            HandleUserInput(); // Verifica as entradas do usuário para alternar os modos
        }
    }

    void HandleUserInput()
    {
        // Ativa o modo split-screen ao pressionar a tecla configurada
        if (Input.GetKeyDown(splitScreenKey))
        {
            ActivateSplitScreen();
        }

        // Volta ao modo de tela cheia ao pressionar a tecla configurada
        if (Input.GetKeyDown(fullScreenKey))
        {
            DeactivateSplitScreen();
        }
    }

    void ActivateSplitScreen()
    {
        isSplitScreen = true;

        // Configura a câmera principal para a metade esquerda da tela
        camera1.rect = new Rect(0, 0, 0.5f, 1);
        camera1.enabled = true;

        // Configura a câmera secundária para a metade direita da tela
        camera2.rect = new Rect(0.5f, 0, 0.5f, 1);
        camera2.enabled = true;
    }

    void DeactivateSplitScreen()
    {
        isSplitScreen = false;

        // Configura a câmera principal para ocupar toda a tela
        camera1.rect = new Rect(0, 0, 1, 1);
        camera1.enabled = true;

        // Desativa a câmera secundária
        camera2.enabled = false;
    }
}
