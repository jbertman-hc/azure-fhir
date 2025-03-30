using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ListSmokingStatusesPoco
    {
        public int SmokingStatusId { get; set; }
        public int PatientId { get; set; }
        public string TobaccoCDCCode { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? PendingFlag { get; set; }
        public DateTime? ImportedDate { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public ListSmokingStatusesDomain MapToDomainModel()
        {
            ListSmokingStatusesDomain domain = new ListSmokingStatusesDomain
            {
                SmokingStatusId = SmokingStatusId,
                PatientId = PatientId,
                TobaccoCDCCode = TobaccoCDCCode,
                StartDate = StartDate,
                EndDate = EndDate,
                PendingFlag = PendingFlag,
                ImportedDate = ImportedDate,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public ListSmokingStatusesPoco() { }

        public ListSmokingStatusesPoco(ListSmokingStatusesDomain domain)
        {
            SmokingStatusId = domain.SmokingStatusId;
            PatientId = domain.PatientId;
            TobaccoCDCCode = domain.TobaccoCDCCode;
            StartDate = domain.StartDate;
            EndDate = domain.EndDate;
            PendingFlag = domain.PendingFlag;
            ImportedDate = domain.ImportedDate;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
