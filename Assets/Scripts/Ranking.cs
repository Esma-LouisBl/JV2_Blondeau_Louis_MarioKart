using UnityEngine;
using TMPro;
using Unity.VisualScripting;
public class Ranking : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _rankP1, _rankP2;

    [SerializeField]
    private Laps _p1, _p2;

    private int _lapsP1, _lapsP2, _checkpointsP1, _checkpointsP2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _lapsP1 = _p1.laps;
        _lapsP2 = _p2.laps;
        _checkpointsP1 = _p1.checkpoints;
        _checkpointsP2 = _p2.checkpoints;

        _rankP1.text = "1st";
        _rankP2.text = "2nd";
    }

    // Update is called once per frame
    void Update()
    {
        _lapsP1 = _p1.laps;
        _lapsP2 = _p2.laps;
        _checkpointsP1 = _p1.checkpoints;
        _checkpointsP2 = _p2.checkpoints;
        if (_lapsP1 > _lapsP2)
        {
            _rankP1.text = "1st";
            _rankP2.text = "2nd";
        }
        if (_lapsP2 > _lapsP1)
        {
            _rankP1.text = "2nd";
            _rankP2.text = "1st";
        }
        if (_lapsP1 == _lapsP2)
        {
            if (_checkpointsP1 >= _checkpointsP2)
            {
                _rankP1.text = "1st";
                _rankP2.text = "2nd";
            }
            if (_checkpointsP1 < _checkpointsP2)
            {
                _rankP1.text = "2nd";
                _rankP2.text = "1st";
            }
        }
    }
}
