using UnityEngine;

public class Box : MonoBehaviour, IGrabble, IMovable
{
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void OnGrab()
    {
        _rigidbody.isKinematic = true;
    }

    public void OnRelease()
    {
        _rigidbody.isKinematic = false;
    }

    public void ProcessingIncomingForce(float incomingForce, Vector3 directionForce)
    {
        _rigidbody.AddForce(directionForce * incomingForce, ForceMode.Impulse);
    }
}
