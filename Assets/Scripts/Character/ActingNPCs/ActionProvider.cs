using UnityEngine;

public abstract class ActionProvider : BaseBehaviour {
  public abstract void PerformAction(GameObject target);
  public abstract void OnTargetSet(GameObject target);
}
