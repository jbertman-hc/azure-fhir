using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class DirectMessageStatusDataPoco
    {
        public int DirectMessageStatusDataId { get; set; }
        public long MessageId { get; set; }
        public int ProviderId { get; set; }
        public DateTime? DateTimeSent { get; set; }
        public string MdNStatus { get; set; }
        public DateTime? MdNLastCheckedDt { get; set; }
        public bool? WasToCAttached { get; set; }
        public int PatientId { get; set; }
        public string RecordRelease { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public DirectMessageStatusDataDomain MapToDomainModel()
        {
            DirectMessageStatusDataDomain domain = new DirectMessageStatusDataDomain
            {
                DirectMessageStatusDataId = DirectMessageStatusDataId,
                MessageId = MessageId,
                ProviderId = ProviderId,
                DateTimeSent = DateTimeSent,
                MdNStatus = MdNStatus,
                MdNLastCheckedDt = MdNLastCheckedDt,
                WasToCAttached = WasToCAttached,
                PatientId = PatientId,
                RecordRelease = RecordRelease,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public DirectMessageStatusDataPoco() { }

        public DirectMessageStatusDataPoco(DirectMessageStatusDataDomain domain)
        {
            DirectMessageStatusDataId = domain.DirectMessageStatusDataId;
            MessageId = domain.MessageId;
            ProviderId = domain.ProviderId;
            DateTimeSent = domain.DateTimeSent;
            MdNStatus = domain.MdNStatus;
            MdNLastCheckedDt = domain.MdNLastCheckedDt;
            WasToCAttached = domain.WasToCAttached;
            PatientId = domain.PatientId;
            RecordRelease = domain.RecordRelease;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
