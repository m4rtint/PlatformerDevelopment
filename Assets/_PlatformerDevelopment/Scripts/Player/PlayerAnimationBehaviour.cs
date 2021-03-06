﻿
using UnityEngine;

namespace PersonalDevelopment
{
    public class PlayerAnimationBehaviour : MonoBehaviour
    {
        private const string IsRunningAnimationParameter = "IsRunning";
        private const string IsTauntingAnimationParameter = "IsTaunting";
        private const string IsJumpingAnimationParameter = "IsJumping";
        private const string DoMeleeParameter = "DoMelee";
        private const string DoShootParameter = "DoShoot";
        private const string HurtParameter = "Hurt";
        private const string DeathParameter = "IsDead";

        private float _hadoukenDuration = 0;

        private Animator _animator = null;

        public void Initialize(Animator animator)
        {
            _animator = animator;
            SetupAnimationClipsTimes();
        }

        
        #region Animation
        public void SetRunParameter(bool isRunning)
        {
            if (_animator)
            {
                _animator.SetBool(IsRunningAnimationParameter, isRunning);
            }
        }

        public void SetJumpParameter(bool isJumping)
        {
            if (_animator)
            {
                _animator.SetBool(IsJumpingAnimationParameter, isJumping);
            }
        }

        public void SetTauntParameter(bool isTaunting)
        {
            if (_animator)
            {
                _animator.SetBool(IsTauntingAnimationParameter, isTaunting);
            }
        }

        public void DoMeleeAttack()
        {
            if (_animator && !IsJumping())
            {
                _animator.SetTrigger(DoMeleeParameter);
            }
        }

        public void DoShootAttack()
        {
            if (_animator && !IsJumping())
            {
                _animator.SetTrigger(DoShootParameter);
            }
        }

        public void Hurt()
        {
            if (_animator && !_animator.GetCurrentAnimatorStateInfo(0).IsName("Hurt"))
            {
                _animator.SetTrigger(HurtParameter);
            }
        }

        public void SetDead(bool isDead)
        {
            if (_animator)
            {
                _animator.SetBool(DeathParameter, isDead);
            }
        }
        
        #endregion
        
        #region Helpers

        private bool IsKicking()
        {
            if (_animator)
            {
                return _animator.GetCurrentAnimatorStateInfo(0).IsName("Melee");
            }

            return false;
        }

        public bool IsShooting()
        {
            if (_animator)
            {
                return _animator.GetCurrentAnimatorStateInfo(0).IsName("Shoot");
            }

            return false;
        }
        
        private bool IsJumping()
        {
            if (_animator)
            {
                return _animator.GetCurrentAnimatorStateInfo(0).IsName("Jump");
            }

            return false;
        }

        public bool IsAttacking()
        {
            return IsShooting() || IsKicking();
        }

        public float ShootAnimationTime()
        {
            return _hadoukenDuration;
        }
        #endregion

        private void SetupAnimationClipsTimes()
        {
            AnimationClip[] clips = _animator.runtimeAnimatorController.animationClips;
            foreach(AnimationClip clip in clips)
            {
                switch(clip.name)
                {
                    case "Humanoid_Hadouken":
                        _hadoukenDuration = clip.length;
                        break;
                }
            }
        }
    }

}