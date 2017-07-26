﻿// <copyright file="IAbilityUnitHeroComposer.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Composer
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///     The AbilityUnitComposer interface.
    /// </summary>
    public interface IAbilityUnitHeroComposer
    {
        #region Public Properties

        /// <summary>Gets the assignments.</summary>
        IDictionary<Type, Action<IAbilityUnit>> Assignments { get; }

        #endregion

        #region Public Methods and Operators

        void AssignPart<T>(Func<IAbilityUnit, T> factory) where T : IAbilityUnitPart;

        /// <summary>
        ///     The compose.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        void Compose(IAbilityUnit unit);

        #endregion
    }
}