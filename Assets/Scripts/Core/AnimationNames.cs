public class AnimationConstants {
  public const string ATTACK = "Attack";
  public const string DIE = "Die";
  public const string GET_HIT = "GetHit";
  public const string IS_RUNNING = "IsRunning";
  public const string IS_WALKING = "IsWalking";
  public const string IS_WATCHING = "IsStanding";
  public const string IS_STUNNED = "IsStanding";
  public const string KNOCKBACK = "Knockback";
  public const string SKILL = "Skill";

  // While obviously this wouldn't work in case we had lots of characters that 
  // have different animations. But well, good enough for now I guess
  public const float KNOCKBACK_DURATION = 2.367f;
  public const float ATTACK_DURATION = 2.767f;
  public const float CASTING_DURATION = 1.167f;
}
