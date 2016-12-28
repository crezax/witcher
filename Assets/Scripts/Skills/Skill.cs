using UnityEngine;
using System.Collections;

public abstract class Skill : BaseBehaviour {

  protected abstract float CastTime { get; }
  protected abstract void PerformImplementation();
  protected abstract bool CanPerform { get; }
  protected abstract void PaySkillCost();

  public bool IsPerforming { get; private set; }

  public void Perform() {
    if (!CanPerform) {
      return;
    }
    StartCoroutine(PerformCoroutine());
  }

  private IEnumerator PerformCoroutine() {
    PaySkillCost();
    IsPerforming = true;
    yield return new WaitForSeconds(CastTime);
    IsPerforming = false;
    PerformImplementation();
  }
}
