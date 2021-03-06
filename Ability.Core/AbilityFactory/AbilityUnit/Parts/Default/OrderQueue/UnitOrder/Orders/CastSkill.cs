﻿// <copyright file="CastSkill.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue.UnitOrder.Orders
{
    using System;

    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue.UnitOrder.OrderPriority;
    using Ability.Core.Utilities;

    using Ensage;

    using SharpDX;

    public class CastSkill : UnitOrderBase
    {
        #region Constructors and Destructors

        public CastSkill(OrderType orderType, IAbilitySkill skill, Func<bool> executeFunction)
            : base(orderType, skill.Owner, "Cast skill " + skill.Name)
        {
            this.Skill = skill;
            this.ExecutionInterval = this.Skill.IsItem ? 250 : (float)(this.Skill.CastData.CastPoint * 250);
            this.ExecuteAction = executeFunction;
            this.Color = Color.GreenYellow;
        }

        #endregion

        #region Public Properties

        public Func<bool> ExecuteAction { get; }

        public float ExecutionInterval { get; set; } = 100;

        public IAbilitySkill Skill { get; }

        public Sleeper Sleeper { get; set; } = new Sleeper();

        #endregion

        #region Public Methods and Operators

        public override bool CanExecute()
        {
            if (!this.Skill.CastFunction.CanCast())
            {
                if (this.executed)
                {
                    this.Skill.CastSleeper.Sleep(500 + Game.Ping);
                }

                return false;
            }

            if (!this.Skill.CastFunction.TargetIsValid(this.Skill.Owner.TargetSelector.Target))
            {
                this.Skill.Owner.SourceUnit.Stop();

                return false;
            }

            return true;
        }

        public override void Dequeue()
        {
            this.Skill.CastData.Queued = false;
        }

        public override void Enqueue()
        {
            this.Skill.CastData.Queued = true;
            //this.target = this.Skill.Owner.TargetSelector.Target;
        }

        private bool executed;

        //private IAbilityUnit target;

        public override float Execute()
        {
            if (this.Sleeper.Sleeping)
            {
                return 0;
            }

            if (this.ExecuteAction())
            {
                this.executed = true;
                if (this.Skill.AbilityInfo.IsDisable)
                {
                    this.Skill.Owner.TargetSelector.Target.DisableManager.CastingDisable(this.Skill.HitDelay.Get());
                }

                this.Sleeper.Sleep(this.ExecutionInterval);
            }

            return 0f;
        }

        #endregion
    }
}