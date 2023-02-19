using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Linq;

public class DialogueBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Image portrait;
    [SerializeField] private GameObject buttonContainer;
    [SerializeField] private Button buttonPrefab;
    
    private Button talk;
    private Story story;
    private PlayerInput playerInput;
    private string currentLine;
    private bool dialoguePlaying = false;

    public float textSpeed = 0.1f;

    private void OnEnable()
    {
        // Reset Dialogue
        dialogueText.text = string.Empty;

        // Set Player Controls to UI
        // Disable Player Controller
        playerInput = PlayerController.instance.playerInput;
        playerInput.SwitchCurrentActionMap("UI");
        PlayerController.instance.EnablePlayerController(false);
        PlayerController.instance.SubscribeEvents(false);
        playerInput.actions["Cancel"].performed += Cancel;
    }
    private void OnDisable()
    {
        // Set Player Controls to Player
        // Enable Player Controller
        playerInput.SwitchCurrentActionMap("Player");
        PlayerController.instance.EnablePlayerController(true);
        PlayerController.instance.SubscribeEvents(true);
        playerInput.actions["Cancel"].performed -= Cancel;

    }

    private void Cancel(InputAction.CallbackContext context)
    {
        EndInteraction();
    }
    public void Interact(InputAction.CallbackContext context)
    {
        StartInteraction(Player.instance.selectedNPC.data);
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
    
    public void StartInteraction(NPCData data)
    {
        gameObject.SetActive(true);

        UpdatePortrait(data.sprite);

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
        // Close Dialogue Box
        gameObject.SetActive(false);

        // Stop Dialogue
        dialoguePlaying = false;

        // Destroy Buttons
        foreach (Transform child in buttonContainer.transform)
            Destroy(child.gameObject);
    }
}
