using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListenerWithDelay : GameEventListener 
{
    // Preform the unity event response to the call
    public UnityEvent delayresponse;
    // Delay time for the event call
    [SerializeField] private float delayTime = 1f;

    public override void OnEventCall()
    {
        response.Invoke();
        StartCoroutine(RunDelayEvent());
    }

    private IEnumerator RunDelayEvent()
    {
        yield return new WaitForSeconds(delayTime);
        delayresponse.Invoke();
    }
}
