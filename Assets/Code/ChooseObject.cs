using UnityEngine;
using UnityEngine.UI;

public class ChooseObject : MonoBehaviour
{
    private ProgramManager _programManager;
    private Button _button;
    public GameObject _gameObject;

    private void Start()
    {
        _programManager = FindObjectOfType<ProgramManager>();

        _button = GetComponent<Button>();
        _button.onClick.AddListener(ChooseObjectFunktion);
    }

    void ChooseObjectFunktion()
    {
        _programManager.objSpawn = _gameObject;
        _programManager.ChooseObject = true;
        _programManager._scrollView.SetActive(false);
    }
}
