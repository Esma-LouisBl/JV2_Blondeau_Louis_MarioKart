using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CarControler : MonoBehaviour
{

    [SerializeField]
    private LayerMask _layerMask;
    [SerializeField]
    private Rigidbody _rb;

    private float _speed, _accelerationLerpInterpolator, _rotationInput; 
    [SerializeField]
    private float _speedMaxBasic = 3, _speedMaxTurbo = 10, _accelerationFactor, _rotationSpeed = 0.5f;
    private bool _isAccelerating, _isTurbo;
    private float _terrainSpeedVariator;

    [SerializeField]
    private AnimationCurve _accelerationCurve;

    [SerializeField]
    private string _horizontalControl;

    [SerializeField]
    private KeyCode _accelerationControl;

    [SerializeField]
    private Image _inkSplash;

    private void Start()
    {
        _inkSplash.enabled = false;
    }

    public void Turbo()
    {
        if (!_isTurbo)
        {
            StartCoroutine(Turboroutine());
        }
    }

    private IEnumerator Turboroutine()
    {
        _isTurbo = true;
        yield return new WaitForSeconds(3);
        _isTurbo = false;
    }

    public void Splash()
    {
        StartCoroutine(SplashCoroutine());
    }
    private IEnumerator SplashCoroutine()
    {
        _inkSplash.enabled = true;
        yield return new WaitForSeconds(5);
        _inkSplash.enabled = false;
    }


    void Update()
    {


        _rotationInput = Input.GetAxis(_horizontalControl);

        if (Input.GetKeyDown(_accelerationControl))
        {
            _isAccelerating = true;
        }
        if (Input.GetKeyUp(_accelerationControl))
        {
            _isAccelerating = false;
        }
    }

    private void FixedUpdate()
    {
        
        if (_isAccelerating)
        {
            _accelerationLerpInterpolator += _accelerationFactor;
        }
        else
        {
            _accelerationLerpInterpolator -= _accelerationFactor*2;
        }

        _accelerationLerpInterpolator = Mathf.Clamp01(_accelerationLerpInterpolator);
        

        if(_isTurbo)
        {
            _speed = _speedMaxTurbo;
        }
        else
        {
            _speed = _accelerationCurve.Evaluate(_accelerationLerpInterpolator)*_speedMaxBasic*_terrainSpeedVariator;
        }

        transform.eulerAngles += Vector3.up * _rotationSpeed * Time.deltaTime*_rotationInput;
        _rb.MovePosition(transform.position+transform.forward*_speed*Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rough"))
        {
            _terrainSpeedVariator = 0.5f;
        }
        if (collision.gameObject.CompareTag("Road"))
        {
            _terrainSpeedVariator = 1;
        }
        if (collision.gameObject.CompareTag("PatchTurbo"))
        {
            Turbo();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rough"))
        {
            _terrainSpeedVariator = 1f;
        }
    }
}
