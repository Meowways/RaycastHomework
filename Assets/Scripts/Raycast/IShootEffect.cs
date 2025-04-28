using UnityEngine;

public interface IShootEffect
{
    void Execute(Vector3 point, Collider collider);
}
