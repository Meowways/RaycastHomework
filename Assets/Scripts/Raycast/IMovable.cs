using UnityEngine;

public interface IMovable 
{
    void ProcessingIncomingForce(float IncomingForce, Vector3 directionForce);
}
