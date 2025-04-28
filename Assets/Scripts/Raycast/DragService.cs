using UnityEngine;

public class DragService 
{
    private IGrabble _grabbleObject;

    private Transform _grabbleObjectTransform;
    private LayerMask _layerMask;

    public DragService(LayerMask layerMask)
    {
        _layerMask = layerMask;
    }

    public void TakeObject(Vector3 origin, Vector3 direction) 
    {
        Physics.Raycast(origin, direction, out RaycastHit hit);

        _grabbleObject = hit.collider.gameObject.GetComponent<IGrabble>();

        if (_grabbleObject != null)
        {
            _grabbleObjectTransform = hit.collider.gameObject.transform;
            _grabbleObject.OnGrab();
        }
    }

    public void HoldObject(Vector3 origin, Vector3 direction)
    {
        SetPosition(origin, direction);
    }

    public void DropObject()
    {
        if (_grabbleObject != null)
        {
            _grabbleObject.OnRelease();
            _grabbleObject = null; 
        }
    }

    public bool IsGrabbedObject() => _grabbleObject != null;

    public void SetPosition(Vector3 origin, Vector3 direction) 
    {
        Physics.Raycast(origin, direction, out RaycastHit ground, Mathf.Infinity, _layerMask);
        _grabbleObjectTransform.position = new Vector3(ground.point.x, ground.point.y + 1f, ground.point.z);
    }
}
