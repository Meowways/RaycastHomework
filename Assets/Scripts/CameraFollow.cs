
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private Vector3 _offset;

    private void Update()
    {
        transform.position = _target.transform.position + _offset;
    }
}
