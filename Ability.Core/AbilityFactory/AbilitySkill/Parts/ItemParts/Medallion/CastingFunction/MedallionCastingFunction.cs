﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.ItemParts.Medallion.CastingFunction
{
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.CastFunction.Generic;
    public class MedallionCastingFunction : DefaultCastingFunction
    {
        public MedallionCastingFunction(IAbilitySkill skill)
            : base(skill)
        {
        }

        public override bool CanCast()
        {
            return base.CanCast()
                   && this.Skill.Owner.TargetSelector.LastDistanceToTarget < this.Skill.Owner.AttackRange.Value + 150
                   && this.Skill.Owner.TargetSelector.Target.Modifiers.Attackable
                   && (this.Skill.Owner.TargetSelector.Target.Health.Current
                       > this.Skill.Owner.AttackDamage.GetDamage(this.Skill.Owner.TargetSelector.Target) * 1.5);
        }
    }
}
