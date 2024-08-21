using System.Threading.Tasks;
using SFB;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

/// <summary>
/// Handles the Connected Components Scene UI Toolkit Events.
/// </summary>
/// <remarks>
/// This script utilizes the StandaloneFileBrowser unitypackage from 
/// https://github.com/gkngkc/UnityStandaloneFileBrowser/tree/master/Assets/StandaloneFileBrowser.
/// Additionally, it includes async modifications with help from GPT-4o.
/// </remarks>
public class CCEvents : MonoBehaviour
{
    public App app;
    private UIDocument _document;
    private VisualElement _root;
    private VisualElement _mainContainer;
    private Button _backButton;
    private Button _selectFileButton;
    private Button _scanButton;
    private Label _vText;
    private Label _fText;
    private Label _ccText;

    private bool _isScannable;
    public bool IsUIActive
    {
        get => !_mainContainer.ClassListContains("hidden");
        set
        {
            if (value)
            {
                _mainContainer.RemoveFromClassList("hidden");
            }
            else
            {
                _mainContainer.AddToClassList("hidden");
            }
        }
    }

    private void Awake()
    {
        _document = GetComponent<UIDocument>();
        _root = _document.rootVisualElement;
        _mainContainer = _root.Q<VisualElement>("MenuContainer");
    }

    private void OnEnable()
    {
        _backButton = _root.Q<Button>("Back");
        _backButton.clicked += OnBackButtonClicked;

        _selectFileButton = _root.Q<Button>("Select");
        _selectFileButton.clicked += async () => await OnSelectFileButtonClickedAsync();

        _scanButton = _root.Q<Button>("Submit");
        _scanButton.clicked += OnSubmitButtonClicked;
        _scanButton.SetEnabled(false);
        
        _vText = _root.Q<Label>("Vertices");
        _fText = _root.Q<Label>("Faces");
        _ccText = _root.Q<Label>("CC");
    }

    private void OnBackButtonClicked()
    {
        SceneManager.LoadScene(0);
    }

    private async Task OnSelectFileButtonClickedAsync()
    {
        string originalText = _selectFileButton.text;
        _selectFileButton.text = "Waiting...";
        Application.runInBackground = true;
        var extensions = new[]
        {
            new ExtensionFilter("3D Files", "obj", "ply")
        };
        var paths = StandaloneFileBrowser.OpenFilePanel("Select a 3D File", "", extensions, false);

        if (paths.Length > 0 && !string.IsNullOrEmpty(paths[0]))
        {
            string path = paths[0];
            await FileProcessor.LoadFileAsync(path);
            if (FileProcessor.Structure != null && FileProcessor.Structure.Count > 0)
            {
                Debug.Log("Archivo cargado y procesado correctamente.");
                app.RenderMesh(FileProcessor.Structure);
                _isScannable = true;
                _vText.text = $"{FileProcessor.Vertices.Count}";
                _fText.text = $"{FileProcessor.Faces.Count}";
                _ccText.text = "";
                _selectFileButton.text = originalText;
                _scanButton.SetEnabled(true);
            }
            else
            {
                Debug.Log("No se pudo cargar el archivo.");
                _isScannable = false;
            }
        }
        Application.runInBackground = false;
    }

    private void OnSubmitButtonClicked()
    {
        Application.runInBackground = true;
        if (_isScannable && FileProcessor.Structure != null && FileProcessor.Structure.Count > 0)
        {
            
            int componentCount = app.ConnectedComponents();
            _ccText.text = $"{componentCount}";
        }
        Application.runInBackground = false;
    }
}