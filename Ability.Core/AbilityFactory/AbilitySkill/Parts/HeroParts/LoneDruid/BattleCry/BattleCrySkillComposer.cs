﻿// <copyright file="BattleCrySkillComposer.cs" company="EnsageSharp">
//    Copyright (c) 2017 Moones.
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see http://www.gnu.org/licenses/
// </copyright>
namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.HeroParts.LoneDruid.BattleCry
{
    using System.ComponentModel.Composition;

    using Ability.Core.AbilityFactory.AbilitySkill.Metadata;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.CastFunction;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillCastData;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.HeroParts.LoneDruid.BattleCry.CastingFunction;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.SkillComposer;

    using Ensage;

    [Export(typeof(IAbilitySkillComposer))]
    [AbilitySkillMetadata((uint)AbilityId.lone_druid_true_form_battle_cry)]
    internal class BattleCrySkillComposer : DefaultSkillComposer
    {
        #region Constructors and Destructors

        public BattleCrySkillComposer()
        {
            this.AssignPart<ICastFunction>(
                skill =>
                    {
                        if (!skill.Owner.IsEnemy && skill.Owner.SourceUnit.IsControllable)
                        {
                            return new BattleCryCastingFunction(skill);
                        }

                        return null;
                    });
        }

        #endregion
    }
}