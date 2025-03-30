using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ListRecordReleasesPoco
    {
        public int RowID { get; set; }
        public int PatientID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string Phone { get; set; }
        public string URL { get; set; }
        public DateTime? DateOfRelease { get; set; }
        public string AuthorizationField { get; set; }
        public string Reason { get; set; }
        public string ReleasedBy { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public string Fax { get; set; }
        public int? ReasonID { get; set; }
        public string Comments { get; set; }
        public string Method { get; set; }
        public bool IsFullPatientRecord { get; set; }
        public int? ReferralId { get; set; }

        public ListRecordReleasesDomain MapToDomainModel()
        {
            ListRecordReleasesDomain domain = new ListRecordReleasesDomain
            {
                RowID = RowID,
                PatientID = PatientID,
                Name = Name,
                Address = Address,
                City = City,
                State = State,
                Zipcode = Zipcode,
                Phone = Phone,
                URL = URL,
                DateOfRelease = DateOfRelease,
                AuthorizationField = AuthorizationField,
                Reason = Reason,
                ReleasedBy = ReleasedBy,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                Fax = Fax,
                ReasonID = ReasonID,
                Comments = Comments,
                Method = Method,
                IsFullPatientRecord = IsFullPatientRecord,
                ReferralId = ReferralId
            };

            return domain;
        }

        public ListRecordReleasesPoco() { }

        public ListRecordReleasesPoco(ListRecordReleasesDomain domain)
        {
            RowID = domain.RowID;
            PatientID = domain.PatientID;
            Name = domain.Name;
            Address = domain.Address;
            City = domain.City;
            State = domain.State;
            Zipcode = domain.Zipcode;
            Phone = domain.Phone;
            URL = domain.URL;
            DateOfRelease = domain.DateOfRelease;
            AuthorizationField = domain.AuthorizationField;
            Reason = domain.Reason;
            ReleasedBy = domain.ReleasedBy;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            Fax = domain.Fax;
            ReasonID = domain.ReasonID;
            Comments = domain.Comments;
            Method = domain.Method;
            IsFullPatientRecord = domain.IsFullPatientRecord;
            ReferralId = domain.ReferralId;
        }
    }
}
