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

    private Button talk;
    private Story story;
    private bool dialoguePlaying = false;
    private string currentLine;

    public float textSpeed = 0.1f;

    private void Start()
    {
        dialogueText.text = string.Empty;
    }
    public void StartInteraction(NPCData data)
    {
        PlayerMovement.instance.Freeze();
        UpdatePortrait(data.sprite);
        gameObject.SetActive(true);

        // Set Dialogue 
        story = new Story(data.inkJSON.text);

        // Add Buttons based on NPC functions
        foreach (string s in data.options)
        {
            // Add Button
            Button newButton = Instantiate(buttonPrefab, transform);
            newButton.transform.SetParent(buttonContainer.transform);
            newButton.interactable = false;

            // Change Text
            TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = s;

            // Assign Button function
            AssignButtonFunction(s, newButton);
        }

        // Start Dialogue
        ContinueDialogue();
    }

    public void EndInteraction()
    {
        Player.instance.interacting = false;
        PlayerMovement.instance.Unfreeze();
        gameObject.SetActive(false);

        // Destroy Buttons
        foreach (Transform child in buttonContainer.transform)
            Destroy(child.gameObject);
    }

    private void UpdatePortrait(Sprite sprite)
    {
        // Set Portrait from sprite
        portrait.sprite = sprite;
    }

    private void AssignButtonFunction(string s, Button button)
    {
        // Uses String to determine button function
        // Talk, Repair, Shop, Warp
        switch (s)
        {
            // Continue Dialogue
            case "Talk":
                talk = button;
                talk.onClick.AddListener(ContinueDialogue);
                break;

            // Open Repair
            case "Repair":
                button.onClick.AddListener(() => { Debug.Log("Repairing"); });
                break;

            // Open Shop
            case "Shop":
                button.onClick.AddListener(() => { Debug.Log("Shopping"); });
                break;

            // Warp
            case "Warp":
                button.onClick.AddListener(() => { Debug.Log("Warping"); });
                break;

            default:
                button.onClick.AddListener(() => { Debug.Log("No Function Found"); });
                break;
        }
    }
    
    private void ContinueDialogue()
    {
        // Fills out line
        if (dialoguePlaying)
        {
            StopAllCoroutines();
            dialogueText.text = currentLine;
            dialoguePlaying = false;
            if (!story.canContinue)
                EnableButtons();
        }
        // Continues Dialogue 
        else if (story.canContinue)
        {
            dialogueText.text = string.Empty;
            dialoguePlaying = true;
            talk.interactable = true;
            currentLine = story.Continue();
            StartCoroutine(typeDialogue(currentLine));
        }
    }

    private IEnumerator typeDialogue(string s)
    {
        foreach (char c in s)
        {
            yield return new WaitForSeconds(textSpeed);
            dialogueText.text += c;
        }
        dialoguePlaying = false;

        if (!story.canContinue)
            EnableButtons();
    }

    private void EnableButtons()
    {
        Button[] buttons = buttonContainer.GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
            button.interactable = true;
    }
}
