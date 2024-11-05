using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _maxSpeed = 3.4f;
    [SerializeField] private float _jumpHeight = 6.5f;
    [SerializeField] private float _jumpRange = 1;
    [SerializeField] private float _gravityScale = 1.5f;
    [SerializeField] private Camera _mainCamera;

    private bool _facingRight = true;
    private bool _isGrounded = false;
    private float _moveDirection = 0;
    private Vector3 _cameraPos;
    private Rigidbody2D _rigidbody;
    private CapsuleCollider2D _collider;
    private Animator _animator;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CapsuleCollider2D>();
        _animator = GetComponent<Animator>();
        _rigidbody.freezeRotation = true;
        _rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        _rigidbody.gravityScale = _gravityScale;
        _facingRight = transform.localScale.x > 0;

        if (_mainCamera)
        {
            _cameraPos = _mainCamera.transform.position;
        }
    }

    void Update()
    {
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && _isGrounded)
        {
            _moveDirection = Input.GetKey(KeyCode.A) ? -1 : 1;
        }
        else
        {
            if (_isGrounded || _rigidbody.linearVelocity.magnitude < 0.01f)
            {
                _moveDirection = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded == true)
        {
            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, _jumpHeight);
        }

        if (_mainCamera)
        {
            _mainCamera.transform.position = new Vector3(transform.position.x, _cameraPos.y, _cameraPos.z);
        }
    }

    void FixedUpdate()
    {
        Bounds colliderBounds = _collider.bounds;
        float colliderRadius = _collider.size.x * 0.4f * Mathf.Abs(transform.localScale.x);
        Vector3 groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, colliderRadius * 0.9f, 0);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPos, colliderRadius);
        _isGrounded = false;

        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != _collider)
                {
                    _isGrounded = true;
                    break;
                }
            }
        }

        _animator.SetBool("IsGrounded", _isGrounded);
        _animator.SetFloat("Direction", _moveDirection);

        if (_isGrounded == true)
        {
            _rigidbody.linearVelocity = new Vector2((_moveDirection) * _maxSpeed, _rigidbody.linearVelocity.y);
        }
        else
        {
            _rigidbody.linearVelocity = new Vector2((_moveDirection) * _jumpRange, _rigidbody.linearVelocity.y);
        }
    }
}
