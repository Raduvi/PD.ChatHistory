namespace PD.ChatHistory.Domain.Entities.Chatroom
{
    public record MinutelyView
    {
        public MinutelyView(DateTime dateTime, string comment)
        {
            DateInfo = dateTime;
            Comment = comment;
        }

        public DateTime DateInfo { get; private set; }

        public string Comment { get; private set; } = string.Empty;
    }
}
