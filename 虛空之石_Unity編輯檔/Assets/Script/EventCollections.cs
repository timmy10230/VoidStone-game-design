using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCollections : MonoBehaviour
{
    public void Message(string msg)
    {
        transform.root.SendMessage(msg, SendMessageOptions.DontRequireReceiver);
    }

    public void MessageFloat(AnimationEvent evt)
    {
        transform.root.SendMessage(evt.stringParameter, evt.floatParameter, SendMessageOptions.DontRequireReceiver);
    }

    public void MessageInt(AnimationEvent evt)
    {
        transform.root.SendMessage(evt.stringParameter, evt.intParameter, SendMessageOptions.DontRequireReceiver);
    }

    public void MessageString(string msg)
    {
        string[] str = msg.Split(","[0]);
        transform.root.SendMessage(str[0], str[1], SendMessageOptions.DontRequireReceiver);
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
