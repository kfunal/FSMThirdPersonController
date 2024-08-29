using TMPro;
using UnityEngine;

public class CharacterUI : MonoBehaviour
{
    [Header("Interaction")]
    [SerializeField] private GameObject interactionObject;
    [SerializeField] private TextMeshProUGUI promptText;

    public void ShowInteraction(string _promptText)
    {
        promptText.SetText(_promptText);
        interactionObject.SetActive(true);
    }

    public void HideInteraction() => interactionObject.SetActive(false);
}
