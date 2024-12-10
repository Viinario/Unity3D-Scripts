using System.Collections;
using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    [System.Serializable]
    public class SkyboxKeyMapping
    {
        public KeyCode key;        // Tecla para ativar o skybox temporário
        public Material skybox;    // Material do skybox temporário
    }

    public SkyboxKeyMapping[] skyboxMappings; // Lista de teclas e skyboxes
    public Material defaultSkybox;            // Skybox padrão
    public float duration = 3f;               // Duração em segundos do skybox temporário

    private bool isChanging = false;          // Impede múltiplas mudanças simultâneas

    void Update()
    {
        if (!isChanging)
        {
            foreach (var mapping in skyboxMappings)
            {
                if (Input.GetKeyDown(mapping.key))
                {
                    StartCoroutine(ChangeSkyboxTemporarily(mapping.skybox));
                }
            }
        }
    }

    IEnumerator ChangeSkyboxTemporarily(Material temporarySkybox)
    {
        isChanging = true;

        if (temporarySkybox != null)
        {
            RenderSettings.skybox = temporarySkybox;
            DynamicGI.UpdateEnvironment(); // Atualiza o ambiente global
        }
        else
        {
            Debug.LogWarning("Skybox temporário não definido!");
        }

        yield return new WaitForSeconds(duration);

        // Retorna ao skybox padrão
        RenderSettings.skybox = defaultSkybox;
        DynamicGI.UpdateEnvironment(); // Atualiza o ambiente global

        isChanging = false;
    }
}
