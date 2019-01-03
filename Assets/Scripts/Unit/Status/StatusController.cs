using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StatusController {
	private Dictionary<string, StatusEffect> statusList;
	private Queue<StatusEffect> AddStatusFrameQueue;
	private Queue<StatusEffect> RemoveStatusFrameQueue;

	public StatusController() {
		statusList = new Dictionary<string, StatusEffect>();
		AddStatusFrameQueue = new Queue<StatusEffect>();
		RemoveStatusFrameQueue = new Queue<StatusEffect>();
	}

	public void QueueAddStatus(StatusEffect status) {
		AddStatusFrameQueue.Enqueue(status);
	}

	public void QueueRemoveStatus(StatusEffect status) {
		RemoveStatusFrameQueue.Enqueue(status);
	}

	public void ApplyAddStatusEffects() {
		while(AddStatusFrameQueue.Count > 0) {
			AddStatus(AddStatusFrameQueue.Dequeue());
		}
	}

	public void ApplyRemoveStatusEffects() {
		while(RemoveStatusFrameQueue.Count > 0) {
			ClearStatus(RemoveStatusFrameQueue.Dequeue());
		}
	}

	public void DecrementDuration(int time) {
		foreach(KeyValuePair<string, StatusEffect> status in statusList) {
			status.Value.duration -= time;
		}
	}

	public void ExpireStatus() {
		List<string> expiredStatusNames = new List<string>();
		foreach(KeyValuePair<string, StatusEffect> status in statusList) {
			if(status.Value.duration <= 0) {
				expiredStatusNames.Add(status.Key);
			}
		}
		foreach(string expired in expiredStatusNames) {
			statusList.Remove(expired);
		}
	}

	public IEnumerator<KeyValuePair<string, StatusEffect>> GetEnumerator() {
		return statusList.GetEnumerator();
	}

	private void AddStatus(StatusEffect status) {
		string key = GetStatusEffectKey(status);

		if(statusList.ContainsKey(key)) {
			StatusEffect overwrite = statusList[key].GetOverwrite(status);
			statusList[key] = overwrite;
		}
		else {
			statusList.Add(key, status);
		}
	}

	private void ClearStatus(StatusEffect status) {
		statusList.Remove(GetStatusEffectKey(status));
	}

	private void ClearStatus(string statusName) {
		statusList.Remove(statusName);		
	}

	private string GetStatusEffectKey(StatusEffect status) {
		return status.GetType().Name;
	}
}