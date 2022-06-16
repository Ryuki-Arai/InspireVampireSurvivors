interface ITaskBase
{
    int Update();
}

class TaskBase : ITaskBase
{
    public int Update()
    {
        return 0;
    }
}