using UnityEngine;

public class Player : MonoBehaviour
{
    private const int LeftMouseButton = 0;
    private const int RightMouseButton = 1;

    [SerializeField] private LayerMask _groundLayerMask;

    private DragService _dragService;
    private IShoot _shooter;

    private void Awake()
    {
        _dragService = new DragService(_groundLayerMask);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(LeftMouseButton))
        {
            Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            _dragService.TakeObject(cameraRay.origin, cameraRay.direction);
        }

        if (_dragService.IsGrabbedObject())
        {
            Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            _dragService.HoldObject(cameraRay.origin, cameraRay.direction);
        }

        if (Input.GetMouseButtonUp(LeftMouseButton))
            _dragService.DropObject();

        if (Input.GetMouseButtonDown(RightMouseButton))
        {
            Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            _shooter.Shoot(cameraRay.origin, cameraRay.direction);
        }
    }

    public void SetShooter(IShoot shooter) => _shooter = shooter;
}
