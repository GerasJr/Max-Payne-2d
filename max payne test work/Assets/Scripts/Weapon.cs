using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;

    private WeaponPivot _pivot;
    private WeaponMuzzle _muzzle;

    private void Start()
    {
        _pivot = GetComponentInParent<WeaponPivot>();
        _muzzle = GetComponentInChildren<WeaponMuzzle>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Bullet bullet = Instantiate(_bullet, _muzzle.transform.position, Quaternion.identity);
            bullet.transform.rotation = Quaternion.Euler(0f, 0f, _pivot._angle - 90);
        }
    }
}
