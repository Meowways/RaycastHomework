using UnityEngine;

public class SwitchShooter : MonoBehaviour
{
    private const KeyCode WeaponSlot1 = KeyCode.Alpha1;

    [SerializeField] private Player _player;

    [SerializeField] private float _repulsiveForce;
    [SerializeField] private float _radius;

    [SerializeField] private ParticleSystem _repulsiveShootEffect;

    private void Awake()
    {
        _player.SetShooter(new RayShooter(new RepulsiveShootEffect(_radius, _repulsiveForce), _repulsiveShootEffect));
    }

    private void Update()
    {
        if (Input.GetKeyUp(WeaponSlot1))
            _player.SetShooter(new RayShooter(new RepulsiveShootEffect(_radius, _repulsiveForce), _repulsiveShootEffect));
    }
}
