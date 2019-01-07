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
			SetBool("Forward", true);
			if(flipped) {
				FlipSprite();
			}
		} else if(dir == Direction.RIGHT) {
			SetBool("Forward", true);
			if(!flipped) {
				FlipSprite();
			}
		} else if(dir == Direction.UP) {
			SetBool("Forward", false);
			if(flipped) {
				FlipSprite();
			}
		} else if (dir == Direction.LEFT) {
			SetBool("Forward", false);
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

	private void FlipSprite() {
		Vector3 flipTransform = transform.localScale;
		flipTransform.x *= -1;
		transform.localScale = flipTransform;
		flipped = !flipped;
	}
}