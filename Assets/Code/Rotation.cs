using UnityEngine;
using UnityEngine.UI;

public class Rotation : MonoBehaviour
{
    private Button _button;
    private ProgramManager _programManager;

    private void Start()
    {
        _programManager = FindObjectOfType<ProgramManager>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(RotationFunction);
    }

    void RotationFunction()
    {
        if (_programManager.rotate)
        {
            _programManager.rotate = false;
            GetComponent<Image>().color = Color.red;
        }
        else
        {
            _programManager.rotate = true;
            GetComponent<Image>().color = Color.green;
        }
    }
}
