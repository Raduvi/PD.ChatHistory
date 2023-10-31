using PD.ChatHistory.Domain.Entities.ChatroomEvent;
using PD.ChatHistory.Domain.Entities.Common;
using PD.ChatHistory.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PD.ChatHistory.Domain.Entities.Chatroom
{
    public class ChatRoom : Entity
    {
        [MaxLength(150)]
        public string Name { get; private set; } = string.Empty;

        [MaxLength(450)]
        public string Description { get; private set; } = string.Empty;

        public IEnumerable<ChatRoomEvent> Events { get; private set; } = new List<ChatRoomEvent>();

        public ChatRoom Create(
            int id,
            string name,
            string description,
            int createdBy,
            int updatedBy,
            DateTime createdOnUTC,
            DateTime updatedOnUTC
            )
        {
            return new ChatRoom()
            {
                Id = id,
                Name = name,
                Description = description,
                CreatedBy = createdBy,
                UpdatedBy = updatedBy,
                CreatedOnUTC = createdOnUTC,
                UpdatedOnUTC = updatedOnUTC
            };
        }

        public List<MinutelyView> GetMinutely()
        {
            return this.Events.Select(e => new MinutelyView(e.CreatedOnUTC.ToLocalTime(), e.Comment)).ToList();
        }

        public List<HourlyView> GetHourly()
        {
            var hourlyView = new List<HourlyView>();

            var groupedEventsByHour = this.Events
                .Select(e => new { DateInfo = e.CreatedOnUTC.ToLocalTime(), e.EventType, e.ChatUser, e.HighFivedChatUser })
                .GroupBy(e => e.DateInfo.Hour).ToList();

            foreach (var eventByHour in groupedEventsByHour)
            {
                var enters = eventByHour
                    .Where(e => e.EventType == EventTypes.Enter)
                    .Count();
                var leaves = eventByHour
                    .Where(e => e.EventType == EventTypes.Leave)
                    .Count();
                var peopleWhomHighFived = eventByHour
                    .Where(e => e.EventType == EventTypes.HighFive)
                    .Select(e => e.ChatUser)
                    .Distinct()
                    .Count();
                var highFivedPeople = eventByHour
                    .Where(e => e.EventType == EventTypes.HighFive)
                    .Select(e => e.HighFivedChatUser)
                    .Distinct()
                    .Count();
                var comments = eventByHour
                    .Where(e => e.EventType == EventTypes.Comment)
                    .Count();

                var hour = eventByHour.Key;

                var stringBuilder = new StringBuilder();

                if (enters > 0)
                {
                    stringBuilder.Append($"{enters} {SingularOrPlurar(enters)} entered ");
                }

                if (leaves > 0)
                {
                    stringBuilder.Append($"{leaves} left ");
                }

                if (peopleWhomHighFived > 0)
                {
                    stringBuilder.Append($"{peopleWhomHighFived} {SingularOrPlurar(peopleWhomHighFived)} high-fived {highFivedPeople} other {SingularOrPlurar(highFivedPeople)} ");
                }

                if (comments > 0)
                {
                    stringBuilder.Append($"{comments} comments");
                }


                var stats = stringBuilder.ToString();

                hourlyView.Add(new HourlyView(hour, stats));
            }

            return hourlyView;
        }

        private string SingularOrPlurar(int numberOfPeople)
        {
            if (numberOfPeople == 1) return "person";
            if (numberOfPeople > 1) return "people";

            return "person";
        }
    }
}