using System.Collections.Generic;
using UnityEngine;

namespace Observer
{
    public interface IObserver
    {
        void Update(Object ob);
    }

    public interface IObservable
    {
        void RegisterObserver(IObserver o);
        void RemoveObserver(IObserver o);
        void NotifyObservers();
    }

    public class EnemyObserver : IObserver
    {
        IObservable player;
        Transform self;

        public EnemyObserver(Transform self, IObservable player)
        {
            this.self = self;
            this.player = player;
        }

        public void Update(Object ob)
        {
            PlayerStats stats = (PlayerStats)(ob);
            if (stats.InRageMode)
            {
                AvoidPlayer();
                return;
            }
            FollowPlayer();
        }

        public void StopObserve()
        {
            player.RemoveObserver(this);
        }

        public void AvoidPlayer()
        {
            self.GetComponent<EnemyAI>().AvoidPlayer();
        }

        public void FollowPlayer()
        {
            self.GetComponent<EnemyAI>().FollowPlayer();
        }
    }
}

namespace AbstractFactory { 

    public enum EEnemy
    {
        Red,
        Blue
    }

    public abstract class Weapon
    {
        public ScriptableWeapon weapon;
        public abstract void Hit();
    }

    public abstract class Enemy
    {
        public ScriptableStats stats;

        public void RecieveHit(float value)
        {
            stats.Health -= value;
            if(stats.Health <= 0)
            {
                stats.IsDead = true;
                Death();
            }
        }

        public abstract void Death();
    }

    public class Fist : Weapon
    {
        public Fist(Transform origin)
        {
            weapon = Resources.instance.GetWeaponByName("Fist");
        }

        public override void Hit()
        {
            // hits the fist, no weapon shown

        }
    }

    public class Bat : Weapon
    {
        public Bat(Transform origin)
        {
            weapon = Resources.instance.GetWeaponByName("Bat");
            origin.GetComponent<WeaponInstantiator>().InstantiateWeapon();
        }

        public override void Hit()
        {
            // hits the bat, weapon shown

        }
    }

    public class RedEnemy : Enemy
    {
        public RedEnemy()
        {
            stats = Resources.instance.GetStatsByName("RedEnemy");
        }

        public override void Death()
        {
            Debug.Log("Cringanyl");
        }
    }

    public class BlueEnemy : Enemy
    {
        public BlueEnemy()
        {
            stats = Resources.instance.GetStatsByName("BlueEnemy");
        }

        public override void Death()
        {
            Debug.Log("Slovil cringe");
        }
    }

    public abstract class EnemyFactory
    {
        public abstract Enemy CreateEnemy();
        public abstract Weapon CreateWeapon(Transform origin);
    }

    public class RedEnemyFactory : EnemyFactory
    {
        public override Enemy CreateEnemy()
        {
            return new RedEnemy();
        }

        public override Weapon CreateWeapon(Transform origin)
        {
            return new Fist(origin);
        }
    }

    public class BlueEnemyFactory : EnemyFactory
    {
        public override Enemy CreateEnemy()
        {
            return new BlueEnemy();
        }

        public override Weapon CreateWeapon(Transform origin)
        {
            return new Bat(origin);
        }
    }

    public class Extentions
    {
        public static EnemyFactory GetFactoryByEnum(EEnemy enemy)
        {
            if (enemy == EEnemy.Red)
                return new RedEnemyFactory();
            else if (enemy == EEnemy.Blue)
                return new BlueEnemyFactory();
            else
                return null;
        }
    }
}