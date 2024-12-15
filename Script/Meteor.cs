using UnityEngine;

public class MeteorShowerManager : MonoBehaviour
{
    public GameObject meteorPrefab; // Prefab do meteoro
    public Transform spaceship; // Referência à nave

    public float spawnInterval = 5f; // Intervalo entre chuvas de meteoros
    public int meteorsPerShower = 10; // Número de meteoros por chuva
    public float spawnRadius = 50f; // Raio de spawn dos meteoros
    public float meteorSpeed = 20f; // Velocidade dos meteoros
    public float meteorLifetime = 10f; // Tempo de vida dos meteoros

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnMeteorShower();
            timer = 0f;
        }
    }

    void SpawnMeteorShower()
    {
        for (int i = 0; i < meteorsPerShower; i++)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition();
            GameObject meteor = Instantiate(meteorPrefab, spawnPosition, Quaternion.identity);

            Vector3 directionToSpaceship = (spaceship.position - spawnPosition).normalized;
            meteor.GetComponent<Rigidbody>().linearVelocity = directionToSpaceship * meteorSpeed;

            Destroy(meteor, meteorLifetime);
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        // Gera uma posição aleatória ao redor da nave
        Vector2 randomCircle = Random.insideUnitCircle.normalized * spawnRadius;
        Vector3 spawnPosition = new Vector3(randomCircle.x, Random.Range(-spawnRadius, spawnRadius), randomCircle.y);

        return spaceship.position + spawnPosition;
    }
}
