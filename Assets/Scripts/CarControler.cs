using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CarControler : MonoBehaviour
{

    [SerializeField]
    private Rigidbody _rb;

    private float _speed, _accelerationLerpInterpolator, _rotationInput; 
    [SerializeField]
    private float _speedMaxBasic = 3, _speedMaxTurbo = 10, _accelerationFactor, _rotationSpeed = 0.5f;
    public float speedThunder = 1;
    private bool _isAccelerating, _isTurbo, _isStopped;
    private float _terrainSpeedVariator;

    [SerializeField]
    private AnimationCurve _accelerationCurve;

    [SerializeField]
    private string _horizontalControl;

    [SerializeField]
    private KeyCode _accelerationControl;

    [SerializeField]
    private Image _inkSplash;

    [SerializeField]
    private CarControler _opponentControler;
    [SerializeField]
    private Transform _opponentTransform;

    private void Start()
    {
        _inkSplash.enabled = false;
        _isStopped = false;
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

    public void Thunder()
    {
        StartCoroutine(ThunderCoroutine());
    }

    private IEnumerator ThunderCoroutine()
    {
        _opponentControler.speedThunder = 0.5f;
        _opponentTransform.localScale = _opponentTransform.localScale / 2;
        yield return new WaitForSeconds(8);
        _opponentControler.speedThunder = 1;
        _opponentTransform.localScale = _opponentTransform.localScale * 2;
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
            _speed = _speedMaxTurbo * speedThunder;
        }
        if (_isStopped)
        {
            _speed = 0;
        }
        else
        {
            _speed = _accelerationCurve.Evaluate(_accelerationLerpInterpolator)*_speedMaxBasic*_terrainSpeedVariator*speedThunder;
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

        if (collision.gameObject.CompareTag("Shell"))
        {
            Destroy(collision.gameObject);
            StartCoroutine(Stop(2));
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rough"))
        {
            _terrainSpeedVariator = 1f;
        }
    }

    public IEnumerator Stop(float time)
    {
        _isStopped = true;
        yield return new WaitForSeconds(time);
        _isStopped = false;
    }
}
