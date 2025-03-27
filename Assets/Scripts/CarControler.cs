using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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
    void Update()
    {


        _rotationInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isAccelerating = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _isAccelerating = false;
        }

        if (Physics.Raycast(transform.position, transform.up * -1, out var info, 1, _layerMask))
        {
            Terrain terrainBellow = info.transform.GetComponent<Terrain>();
            if (terrainBellow != null)
            {
                _terrainSpeedVariator = terrainBellow.speedVariator;
            }
            else
            {
                _terrainSpeedVariator = 1;
            }
        }
        else
        {
            _terrainSpeedVariator = 1;
        }

        //var xAngle = transform.eulerAngles.x;
        //if (xAngle>180)
        //{
        //    xAngle = Mathf.Clamp(transform.eulerAngles.x, 0, 40);
        //    xAngle -= 360;
        //}

        //var yAngle = transform.eulerAngles.y;
        //var zAngle = 0;
        //transform.eulerAngles = new Vector3(xAngle,yAngle,zAngle);
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
}
