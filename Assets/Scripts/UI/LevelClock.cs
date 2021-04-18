using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class LevelClock : MonoBehaviour
    {

        [SerializeField] 
        private TMP_Text wholeNumberText = null;

        [FormerlySerializedAs("decimaNumberText")] [FormerlySerializedAs("decText")] [SerializeField] 
        private TMP_Text decimalNumberText = null;

        // Update is called once per frame
        void Update()
        {
        
            wholeNumberText.text = Mathf.Floor(TimeLord.Instance.LevelTime).ToString("0");
            decimalNumberText.text = (TimeLord.Instance.LevelTime - Mathf.Floor(TimeLord.Instance.LevelTime))
                .ToString("F3").Remove(0,1);
        }
    }
}
