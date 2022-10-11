using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPromptPanelController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject promptPanel;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (PlacementController.prefabIns != null)
        {
            promptPanel.SetActive(false);
        }
    }
}
