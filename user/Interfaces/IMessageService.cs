namespace user.Interfaces
{
    public interface IMessageService
    {
        bool Enqueue(int userId, bool visibility);
    }
}