using UnityEngine;
using TMPro;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class Laps : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private TextMeshProUGUI _textLaps, _lastLap;

    private int _laps = 1;
    private int _checkpoints = 0;
    void Start()
    {
        _lastLap.enabled = false;
        _textLaps.text = _laps + "/3";
    }

    void Update()
    {
        _textLaps.text = _laps + "/3";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_player != null)
        {
            if (other.CompareTag("Checkpoint"))
            {
                _checkpoints ++;
            }

            if (other.CompareTag("Finish") && _checkpoints>20)
            {
                _laps++;
                _checkpoints = 0;
                StartCoroutine(LapWink());
                if (_laps == 3)
                {
                    StartCoroutine(LastLap());
                }
                if (_laps == 4)
                {
                    SceneManager.LoadScene("Win");
                }
            }
        }
    }

    IEnumerator LastLap()
    {
        for (int i = 0; i < 5; i++)
        {
            _lastLap.enabled = true;
            yield return new WaitForSeconds(0.5f);
            _lastLap.enabled = false;
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator LapWink()
    {
        for (int i = 0; i < 5; i++)
        {
            _textLaps.enabled = false;
            yield return new WaitForSeconds(0.3f);
            _textLaps.enabled = true;
            yield return new WaitForSeconds(0.3f);
        }
    }
}
