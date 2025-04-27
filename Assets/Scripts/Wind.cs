using UnityEngine;
using UnityEngine.UI;

public class Wind : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private float _timeToChangeDirection = 5f;

    private float _timer;
    private float _degrees;

    private void Awake()
    {
        _timer = _timeToChangeDirection;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer > 0)
            return;

        _degrees = Random.Range(0, 2) == 0 ? -25 : 25;

        float newAngle = transform.rotation.eulerAngles.y + _degrees;
        transform.rotation = Quaternion.Euler(0f, newAngle, 0f);
        _image.rectTransform.rotation = Quaternion.Euler(0f, 0f, _image.rectTransform.rotation.eulerAngles.z + -_degrees);

        _timer = _timeToChangeDirection;
    }
}
