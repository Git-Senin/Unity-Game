using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySlot : MonoBehaviour
{
    [SerializeField] private Image abilityImage;
    [SerializeField] private Image mask;
    [SerializeField] private TextMeshProUGUI cooldownTimer;
    private Transform abilityContainer;

    private void Awake()
    {
        abilityContainer = UIManager.instance.PlayerInterface.transform.Find("Abilities").GetComponent<Transform>();
        transform.SetParent(abilityContainer);
    }
    public void SetFill(float current, float max)
    {
        mask.fillAmount = current/max;
    }
    public void SetImage(Sprite image)
    {
        abilityImage.sprite = image;
    }
    public void SetCooldown(float timer)
    {
        cooldownTimer.text = timer.ToString("0"); 
    }
}
