using UnityEngine;

public class PlayerAnimationTrigger : MonoBehaviour
{
    private PlayerContext mPlayer;
    private PlayerDamage playerDamage;
    void Awake()
    {
        mPlayer = GetComponentInParent<PlayerContext>();
        playerDamage = GetComponentInParent<PlayerDamage>();
    }

    public void PlayerAttackTrigger()
    {
        Debug.Log("Player attack trigger");
        playerDamage.DealDamage();
    }

    public void AnimationTrigger()
    {
        Debug.Log("animation trigger player");
        mPlayer.animTrigger = true;
    }

    public void ResetTriggers()
    {
        mPlayer.animTrigger = false;
    }
}
