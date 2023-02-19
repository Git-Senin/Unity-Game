using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerForwarder : MonoBehaviour
{
    [SerializeField] GameObject target;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        target.SendMessage("OnTriggerEnter2D", collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        target.SendMessage("OnTriggerExit2D", collision);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        target.SendMessage("OnTriggerStay2D", collision);
    }
}
