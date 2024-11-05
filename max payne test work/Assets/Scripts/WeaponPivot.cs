using UnityEngine;

public class WeaponPivot : MonoBehaviour
{
    public float _angle { get; private set; }
    private Weapon _weapon;
    private Vector2 _direction;
    Vector3 _mousePosition;

    private void Start()
    {
        _weapon = GetComponentInChildren<Weapon>();
    }

    private void Update()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _direction = _mousePosition - transform.position;
        _angle = Vector2.SignedAngle(Vector2.right, _direction);
        transform.eulerAngles = new Vector3(0,0,_angle);
        _weapon.GetComponent<SpriteRenderer>().flipY = _weapon.transform.position.x < transform.position.x ? true : false;
    }
}
