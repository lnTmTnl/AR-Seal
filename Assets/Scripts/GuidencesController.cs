using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidencesController : MonoBehaviour
{
    public GameObject ARGuidence;
    public GameObject SealGuidence;
    public GameObject MuseumGuidence;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ARGuidenceClose()
    {
        if (ARGuidence.activeSelf)
        {
            ARGuidence.SetActive(false);
        }
    }

    public void SealGuidenceClose()
    {
        if (SealGuidence.activeSelf)
        {
            SealGuidence.SetActive(false);
        }
    }

    public void MuseumGuidenceClose()
    {
        if (MuseumGuidence.activeSelf)
        {
            MuseumGuidence.SetActive(false);
        }
    }
}
