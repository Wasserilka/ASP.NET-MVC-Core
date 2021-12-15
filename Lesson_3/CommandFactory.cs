namespace Lesson_3
{
    interface ICommandFactory 
    {
        MessageCommand CreateCommand(string Message); 
    }

    internal class CommandFactory : ICommandFactory
    {
        public MessageCommand CreateCommand(string Message)
        {
            return new MessageCommand(Message);
        }
    }
}
