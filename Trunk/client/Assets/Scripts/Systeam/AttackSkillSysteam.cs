using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class AttackSkillSysteam : ReactiveSystem<InputEntity>
{
    public AttackSkillSysteam(Contexts contexts) : base(contexts.input)
    {
    }
    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
    {
        return context.CreateCollector(Matcher<InputEntity>.AnyOf(
            InputMatcher.KeyAttackSkill1
           
        ));
    }
    protected override bool Filter(InputEntity entity)
    {
        return entity.hasKeyAttackSkill1;
    }

    protected override void Execute(List<InputEntity> entities)
    {
        foreach (var e in entities)
        {
            Debug.Log("Attack1");
        }
    }


}