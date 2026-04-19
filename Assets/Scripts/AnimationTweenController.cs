using System;
using UnityEngine;
using DG.Tweening;


public class AnimationTweenController : MonoBehaviour {
    private Vector3 basePosition;


    private void Start()
    {
        basePosition = transform.position;
        
        TweenParams tParams = new TweenParams().SetLoops(-1).SetEase(Ease.InBounce);
        
        Sequence babyShake = DOTween.Sequence();
        babyShake.Append(
                transform.DORotate(
                    new Vector3(0, 50, 0), 0.1f
                )).Append(
            transform.DORotate(
                new Vector3(0, -50, 0), 0.1f
            )).SetAs(tParams);
    }
    
    private void Update()
    {
        
    }
}
