﻿// <copyright file="PipeSkillComposer.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.ItemParts.Pipe
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    using Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier;
    using Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier.Workers.DamageManipulation;
    using Ability.Core.AbilityFactory.AbilitySkill.Metadata;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.ModifierGenerator;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.SkillComposer;

    using Ensage;
    using Ensage.Common.Extensions;

    [Export(typeof(IAbilitySkillItemComposer))]
    [AbilitySkillItemMetadata((uint)AbilityId.item_pipe)]
    internal class PipeSkillComposer : DefaultSkillComposer
    {
        #region Constructors and Destructors

        internal PipeSkillComposer()
        {
            this.AssignPart<IModifierGenerator>(
                skill =>
                    new ModifierGenerator(skill)
                        {
                            Workers =
                                new List<ModifierGeneratorWorker>
                                    {
                                        new ModifierGeneratorWorker(
                                            "modifier_item_pipe_barrier",
                                            modifier =>
                                                modifier.AssignModifierEffectApplier(
                                                    new ModifierEffectApplier(modifier)
                                                        {
                                                            Workers =
                                                                new List<IEffectApplierWorker>
                                                                    {
                                                                        new MagicOnlyEffectApplierWorker(
                                                                            modifier,
                                                                            false,
                                                                            abilityModifier =>
                                                                                abilityModifier.SourceSkill.SourceItem
                                                                                    .GetAbilityData("barrier_block"))
                                                                    }
                                                        }),
                                            false,
                                            true,
                                            true)
                                    }
                        });
        }

        #endregion
    }
}