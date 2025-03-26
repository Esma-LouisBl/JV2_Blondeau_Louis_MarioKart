using UnityEngine;
using TMPro;
using System.Collections;
using Unity.VisualScripting;

public class Laps : MonoBehaviour
{
    [SerializeField]
    private GameObject _joueur;
    [SerializeField]
    private TextMeshProUGUI _textLaps, _lastLap;

    private int _laps = 1;
    private bool _checked = false;
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
        if (_joueur != null)
        {
            if (other.CompareTag("Checkpoint"))
            {
                _checked = true;
            }

            if (other.CompareTag("Finish") && _checked)
            {
                _laps++;
                _checked = false;
                StartCoroutine(LapWink());
                if (_laps == 3)
                {
                    StartCoroutine(LastLap());
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
