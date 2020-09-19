using System;

namespace Repository
{
    public partial class EventRegistration
    {
        public int Id { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool AttendingStatus { get; set; }
        public int EventsId { get; set; }
        public int UsersId { get; set; }

        public virtual Events Events { get; set; }
        public virtual Users Users { get; set; }
    }
}