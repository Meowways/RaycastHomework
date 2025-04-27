using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleShoot;

    [SerializeField] private float _attractionForce;
    [SerializeField] private float _radius;

    public void Shoot(Vector3 origin, Vector3 direction)
    {
        Instantiate(_particleShoot, origin, Quaternion.identity);

        if (Physics.Raycast(origin, direction, out RaycastHit hit))
        {
            Collider[] targets = Physics.OverlapSphere(hit.point, _radius);

            foreach (Collider target in targets)
            {
                IMovable movingObject = target.gameObject.GetComponent<IMovable>();

                if (movingObject != null)
                {
                    Vector3 directionForce = (target.transform.position - hit.point).normalized;
                    target.GetComponent<Rigidbody>().AddForce(directionForce * _attractionForce, ForceMode.Impulse);
                }
            }
        }
    }
}
