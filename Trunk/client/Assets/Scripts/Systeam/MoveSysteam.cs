using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class MoveSysteam : ReactiveSystem<InputEntity>
{
    private Transform _targetTransform;
    private float _moveSpeed = 2.0f;
    public MoveSysteam(Contexts contexts) : base(contexts.input)
    {
        _targetTransform = GameObject.Find("player").GetComponent<Transform>();
    }
    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
    {
        return context.CreateCollector(Matcher<InputEntity>.AnyOf(
             InputMatcher.KeyForwardInput
            ,InputMatcher.KeyBackInput
            ,InputMatcher.KeyLeftInput
            ,InputMatcher.KeyRightInput
            ));
    }
    protected override bool Filter(InputEntity entity)
    {
        return entity.hasKeyBackInput || entity.hasKeyForwardInput || entity.hasKeyRightInput || entity.hasKeyLeftInput;
    }
    protected override void Execute(List<InputEntity> entities)
    {
        foreach (var e in entities)
        {
            if (e.hasKeyForwardInput&& e.keyForwardInput.value)
            {
                _targetTransform.Translate(Vector3.forward * Time.deltaTime* _moveSpeed);
            }

            if (e.hasKeyBackInput &&e.keyBackInput.value)
            {
                _targetTransform.Translate(Vector3.back * Time.deltaTime* _moveSpeed);
            }

            if (e.hasKeyLeftInput&&e.keyLeftInput.value)
            {
                _targetTransform.Translate(Vector3.left * Time.deltaTime* _moveSpeed);
            }

            if (e.hasKeyRightInput&&e.keyRightInput.value)
            {
                _targetTransform.Translate(Vector3.right * Time.deltaTime* _moveSpeed);
            }
        }
    }

}
