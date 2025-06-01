using Silk.NET.Maths;

namespace TheAdventure.Models;

public class ZombieObject : RenderableGameObject
{
    private const int _speed = 96; // pixels per second

    public enum ZombieStateDirection
    {
        None = 0,
        Down,
        Up,
        Left,
        Right,
    }

    public enum ZombieState
    {
        None = 0,
        Move
    }

    public (ZombieState State, ZombieStateDirection Direction) State { get; private set; }

    public ZombieObject(SpriteSheet spriteSheet, int x, int y) : base(spriteSheet, (x, y))
    {
        SetState(ZombieState.Move, ZombieStateDirection.Down);
    }

    public void SetState(ZombieState state, ZombieStateDirection direction)
    {
        if (State.State == state && State.Direction == direction)
        {
            return;
        }

        if (state == ZombieState.None && direction == ZombieStateDirection.None)
        {
            SpriteSheet.ActivateAnimation(null);
        }
        else
        {
            var animationName = Enum.GetName(state) + Enum.GetName(direction);
            SpriteSheet.ActivateAnimation(animationName);
        }

        State = (state, direction);
    }

    public void UpdatePosition(double playerX, double playerY, double time)
    {

        var pixelsToMove = _speed * (time / 1000.0);


        var newState = State.State;
        var newDirection = State.Direction;
        int x = Position.X;
        int y = Position.Y;

        if (Math.Abs(playerX - Position.X) > Math.Abs(playerY - Position.Y))
        {
            if (playerX < Position.X)
            {
                newDirection = ZombieStateDirection.Left;
                x = Position.X - (int)(pixelsToMove);
            }
            else
            {
                newDirection = ZombieStateDirection.Right;
                x = Position.X + (int)(pixelsToMove);
            }
        }
        else
        {
            if (playerY < Position.Y)
            {
                newDirection = ZombieStateDirection.Up;
                y = Position.Y - (int)(pixelsToMove);
            }
            else
            {
                newDirection = ZombieStateDirection.Down;
                y = Position.Y + (int)(pixelsToMove);
            }
        }

        if (newState != State.State || newDirection != State.Direction)
        {
            SetState(newState, newDirection);
        }

        Position = (x, y);
    }
}