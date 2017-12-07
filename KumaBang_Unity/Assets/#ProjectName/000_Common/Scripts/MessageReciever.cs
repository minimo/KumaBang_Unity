using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageReciever : MonoBehaviour, IRecieveMessage {
    
	public void OnRecieve () {
		Debug.Log("recieve");
	}
}
