using UnityEngine;

public class Ship : MonoBehaviour
{
    private const string HorizontalAxis = "Horizontal";
    private const float MinimumTurningSpeed = 0.05f;

    [SerializeField] Transform _shipDirection;
    [SerializeField] Transform _sailDirection;

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
        CalculateCurrentSpeed();

        _rigidbody.MovePosition(transform.localPosition + _shipDirection.transform.forward * _currentSpeed * Time.deltaTime);

        float rotationInput = Input.GetAxisRaw(HorizontalAxis) * _sailRotateSpeed * Time.deltaTime;
        _currentRotationSail = Mathf.Clamp(_currentRotationSail + rotationInput, -_maxDegrees, _maxDegrees);

        _sailDirection.transform.localRotation = Quaternion.Euler(0f, _currentRotationSail, 0f);

        if (_currentSpeed > MinimumTurningSpeed)
        {
            if (Input.GetKey(KeyCode.E))
                _shipDirection.transform.Rotate(transform.up * _shipRotateSpeed * Time.deltaTime);

            if (Input.GetKey(KeyCode.Q))
                _shipDirection.transform.Rotate(transform.up * -_shipRotateSpeed * Time.deltaTime);
        }
    }

    private void CalculateCurrentSpeed()
    {
        _currentSpeed = DotProductBetween(_sailDirection.forward, _shipDirection.forward) * DotProductBetween(_sailDirection.forward, _windDirection.forward) * _maxSpeed;
    }

    private float DotProductBetween(Vector3 vectorA, Vector3 vectorB)
    {
        float dotProduct = Vector3.Dot(vectorA, vectorB);

        if (dotProduct < 0)
            return 0f;

        return dotProduct;
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying == false)
            return;

        Gizmos.color = Color.red;
        Ray sailray = new Ray(_sailDirection.transform.position, _sailDirection.transform.forward);
        Gizmos.DrawRay(sailray.origin, sailray.direction * 100);

        Gizmos.color = Color.green;
        Ray shipray = new Ray(_shipDirection.transform.position, _shipDirection.transform.forward);
        Gizmos.DrawRay(shipray.origin, shipray.direction * 100);

        Gizmos.color = Color.blue;
        Ray windray = new Ray(_windDirection.transform.position, _windDirection.transform.forward);
        Gizmos.DrawRay(windray.origin, windray.direction * 100);
    }
}
