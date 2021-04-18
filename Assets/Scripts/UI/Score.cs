using TMPro;
using UnityEngine;

namespace UI
{
    public class Score : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _ScoreText = null;

        private void Update()
        {
            _ScoreText.SetText(GameState.Instance.Score.ToString());
        }

    }
}