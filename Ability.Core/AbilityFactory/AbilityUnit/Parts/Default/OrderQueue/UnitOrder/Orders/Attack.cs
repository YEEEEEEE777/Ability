﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue.UnitOrder.Orders
{
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue.UnitOrder.OrderPriority;
    using Ability.Core.Utilities;

    using Ensage;
    using Ensage.Common.Extensions;

    public class Attack : UnitOrderBase
    {
        public Attack(IAbilityUnit unit)
            : base(OrderType.DealDamageToEnemy, unit, "Attack target" + unit.TargetSelector.Target.Name)
        {
            this.PrintInLog = false;
            this.ShouldExecuteFast = true;
        }
        

        public float Time { get; set; }

        public bool Attacking { get; set; }

        private bool executedOnce;

        private Sleeper sleeper = new Sleeper();

        public override bool CanExecute()
        {
            if (this.Unit.Modifiers.Disarmed || this.Unit.Modifiers.Immobile || !this.Unit.TargetSelector.TargetIsSet
                || this.Unit.TargetSelector.Target.Modifiers.AttackImmune
                || this.Unit.TargetSelector.Target.Modifiers.Invul
                || !this.Unit.TargetSelector.Target.Visibility.Visible
                || this.Unit.TargetSelector.LastDistanceToTarget > this.Unit.AttackRange.Value + 1000
                || !this.Unit.TargetSelector.Target.SourceUnit.IsAlive)
            {
                Console.WriteLine("attack canceled");
                return false;
            }

            this.Time = GlobalVariables.Time * 1000 + Game.Ping * 0.99f;
            this.Attacking = this.Unit.SourceUnit.IsAttacking();
            if (this.Attacking && this.Time > this.Unit.AttackAnimationTracker.CancelAnimationTime)
            {
                return false;
            }

            return true;
        }

        private IAbilityUnit target;

        public override void Enqueue()
        {
            this.target = this.Unit.TargetSelector.Target;
            base.Enqueue();
        }

        public override float Execute()
        {
            if (this.sleeper.Sleeping)
            {
                return 0;
            }
            
            //Console.WriteLine("Executing attack " + this.Unit.PrettyName + " target: " + this.Unit.TargetSelector.Target.PrettyName);
            this.sleeper.Sleep(300);
            this.executedOnce = true;
            this.attack();
            return 0;
        }

        public override float ExecuteFast()
        {
            if (this.executedOnce)
            {
                return 0;
            }

            if (this.sleeper.Sleeping)
            {
                return 0;
            }

            this.executedOnce = true;
            this.attack();
            return 0;
        }

        private void attack()
        {
            //if (this.Unit.TargetSelector.Target == null)
            //{
            //    return;
            //}

            this.Unit.SourceUnit.Attack(this.Unit.TargetSelector.Target.SourceUnit);
        }
    }
}
