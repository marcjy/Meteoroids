using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _thrustForce;
    [SerializeField] private int _maxSpeed;


    private float _rotationDirection = 0.0f;
    private bool _isThrusting = false;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InputManager.Instance.OnPlayerRotates += HandlePlayerRotates;
        InputManager.Instance.OnPlayerThrusts += HandlePlayerThrusts;
    }

    private void FixedUpdate()
    {
        if (_isThrusting)
            Thrust();
        ClampVelocity();

        Rotate();
    }

    #region Event Handling
    private void HandlePlayerThrusts(object sender, bool isThrusting) => _isThrusting = isThrusting;
    private void HandlePlayerRotates(object sender, float direction) => _rotationDirection = direction;
    #endregion

    private void Rotate()
    {
        if (_rotationDirection == 0)
            return;

        float rotationValue = _rotationSpeed * Time.fixedDeltaTime;

        if (_rotationDirection < 0)
            rotationValue *= -1;

        _rigidbody.MoveRotation(_rigidbody.rotation + rotationValue);
    }

    private void Thrust()
    {
        _rigidbody.AddForce(transform.up * _thrustForce);
    }
    private void ClampVelocity()
    {
        if (_rigidbody.linearVelocity.magnitude > _maxSpeed)
            _rigidbody.linearVelocity = _rigidbody.linearVelocity.normalized * _maxSpeed;
    }
}