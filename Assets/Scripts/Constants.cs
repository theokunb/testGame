using System.IO;

public static class Constants
{
    public static class Animation
    {
        public static string SpeedX = nameof(SpeedX);
        public static string SpeedY = nameof(SpeedY);

        public static int BaseLayerIndex = 0;
        public static int LegsLayer = 1;
        public static int PistolLayer = 2;
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
        }
    }
}
