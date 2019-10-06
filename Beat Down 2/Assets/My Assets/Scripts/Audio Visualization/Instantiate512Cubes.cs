using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate512Cubes : MonoBehaviour
{

    public GameObject _sampleCubePrefab;
    private GameObject[] _sampleCube = new GameObject[512];
    private GameObject[] _bandCube = new GameObject[8];
    public AudioPeer peer;
    public AudioPeer peer2;
    public float _maxScale;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            GameObject _instanceOfSampleCube = (GameObject)Instantiate(_sampleCubePrefab);
            _instanceOfSampleCube.transform.SetParent(transform);
            _instanceOfSampleCube.transform.localPosition = new Vector3((4 * i) - (4 * 4), 0, 0);
            _instanceOfSampleCube.name = "Band Cube " + i;
            _bandCube[i] = _instanceOfSampleCube;
        }

        for (int i = 0; i < 512; i++)
        {
            GameObject _instanceOfSampleCube = (GameObject)Instantiate(_sampleCubePrefab);
            _instanceOfSampleCube.transform.position = transform.position;
            _instanceOfSampleCube.transform.SetParent(transform);
            _instanceOfSampleCube.name = "SampleCube " + i;
            transform.eulerAngles = new Vector3(0, 0 - (360/512f) * i, 0);
            _instanceOfSampleCube.transform.position = Vector3.forward * 10;
            _sampleCube[i] = _instanceOfSampleCube;
        }

        Debug.Log("Goodbye World");


    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < 512; i++)
        {
            if(_sampleCube != null)
            {
                _sampleCube[i].transform.localScale = new Vector3(1, 1 , ((peer._samples[i] + peer2._samples[i]) * _maxScale) + 2);
                //_sampleCube[i].transform.localScale = new Vector3(1, 1, ((peer._freqBand[i]) * _maxScale) + 2);
            }
        }

        for (int i = 0; i < 8; i++)
        {
            if (_bandCube != null)
            {
                //_bandCube[i].transform.localScale = new Vector3(1, 1, ((peer._samples[i] + peer2._samples[i]) * _maxScale) + 2);
                _bandCube[i].transform.localScale = new Vector3(1, 1, ((peer._bandBuffer[i]) * 4) + 2);
            }
        }
    }
}
