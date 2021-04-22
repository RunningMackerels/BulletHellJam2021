using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject _mainCredits;

    [SerializeField]
    private GameObject _mainTitle;

    bool _visible = false;

    private void OnFire(InputValue value)
    {
        SceneManager.LoadScene("Main");
    }

    private void OnCredits(InputValue valus)
    {
        _visible = !_visible;
        _mainCredits.SetActive(_visible);
        _mainTitle.SetActive(!_visible);
    }
}


