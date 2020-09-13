namespace SimpleRPG.Core
{
    interface IDrawable
    {
        string GetGraphics();
        int GetDrawingPriority();

        Point GetPosition();
    }
}
