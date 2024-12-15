using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SpaceshipSoundController : MonoBehaviour
{
    public SpaceshipController spaceshipController; // Referência ao script SpaceshipController
    private AudioSource engineAudio; // Componente de áudio

    [System.Serializable]
    public class SpeedAudio
    {
        public float minSpeed; // Velocidade mínima para este som
        public float maxSpeed; // Velocidade máxima para este som
        public AudioClip audioClip; // Som correspondente
        public bool loop; // Define se o som será em loop
    }

    public SpeedAudio[] speedAudios; // Lista de áudios baseados na velocidade

    private float currentSpeed = 0f;
    private AudioClip currentClip = null; // Som atualmente tocando

    void Start()
    {
        // Obtém o componente AudioSource
        engineAudio = GetComponent<AudioSource>();

        if (spaceshipController == null)
        {
            Debug.LogError("SpaceshipController não atribuído ao SpaceshipSoundController.");
        }
    }

    void Update()
    {
        if (spaceshipController != null && engineAudio != null)
        {
            currentSpeed = spaceshipController.CurrentSpeed;
            UpdateAudioBasedOnSpeed();
        }
    }

    private void UpdateAudioBasedOnSpeed()
    {
        foreach (var speedAudio in speedAudios)
        {
            if (currentSpeed >= speedAudio.minSpeed && currentSpeed <= speedAudio.maxSpeed)
            {
                if (engineAudio.clip != speedAudio.audioClip)
                {
                    engineAudio.clip = speedAudio.audioClip;
                    engineAudio.loop = speedAudio.loop;
                    engineAudio.Play();
                    currentClip = speedAudio.audioClip;
                }
                return;
            }
        }

        // Se não houver áudio correspondente, parar o som
        if (engineAudio.clip != null)
        {
            engineAudio.Stop();
            engineAudio.clip = null;
            currentClip = null;
        }
    }
}
