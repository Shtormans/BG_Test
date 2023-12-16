using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShieldButton : MonoBehaviour
{
    [SerializeField, Range(1f, 5f)] private float _seconds = 4f;

    private Button shieldButton;

    private void Awake()
    {
        shieldButton = GetComponent<Button>();
    }

    public void OnButtonClicked()
    {
        StartCoroutine(ChangeButtonStateForSeconds());
    }

    private IEnumerator ChangeButtonStateForSeconds()
    {
        shieldButton.interactable = false;
        
        yield return new WaitForSeconds(_seconds);

        shieldButton.interactable = true;
    }
}
