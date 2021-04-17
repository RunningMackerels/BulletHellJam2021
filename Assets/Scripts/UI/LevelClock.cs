using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelClock : MonoBehaviour
{

    [FormerlySerializedAs("intText")] [SerializeField] 
    private TMP_Text wholeNumberText = null;

    [FormerlySerializedAs("decText")] [SerializeField] 
    private TMP_Text decimaNumberText = null;

    // Update is called once per frame
    void Update()
    {
        
        wholeNumberText.text = TimeLord.Instance.LevelTime.ToString("0");
        decimaNumberText.text = TimeLord.Instance.LevelTime.ToString(".000").Remove(0,1);
    }
}
