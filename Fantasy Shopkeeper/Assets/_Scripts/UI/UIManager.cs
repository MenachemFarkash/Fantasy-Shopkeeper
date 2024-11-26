using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour {

    #region !========Singleton==========!
    public static UIManager instance;
    private void Awake() {
        instance = this;
    }
    #endregion

    [SerializeField] private GameObject InteractPrompt;


    public void OpenInteractPrompt(string name, KeyCode keyCode) {
        InteractPrompt.SetActive(true);
        TextMeshProUGUI[] prompts = InteractPrompt.GetComponentsInChildren<TextMeshProUGUI>();
        prompts[0].text = name;
        prompts[1].text = keyCode.ToString();
    }

    public void CloseInteractPrompt() {
        InteractPrompt.SetActive(false);
    }
}
