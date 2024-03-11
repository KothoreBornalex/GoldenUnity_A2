using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum Interaction
{
    ColliderEnter,
    ColliderExit,
    TriggerEnter,
    TriggerExit,
}
public class ColliderTriggerEvent : MonoBehaviour
{

    [SerializeField] private Interaction _interactionType;
    [SerializeField] private UnityEvent OnInteraction;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_interactionType == Interaction.ColliderEnter) OnInteraction?.Invoke();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (_interactionType == Interaction.ColliderExit) OnInteraction?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_interactionType == Interaction.TriggerEnter) OnInteraction?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_interactionType == Interaction.TriggerExit) OnInteraction?.Invoke();
    }


}
