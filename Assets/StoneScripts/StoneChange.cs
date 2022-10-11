using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Substance.Game;

public class StoneChange : MonoBehaviour
{
    public Slider slider1;
    public SubstanceGraph mysubstanceGraph;

    private void Awake()
    {
        
    }
    public void changerMat1()
    {
        float value1 = slider1.value;
        mysubstanceGraph.SetInputFloat("change", value1);
        mysubstanceGraph.QueueForRender();
        Substance.Game.Substance.RenderSubstancesAsync();
    }
}
