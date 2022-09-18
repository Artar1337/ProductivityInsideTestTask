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
        Transform self;

        public EnemyObserver(Transform self)
        {
            this.self = self;
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
        protected float health;
        private bool isDead = false;
        public bool IsDead { get => isDead; }

        public void RecieveHit(float value)
        {
            if (isDead)
                return;
            health -= value;
            Debug.Log("enemy's hp: " + health);
            if (health <= 0)
            {
                isDead = true;
                Death();
            }
        }

        public abstract void Death();
    }

    public class Fist : Weapon
    {
        Transform self;
        public Fist(Transform origin)
        {
            weapon = Resources.instance.GetWeaponByName("Fist");
            self = origin;
        }

        public override void Hit()
        {
            // hits the fist, no weapon shown
            // no swoosh...
            GameObject.Instantiate(weapon.hitParticles, self.transform.position, self.transform.rotation);
        }
    }

    public class Bat : Weapon
    {
        Transform self;
        WeaponInstantiator weaponHandler;
        public Bat(Transform origin)
        {
            weapon = Resources.instance.GetWeaponByName("Bat");
            weaponHandler = origin.GetComponent<WeaponInstantiator>();
            weaponHandler.InstantiateWeapon();
            self = origin;
        }

        public override void Hit()
        {
            // hits the bat, weapon shown
            weaponHandler.WeaponSwoosh();
            GameObject.Instantiate(weapon.hitParticles, self.transform.position, self.transform.rotation);
        }
    }

    public class RedEnemy : Enemy
    {
        Transform self;
        public RedEnemy(Transform self)
        {
            this.self = self;
            stats = Resources.instance.GetStatsByName("RedEnemy");
            health = stats.Health;
        }

        public override void Death()
        {
            Debug.Log("Cringanyl");
            self.GetComponent<EnemyAI>().StopObserve();
            MonoBehaviour.Destroy(self.gameObject);
        }
    }

    public class BlueEnemy : Enemy
    {
        Transform self;
        public BlueEnemy(Transform self)
        {
            this.self = self;
            stats = Resources.instance.GetStatsByName("BlueEnemy");
            health = stats.Health;
        }

        public override void Death()
        {
            Debug.Log("Slovil cringe");
            self.GetComponent<EnemyAI>().StopObserve();
            MonoBehaviour.Destroy(self.gameObject);
        }
    }

    public abstract class EnemyFactory
    {
        public abstract Enemy CreateEnemy(Transform origin);
        public abstract Weapon CreateWeapon(Transform origin);
    }

    public class RedEnemyFactory : EnemyFactory
    {
        public override Enemy CreateEnemy(Transform origin)
        {
            return new RedEnemy(origin);
        }

        public override Weapon CreateWeapon(Transform origin)
        {
            return new Fist(origin);
        }
    }

    public class BlueEnemyFactory : EnemyFactory
    {
        public override Enemy CreateEnemy(Transform origin)
        {
            return new BlueEnemy(origin);
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