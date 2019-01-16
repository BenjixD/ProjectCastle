using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitAnimator : MonoBehaviour {
	public Animator animator;

	bool flipped;

	void Start() {
		this.animator = GetComponent<Animator>();
	}

	public void SetDirection(Direction dir) {
		if(dir == Direction.DOWN) {
			UseForwardLayer();
			if(flipped) {
				FlipSprite();
			}
		} else if(dir == Direction.RIGHT) {
			UseForwardLayer();
			if(!flipped) {
				FlipSprite();
			}
		} else if(dir == Direction.UP) {
			UseBackwardLayer();
			if(flipped) {
				FlipSprite();
			}
		} else if (dir == Direction.LEFT) {
			UseBackwardLayer();
			if(!flipped) {
				FlipSprite();
			}
		}
	}

	public void SetBool(string param, bool val) {
		animator.SetBool(param, val);
	}

	public void Trigger(string param) {
		animator.SetTrigger(param);
	}

	private void UseBackwardLayer() {
		animator.SetLayerWeight(1, 0);
		animator.SetLayerWeight(2, 100);
	}

	private void UseForwardLayer() {
		animator.SetLayerWeight(1, 100);
		animator.SetLayerWeight(2, 0);
	}

	private void FlipSprite() {
		Vector3 flipTransform = transform.localScale;
		flipTransform.x *= -1;
		transform.localScale = flipTransform;
		flipped = !flipped;
	}
}