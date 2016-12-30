using System.Collections;
using UnityEngine;

public abstract class Skill : BaseBehaviour {

  protected abstract float CastTime { get; }
  protected abstract float EffectTime { get; }
  protected abstract void PerformImplementation(GameObject target);
  public abstract bool CanPerform(GameObject target);
  protected abstract void PaySkillCost();

  private Character skillUser;
  private MovementController skillUserMovementController;
  private Animator animator;

  protected Character SkillUser {
    get {
      return skillUser;
    }
  }

  protected override void OnAwake() {
    base.OnAwake();

    skillUser = GetComponent<Character>();
    animator = GetComponent<Animator>();

  }

  public void Perform(GameObject target) {
    if (!CanPerform(target) || !skillUser.CanUseSkills || skillUser.IsUsingSkill) {
      return;
    }
    StartCoroutine(PerformCoroutine(target));
  }

  protected virtual string AnimationName {
    get {
      return AnimationConstants.SKILL;
    }
  }

  private IEnumerator PerformCoroutine(GameObject target) {
    animator.SetTrigger(AnimationName);
    float startTime = Time.time;

    skillUser.IsUsingSkill = true;

    while (Time.time - startTime < EffectTime) {
      if (skillUser.CanUseSkills) {
        yield return null;
        continue;
      }
      FinishPerforming();
      yield break;
    }
    PaySkillCost();
    PerformImplementation(target);
    yield return new WaitForSeconds(CastTime - EffectTime);
    FinishPerforming();
  }

  private void FinishPerforming() {
    skillUser.IsUsingSkill = false;
  }
}
