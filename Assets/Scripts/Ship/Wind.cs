using UnityEngine;
using UnityEngine.UI;

public class Wind : MonoBehaviour
{
    [SerializeField] private Image _pointerImage;
    [SerializeField] private float _timeToChangeDirection;

    private float _timer;

    private float _currentRotation;
    private float _targetRotation;

    private float _rotateSpeed = 125f;

    private void Awake()
    {
        _timer = _timeToChangeDirection;
        _currentRotation = transform.rotation.eulerAngles.y;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;

        if (_currentRotation != _targetRotation)
            if (_currentRotation < _targetRotation)
            {
                transform.rotation = Quaternion.Euler(0f, _currentRotation + _rotateSpeed * Time.deltaTime, 0f);
                _pointerImage.rectTransform.rotation = Quaternion.Euler(0f, 0f, _pointerImage.rectTransform.rotation.eulerAngles.z - _rotateSpeed * Time.deltaTime);
            }

            if (_currentRotation > _targetRotation) 
            {
                transform.rotation = Quaternion.Euler(0f, _currentRotation - _rotateSpeed * Time.deltaTime, 0f);
                _pointerImage.rectTransform.rotation = Quaternion.Euler(0f, 0f, _pointerImage.rectTransform.rotation.eulerAngles.z + _rotateSpeed * Time.deltaTime);
            }

        _currentRotation = transform.rotation.eulerAngles.y;

        if (_timer > 0)
            return;

        _targetRotation = Random.Range(0, 360);
        _timer = _timeToChangeDirection;
    }
}
