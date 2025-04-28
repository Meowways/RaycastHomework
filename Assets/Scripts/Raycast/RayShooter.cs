using UnityEngine;

public class RayShooter : IShoot
{
    private IShootEffect _ShootEffect;
    private ParticleSystem _particleShoot;

    public RayShooter(IShootEffect movableEffect, ParticleSystem particleShoot)
    {
        _ShootEffect = movableEffect;
        _particleShoot = particleShoot;
    }

    public void Shoot(Vector3 origin, Vector3 direction)
    {
        if (Physics.Raycast(origin, direction, out RaycastHit hit))
        {
            Object.Instantiate(_particleShoot, hit.point, Quaternion.identity);

            _ShootEffect.Execute(hit.point, hit.collider);
        }
    }
}
