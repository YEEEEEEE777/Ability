﻿// <copyright file="ISkillBook.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.SkillBook
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.AbilityTalent;
    using Ability.Core.AbilityFactory.Utilities;

    using Ensage;

    /// <summary>
    ///     The SkillBook interface.
    /// </summary>
    /// <typeparam name="T">
    ///     the type of AbilitySkills
    /// </typeparam>
    public interface ISkillBook<T> : IDisposable, IAbilityUnitPart
        where T : IAbilitySkill
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the all skills.
        /// </summary>
        IReadOnlyDictionary<double, T> AllSkills { get; set; }

        /// <summary>
        ///     Gets or sets the cast point spells.
        /// </summary>
        IReadOnlyDictionary<double, T> CastPointSpells { get; set; }

        /// <summary>
        ///     Gets or sets the damage amps ordered.
        /// </summary>
        IReadOnlyDictionary<double, T> DamageAmps { get; set; }

        /// <summary>
        ///     Gets or sets the damage amps ordered for cast.
        /// </summary>
        IOrderedEnumerable<T> DamageAmpsOrderedForCast { get; set; }

        bool HasAghanim { get; }

        /// <summary>
        ///     Gets or sets a value indicating whether has blink.
        /// </summary>
        bool HasBlink { get; }

        /// <summary>
        ///     Gets or sets the items.
        /// </summary>
        IReadOnlyDictionary<double, T> Items { get; set; }

        /// <summary>Gets the skill added.</summary>
        DataProvider<T> SkillAdded { get; }

        /// <summary>Gets the skill removed.</summary>
        DataProvider<T> SkillRemoved { get; }

        /// <summary>
        ///     Gets or sets the skills.
        /// </summary>
        IReadOnlyDictionary<double, T> Skills { get; set; }

        /// <summary>
        ///     Gets or sets the skills ordered for cast.
        /// </summary>
        IOrderedEnumerable<T> SkillsOrderedForCast { get; set; }

        /// <summary>
        ///     Gets or sets the skills ordered for damage dealt.
        /// </summary>
        IOrderedEnumerable<T> SkillsOrderedForDamageDealt { get; set; }

        /// <summary>
        ///     Gets or sets the spells.
        /// </summary>
        IReadOnlyDictionary<double, T> Spells { get; set; }

        /// <summary>Gets the talent added.</summary>
        DataProvider<IAbilityTalent> TalentAdded { get; }

        /// <summary>Gets or sets the talents.</summary>
        IReadOnlyDictionary<double, IAbilityTalent> Talents { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The add skill.
        /// </summary>
        /// <param name="skill">
        ///     The skill.
        /// </param>
        void AddSkill(T skill);

        /// <summary>The add talent.</summary>
        /// <param name="talent">The talent.</param>
        void AddTalent(IAbilityTalent talent);

        /// <summary>The delete item.</summary>
        /// <param name="item">The item.</param>
        void DeleteItem(T item);

        /// <summary>The is valid.</summary>
        /// <param name="ability">The ability.</param>
        /// <returns>The <see cref="bool" />.</returns>
        bool IsValid(Ability ability);

        /// <summary>
        ///     The remove skill.
        /// </summary>
        /// <param name="skill">
        ///     The skill.
        /// </param>
        void RemoveSkill(T skill);

        #endregion
    }
}