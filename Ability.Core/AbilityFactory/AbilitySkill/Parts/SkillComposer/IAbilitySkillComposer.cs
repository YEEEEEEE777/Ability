﻿// <copyright file="IAbilitySkillComposer.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.SkillComposer
{
    using System;

    /// <summary>
    ///     The AbilitySkillComposer interface.
    /// </summary>
    public interface IAbilitySkillComposer
    {
        #region Public Methods and Operators

        /// <summary>The assign part.</summary>
        /// <param name="factory">The factory.</param>
        /// <typeparam name="T"></typeparam>
        void AssignPart<T>(Func<IAbilitySkill, T> factory) where T : IAbilitySkillPart;

        /// <summary>
        ///     The compose.
        /// </summary>
        /// <param name="skill">
        ///     The skill.
        /// </param>
        void Compose(IAbilitySkill skill);

        #endregion
    }
}