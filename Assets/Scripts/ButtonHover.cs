using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
 
    private TextMeshProUGUI _buttonText;
    private string _originalText;

    private void Start()
    {
        _buttonText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _originalText = _buttonText.text;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _buttonText.text = $"<color=#b5b5b5>{_originalText}</color>";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _buttonText.text = $"<color=white>{_originalText}</color>";
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        _buttonText.text = $"<color=#808080>{_originalText}</color>";
    }
}
