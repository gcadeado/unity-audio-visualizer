using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateObjects : MonoBehaviour
{
    public GameObject sampleObjectPrefab;
    public int sampleObjectsNumber = 512;

    public float radius;

    public AudioPeer audioPeer;

    public float sampleMaxSize;
    public float sampleMinScale;
    private GameObject[] _samplesObjects;
    // Start is called before the first frame update

    void Awake()
    {
        _samplesObjects = new GameObject[sampleObjectsNumber];
    }

    void Start()
    {
        for (int i = 0; i < sampleObjectsNumber; i++)
        {
            GameObject _sampleObjectInstance = (GameObject)Instantiate(sampleObjectPrefab);
            _sampleObjectInstance.transform.position = this.transform.position;
            _sampleObjectInstance.transform.parent = this.transform;
            _sampleObjectInstance.name = "Sample Object " + i;
            this.transform.eulerAngles = new Vector3(0f, -0.703125f * i, 0);
            _sampleObjectInstance.transform.position = Vector3.forward * 100;
            _samplesObjects[i] = _sampleObjectInstance;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < sampleObjectsNumber; i++)
        {
            if (_samplesObjects[i] != null)
            {
                // _samplesObjects[i].transform.localScale = new Vector3(10, (audioPeer.samples[i] * sampleMaxSize) + sampleMinScale, 10);
            }
        }
    }
}
