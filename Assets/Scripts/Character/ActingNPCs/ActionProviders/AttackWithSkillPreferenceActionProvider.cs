using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class AttackWithSkillPreferenceActionProvider : ActionProvider {
  [SerializeField]
  private float chaseDistance;
  // List of skills that NPC uses in order of preference. 
  [SerializeField]
  private Skill[] skills;
  private MovementController movementController;
  private Character targetCharacter;
  private Animator animator;

  protected override void OnAwake() {
    base.OnAwake();

    movementController = GetComponent<MovementController>();
    animator = GetComponent<Animator>();
  }

  protected override void OnWillDestroy() {
    base.OnWillDestroy();

    if (targetCharacter != null) {
      targetCharacter.UnregisterAttacker(gameObject);
    }
  }

  public override void PerformAction(GameObject target) {
    HandleMovementTowardsTarget(target);

    for (int i = 0; i < skills.Length; i++) {
      Skill skill = GetSkill(i);
      if (skill.CanPerform(target)) {
        skill.Perform(target);
        break;
      }
    }
  }

  public override void OnTargetSet(GameObject target) {
    if (targetCharacter != null) {
      targetCharacter.UnregisterAttacker(gameObject);
    }

    if (target == null) {
      animator.SetBool(AnimationConstants.IS_WATCHING, false);
      return;
    }

    HandleMovementTowardsTarget(target);
  }

  private void HandleMovementTowardsTarget(GameObject target) {
    float targetDistance = Vector3.Distance(
      transform.position,
      target.transform.position
    );
    if (targetDistance > chaseDistance) {
      movementController.KeepLookingAt(target);
      if (animator != null) {
        animator.SetBool(AnimationConstants.IS_WATCHING, true);
      }
    } else {
      targetCharacter = target.GetComponent<Character>();
      if (targetCharacter != null) {
        targetCharacter.RegisterAttacker(gameObject);
      }
      if (movementController.Followed == null) {
        movementController.Follow(target);
      }
      if (animator != null) {
        animator.SetBool(AnimationConstants.IS_WATCHING, false);
      }
    }
  }

  private Skill GetSkill(int preference) {
    return skills[preference];
  }
}
