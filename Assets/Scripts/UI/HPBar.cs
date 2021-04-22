using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HPBar : MonoBehaviour
    {
        [SerializeField]
        private Player _player = null;

        private RectMask2D _mask = null;
        private RectTransform _transform = null;

        private void Start()
        {
            _mask = GetComponent<RectMask2D>();
            _transform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            Vector4 padding = _mask.padding;
            padding.z = _transform.sizeDelta.x * (1f - _player.HPPercentage);
            _mask.padding = padding;
        }

        public void RegisterPlayer(Player player)
        {
            _player = player;
        }
    }
}