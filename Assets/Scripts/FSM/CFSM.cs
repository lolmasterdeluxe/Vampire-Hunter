using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Author: Muhammad Rifdi bin Sabbri 
 * Created: 20/2/2022
 */

public abstract class CFSM : MonoBehaviour
{
    protected enum FSM
    {
        IDLE = 0,
		PATROL = 1,
		ATTACK = 2,
		NUM_FSM,
	};
	// Current FSM
	protected FSM CurrentFSM = FSM.IDLE;
	// FSM counter - count how many frames it has been in this FSM
	[HideInInspector]
	protected float FSMCounter;
	// Max count in a state
	protected const int MaxFSMCounter = 1;

	// Update is called once per frame
	protected void FSM_Update()
    {
		switch (CurrentFSM)
		{
			case FSM.IDLE:
				Debug.Log("sCurrentFSM = IDLE.");
				break;
			case FSM.PATROL:
				Debug.Log("sCurrentFSM = PATROL.");
				break;
			case FSM.ATTACK:
				Debug.Log("sCurrentFSM = ATTACK.");
				break;
			default:
				Debug.Log("sCurrentFSM is Undefined.");
				break;
		}
	}
}
