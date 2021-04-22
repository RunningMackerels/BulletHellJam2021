using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace UI
{
    public class ChooseEquation : MonoBehaviour
    {
        [SerializeField]
        private Sprite[] _equations;

        private void Awake() => GetComponent<Image>().sprite = _equations[Random.Range(0, _equations.Length)];
    }
}
