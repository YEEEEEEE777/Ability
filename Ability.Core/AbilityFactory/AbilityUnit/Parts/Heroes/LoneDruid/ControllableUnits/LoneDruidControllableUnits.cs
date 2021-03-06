﻿// <copyright file="LoneDruidControllableUnits.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.LoneDruid.ControllableUnits
{
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.LocalHero.ControllableUnits;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Units.SpiritBear.SkillBook;
    using Ability.Core.AbilityFactory.Utilities;

    public class LoneDruidControllableUnits : ControllableUnits
    {
        #region Constructors and Destructors

        public LoneDruidControllableUnits(IAbilityUnit unit)
            : base(unit)
        {
        }

        #endregion

        public IAbilityUnit Bear { get; set; }

        public Notifier BearAdded { get; } = new Notifier();

        #region Public Methods and Operators

        public override void UnitAdded(IAbilityUnit unit)
        {
            if (unit.SkillBook is SpiritBearSkillBook)
            {
                this.Bear = unit;
                this.Bear.Owner = this.Unit;
                this.BearAdded.Notify();
                return;
            }

            base.UnitAdded(unit);
        }

        public override void UnitRemoved(IAbilityUnit unit)
        {
            if (unit.SkillBook is SpiritBearSkillBook)
            {
                this.Bear = null;
                return;
            }

            base.UnitRemoved(unit);
        }

        #endregion
    }
}