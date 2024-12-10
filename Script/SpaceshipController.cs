using UnityEngine;
using UnityEngine.UI;

public class SpaceshipController : MonoBehaviour
{
    // Parâmetros gerais da nave
    public float maxSpeed = 100f;
    public float accelerationRate = 25f;
    public float decelerationRate = 15f;
    public float brakeForce = 50f;
    public float turboMultiplier = 2f;
    public float handbrakeMultiplier = 2f;
    public float rotationSpeed = 100f;
    public float verticalSpeed = 5f;
    public float sidewaysSpeed = 25f;

    private float currentSpeed = 0f;

    // Referência ao texto na UI
    public Text speedLabel;

    public KeyCode moveForwardKey = KeyCode.W;
    public KeyCode brakeKey = KeyCode.S;
    public KeyCode turboKey = KeyCode.Space;
    public KeyCode rotateLeftKey = KeyCode.A;
    public KeyCode rotateRightKey = KeyCode.D;
    public KeyCode moveUpKey = KeyCode.LeftShift;
    public KeyCode moveDownKey = KeyCode.LeftControl;

    void Update()
    {
        HandleMovement();
        UpdateSpeedLabel();
        AdjustNearCelestialBodies();
    }

    private void HandleMovement()
    {
        if (Input.GetKey(moveForwardKey))
        {
            float boost = Input.GetKey(turboKey) ? turboMultiplier : 1f;
            currentSpeed += accelerationRate * boost * Time.deltaTime;
        }
        else
        {
            currentSpeed -= decelerationRate * Time.deltaTime;
        }

        if (Input.GetKey(brakeKey))
        {
            float handbrake = Input.GetKey(turboKey) ? handbrakeMultiplier : 1f;
            currentSpeed -= brakeForce * handbrake * Time.deltaTime;
        }

        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

        float rotate = 0f;
        if (Input.GetKey(rotateLeftKey)) rotate = -rotationSpeed * Time.deltaTime;
        if (Input.GetKey(rotateRightKey)) rotate = rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up * rotate);

        float verticalMove = 0f;
        if (Input.GetKey(moveUpKey)) verticalMove = verticalSpeed * Time.deltaTime;
        if (Input.GetKey(moveDownKey)) verticalMove = -verticalSpeed * Time.deltaTime;
        transform.Translate(Vector3.up * verticalMove);

        float sidewaysMove = 0f;
        if (Input.GetKey(KeyCode.A)) sidewaysMove = -sidewaysSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D)) sidewaysMove = sidewaysSpeed * Time.deltaTime;
        transform.Translate(Vector3.right * sidewaysMove);
    }

    private void AdjustNearCelestialBodies()
    {
        foreach (var celestialBody in GameObject.FindGameObjectsWithTag("CelestialBody"))
        {
            GameObject body = celestialBody.transform.Find("Body").gameObject;
            float radius = body.transform.localScale.x / 2;
            float distanceToSurface = Vector3.Distance(transform.position, celestialBody.transform.position) - radius;
            float gInfluence = 1.0f * radius;
            float t = 1 - Mathf.Clamp01(distanceToSurface / gInfluence);

            if (distanceToSurface < gInfluence)
            {
                Vector3 targetUp = (transform.position - celestialBody.transform.position).normalized;
                Vector3 currentUp = Vector3.Lerp(transform.up, targetUp, t);
                transform.LookAt(transform.position + transform.forward, currentUp);
            }
        }
    }

    // Atualiza o texto da velocidade na interface
    private void UpdateSpeedLabel()
    {
        if (speedLabel != null)
        {
            speedLabel.text = $"Speed: {currentSpeed:F1} u/s";
        }
    }
}
