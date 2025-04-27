using UnityEngine;

public class Handle : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;

    private Rigidbody _rigidbodyObject;

    private void FixedUpdate()
    {
        if (_rigidbodyObject == null)
            return;

        _rigidbodyObject.transform.position = GetFollowingPosition();
    }

    public void TakeObject(Vector3 origin, Vector3 direction) 
    {
        Physics.Raycast(origin, direction, out RaycastHit hit);

        _rigidbodyObject = hit.collider.gameObject.GetComponent<Rigidbody>();

        if (_rigidbodyObject != null)
            _rigidbodyObject.isKinematic = true;
    }

    public void DropObject()
    {
        if (_rigidbodyObject != null)
        {
            _rigidbodyObject.isKinematic = false;
            _rigidbodyObject = null;
        }
    }

    private Vector3 GetFollowingPosition()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(cameraRay, out RaycastHit ground, Mathf.Infinity, _layerMask.value);
        return new Vector3(ground.point.x, 1, ground.point.z);
    }
}
