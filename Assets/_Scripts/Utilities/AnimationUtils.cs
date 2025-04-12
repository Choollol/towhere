using UnityEngine;

public static class AnimationUtils
{
    public static AnimationClip GetAnimation(this Animator animator, string animationName)
    {
        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == animationName)
            {
                return clip;
            }
        }

        return null;
    }
    public static bool IsAnimationPlaying(this Animator animator, string animationName)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(animationName);
    }
    public static bool IsCurrentAnimationPlaying(this Animator animator)
    {
        return animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1;
    }
}
