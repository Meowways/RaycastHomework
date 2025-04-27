using Unity.VisualScripting;
using UnityEngine;

public class Ship : MonoBehaviour
{
    private const string HorizontalAxis = "Horizontal";

    [SerializeField] Transform _ship;
    [SerializeField] Transform _sail;

    [SerializeField] Transform _windDirection;

    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _sailRotateSpeed;
    [SerializeField] private float _shipRotateSpeed;
    [SerializeField] private float _maxDegrees;

    private Rigidbody _rigidbody;
    private float _currentSpeed;
    private float _currentRotationSail;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float angleBetweenSailAndWind = CalculateAngleBetween(_sail.forward, _windDirection.forward);

        CalculateCurrentSpeed(angleBetweenSailAndWind);

        _rigidbody.MovePosition(transform.localPosition + _ship.transform.forward * _currentSpeed * Time.deltaTime);

        float rotationInput = Input.GetAxisRaw(HorizontalAxis) * _sailRotateSpeed * Time.deltaTime;
        _currentRotationSail = Mathf.Clamp(_currentRotationSail + rotationInput, -_maxDegrees, _maxDegrees);
        _sail.transform.localRotation = Quaternion.Euler(0f, _currentRotationSail, 0f);

        if (_currentSpeed > 0.05)
        {
            if (Input.GetKey(KeyCode.E))
                _ship.transform.Rotate(transform.up * _shipRotateSpeed * Time.deltaTime);

            if (Input.GetKey(KeyCode.Q))
                _ship.transform.Rotate(transform.up * -_shipRotateSpeed * Time.deltaTime);
        }
    }

    private float CalculateAngleBetween(Vector3 vectorA, Vector3 vectorB)
    {
        float dotProduct = Vector3.Dot(vectorA, vectorB);

        float cos = dotProduct / (vectorA.magnitude * vectorB.magnitude);
        cos = Mathf.Clamp(cos, -1f, 1f);

        float angleToTarget = Mathf.Acos(cos) * Mathf.Rad2Deg;

        return Mathf.Abs(angleToTarget);
    }

    private void CalculateCurrentSpeed(float AngleA)
    {
        AngleA = Mathf.Clamp(AngleA, 0, _maxDegrees);
        float AngleB = CalculateAngleBetween(_ship.forward, _windDirection.forward);
        float AngleC = CalculateAngleBetween(_ship.forward, _sail.forward);

        if (AngleB > 89 && AngleC > 89)
        {
            if (_currentSpeed > 0)
                _currentSpeed -= 10 * Time.deltaTime;

            return;
        }

        _currentSpeed = _maxSpeed * (1 - AngleA / _maxDegrees);

        _currentSpeed = Mathf.Clamp(_currentSpeed, 0, _maxSpeed);
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying == false)
            return;

        Gizmos.color = Color.red;
        Ray sailray = new Ray(_sail.transform.position, _sail.transform.forward);
        Gizmos.DrawRay(sailray.origin, sailray.direction * 100);

        Gizmos.color = Color.blue;
        Ray shipray = new Ray(_ship.transform.position, _ship.transform.forward);
        Gizmos.DrawRay(shipray.origin, shipray.direction * 100);
    }
}
