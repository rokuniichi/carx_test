using UnityEngine;

public class GuidedProjectile : BaseProjectile {
	private Transform _target;
	private Vector3   _lastTargetPosition;
	public void SetTarget(Transform target)
	{
		_target = target;
		_target.GetComponent<IKillable>().OnKill += RemoveTarget;
	}

	public override void SelfRemove()
	{
		if (_target != null)
			RemoveTarget(_target);
		base.SelfRemove();
	}

	private void RemoveTarget(Transform target)
	{
		if (_target != target) return;
		_target.GetComponent<IKillable>().OnKill -= RemoveTarget;
		_target = null;
    }    

    void Update () {
		if (_target != null)
			_lastTargetPosition = _target.transform.position;

		Vector3 translation = _lastTargetPosition - transform.position;
		float frameDistance = speed * Time.deltaTime;
		if (translation.magnitude > frameDistance)
			transform.Translate(translation.normalized * frameDistance);
		else
			if (_target == null)
				SelfRemove();
	}
}
