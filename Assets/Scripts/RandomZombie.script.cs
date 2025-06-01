using TheAdventure.Scripting;
using System;
using TheAdventure;

public class RandomZombie : IScript
{
    DateTimeOffset _nextZombieTimestamp;

    public void Initialize()
    {
        _nextZombieTimestamp = DateTimeOffset.UtcNow.AddSeconds(Random.Shared.Next(1, 3));
    }

    public void Execute(Engine engine)
    {
        if (_nextZombieTimestamp < DateTimeOffset.UtcNow)
        {
            _nextZombieTimestamp = DateTimeOffset.UtcNow.AddSeconds(Random.Shared.Next(1, 3));
            var playerPos = engine.GetPlayerPosition();
            var zombiePosX = playerPos.X + Random.Shared.Next(-150, 150);
            var zombiePosY = playerPos.Y + Random.Shared.Next(-150, 150);
            engine.AddZombie(zombiePosX, zombiePosY, false);
        }
    }
}