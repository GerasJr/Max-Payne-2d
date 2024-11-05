using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _speed = 100f;
    private Rigidbody2D _rigidbody;
    private UnityEngine.Camera camera;

    private void Start()
    {
        camera = UnityEngine.Camera.main;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector3 viewPos = camera.WorldToViewportPoint(transform.position);

        if (!(viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0))
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.linearVelocity = transform.up * _speed;
    }
}
