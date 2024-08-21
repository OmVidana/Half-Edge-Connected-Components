using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

/// <summary>
/// Handles the Main Menu UI Toolkit Events for the Start Scene.
/// </summary>
public class MainMenuEvents : MonoBehaviour
{
    private VisualElement _root;
    private Button _startButton;
    private Button _exitButton;
    private void Awake()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
    }
    
    private void OnEnable()
    {
        _startButton = _root.Q<Button>("Start");
        _startButton.clicked += OnStartButtonClicked;

        _exitButton = _root.Q<Button>("Exit");
        _exitButton.clicked += OnExitButtonClicked;
    }
    private void OnStartButtonClicked()
    {
        SceneManager.LoadScene(1);
    }
    
    private void OnExitButtonClicked()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
