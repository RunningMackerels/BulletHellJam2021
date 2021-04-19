using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PreMain : MonoBehaviour
{
    private void OnFire(InputValue value)
    {
        SceneManager.LoadScene("Main");
    }
}
