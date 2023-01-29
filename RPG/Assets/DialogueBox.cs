using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Image portrait;
    [SerializeField] private GameObject buttonContainer;
    [SerializeField] private Button buttonPrefab;

    private Story story;
    public bool playingDialogue = false;

    public void StartInteraction(NPCData data)
    {
        PlayerMovement.instance.Freeze();
        updatePortrait(data.sprite);
        gameObject.SetActive(true);

        story = new Story(data.inkJSON.text);

        // Add Buttons based on NPC functions
        foreach (string s in data.options)
        {
            // Add Button
            Button newbutton = Instantiate(buttonPrefab, transform);
            newbutton.transform.SetParent(buttonContainer.transform);

            // Change Button Text
            TextMeshProUGUI buttonText = newbutton.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = s;
        }
    }

    public void EndInteraction()
    {
        PlayerMovement.instance.Unfreeze();
        gameObject.SetActive(false);

        // Destroy Buttons
        foreach (Transform child in buttonContainer.transform)
            Destroy(child.gameObject);
    }

    private void updatePortrait(Sprite sprite)
    {
        portrait.sprite = sprite;
    }
}
