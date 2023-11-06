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

        public IEnumerable<MinutelyView> GetMinutely()
        {
            return this.Events.Select(e => new MinutelyView(e.CreatedOnUTC.ToLocalTime(), e.Comment)).ToList();
        }

        public IEnumerable<HourlyView> GetHourly()
        {
            var groupedEventsByHour = GroupEventsByHour();

            var hourlyView = CreateHourlyViewFromGroupedEvents(groupedEventsByHour);

            return hourlyView;
        }

        private IEnumerable<HourlyView> CreateHourlyViewFromGroupedEvents(IEnumerable<IGrouping<int, dynamic>> groupedEventsByHour)
        {
            var hourlyView = new List<HourlyView>();

            foreach (var eventByHour in groupedEventsByHour)
            {
                var enters = CountEventType(eventByHour, EventTypes.Enter);
                var leaves = CountEventType(eventByHour, EventTypes.Leave);
                var peopleWhomHighFived = CountPeopleHighFived(eventByHour);
                var highFivedPeople = CountHighFivedPeople(eventByHour);
                var comments = CountEventType(eventByHour, EventTypes.Comment);

                var hour = eventByHour.Key;

                var stats = BuildStatsString(enters, leaves, peopleWhomHighFived, highFivedPeople, comments);

                hourlyView.Add(new HourlyView(hour, stats));
            }

            return hourlyView;
        }

        private IEnumerable<IGrouping<int, dynamic>> GroupEventsByHour()
        {
            return this.Events
                .Select(e => new { DateInfo = e.CreatedOnUTC.ToLocalTime(), e.EventType, e.ChatUser, e.HighFivedChatUser })
                .GroupBy(e => e.DateInfo.Hour).ToList();
        }

        private int CountEventType(IGrouping<int, dynamic> eventByHour, EventTypes eventType)
        {
            return eventByHour
                .Where(e => e.EventType == eventType)
                .Count();
        }

        private int CountPeopleHighFived(IGrouping<int, dynamic> eventByHour)
        {
            return eventByHour
                .Where(e => e.EventType == EventTypes.HighFive)
                .Select(e => e.ChatUser)
                .Distinct()
                .Count();
        }

        private int CountHighFivedPeople(IGrouping<int, dynamic> eventByHour)
        {
            return eventByHour
                .Where(e => e.EventType == EventTypes.HighFive)
                .Select(e => e.HighFivedChatUser)
                .Distinct()
                .Count();
        }

        private string BuildStatsString(int enters, int leaves, int peopleWhomHighFived, int highFivedPeople, int comments)
        {
            var stringBuilder = new StringBuilder();

            if (enters > 0)
            {
                stringBuilder.Append($"{enters} {SingularOrPlural(enters)} entered ");
            }

            if (leaves > 0)
            {
                stringBuilder.Append($"{leaves} left ");
            }

            if (peopleWhomHighFived > 0)
            {
                stringBuilder.Append($"{peopleWhomHighFived} {SingularOrPlural(peopleWhomHighFived)} high-fived {highFivedPeople} other {SingularOrPlural(highFivedPeople)} ");
            }

            if (comments > 0)
            {
                stringBuilder.Append($"{comments} comments");
            }

            return stringBuilder.ToString();
        }

        private string SingularOrPlural(int count)
        {
            return count == 1 ? "person" : "people";
        }
    }
}