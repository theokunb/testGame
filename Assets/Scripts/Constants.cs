using System.IO;

public static class Constants
{
    public static class Prefs
    {
        public static string Highscore = nameof(Highscore);
    }

    public static class SceneIndex
    {
        public static int MenuScene = 0;
        public static int GameScene = 1;
    }

    public static class Animation
    {
        public static string Speed = nameof(Speed);

        public static string SpeedX = nameof(SpeedX);
        public static string SpeedY = nameof(SpeedY);

        public static int BaseLayerIndex = 0;
        public static int LegsLayer = 1;
        public static int PistolLayer = 2;
        public static int GranadeLauncherLayer = 3;

        public static string Die = nameof(Die);
    }

    public static class Prefabs
    {
        public static class Weapon
        {
            public static string AutoGun = Path.Combine("Prefabs", "Weapon", "AutoGun");
            public static string Pistol = Path.Combine("Prefabs", "Weapon", "Pistol");
            public static string Shotgun = Path.Combine("Prefabs", "Weapon", "Shotgun");
        }

        public static class Missile
        {
            public static string Bullet = Path.Combine("Prefabs", "Missile", "Bullet");
            public static string Granade = Path.Combine("Prefabs", "Missile", "Granade");
        }

        public static class Bonus
        {
            public static string Pistol = Path.Combine("Prefabs", "Bonus", "Pistol bonus");
            public static string AutoGun = Path.Combine("Prefabs", "Bonus", "AutoGun bonus");
            public static string Shotgun = Path.Combine("Prefabs", "Bonus", "Shotgun bonus");
            public static string GranadeLauncher = Path.Combine("Prefabs", "Bonus", "GranadeLauncher bonus");

            public static string Haste = Path.Combine("Prefabs", "Bonus", "HasteBuff");
            public static string Invincible = Path.Combine("Prefabs", "Bonus", "InvincibleBuff");

        }

        public static string EnemyContainer = Path.Combine("Prefabs", "Enemy", "New enemy container");
        public static string WeaponContainer = Path.Combine("Prefabs", "Weapon", "new weapon container");
    }
}
