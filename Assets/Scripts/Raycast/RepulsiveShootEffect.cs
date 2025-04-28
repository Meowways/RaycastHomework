using UnityEngine;

public class RepulsiveShootEffect : IShootEffect
{
    private float _radius;
    private float _repulsiveForce;

    public RepulsiveShootEffect(float radius, float repulsiveForce)
    {
        _radius = radius;
        _repulsiveForce = repulsiveForce;
    }

    public void Execute(Vector3 point, Collider collider)
    {
        Collider[] targets = Physics.OverlapSphere(point, _radius);

        foreach (Collider target in targets)
        {
            IMovable movableObject = target.gameObject.GetComponent<IMovable>();

            if (movableObject != null)
            {
                Vector3 directionForce = (target.transform.position - point).normalized;
                movableObject.ProcessingIncomingForce(_repulsiveForce, directionForce);
            }
        }
    }
}
