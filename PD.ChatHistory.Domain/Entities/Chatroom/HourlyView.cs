namespace PD.ChatHistory.Domain.Entities.Chatroom
{
    public record HourlyView
    {
        public HourlyView(int hour, string info)
        {
            Hour = hour;
            Info = info;
        }

        public int Hour { get; private set; }

        public string Info { get; private set; } = string.Empty;
    }
}
