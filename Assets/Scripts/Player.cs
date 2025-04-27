using UnityEngine;

public class Player : MonoBehaviour
{
    private const int LeftMouseButton = 0;
    private const int RightMouseButton = 1;

    [SerializeField] private Handle _handle;
    [SerializeField] private Shooter _shooter;

    private void Update()
    {
        if (Input.GetMouseButtonDown(LeftMouseButton))
        {
            Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            _handle.TakeObject(cameraRay.origin, cameraRay.direction);
        }

        if (Input.GetMouseButtonUp(LeftMouseButton))
            _handle.DropObject();

        if (Input.GetMouseButtonDown(RightMouseButton))
        {
            Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            _shooter.Shoot(cameraRay.origin, cameraRay.direction);
        }
    }
}
