using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ListPayorsPoco
    {
        public Guid ListPayorID { get; set; }
        public int? PatientID { get; set; }
        public Guid? PayorsID { get; set; }
        public Guid? SubscriberID { get; set; }
        public Guid? GuarantorID { get; set; }
        public int? InsuranceType { get; set; }
        public string SubMemberNo { get; set; }
        public string PtMemberNo { get; set; }
        public string GroupNo { get; set; }
        public string GroupName { get; set; }
        public decimal? CoPay { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Notes { get; set; }
        public int? MedicareSecondaryCode { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public bool Active { get; set; }
        public bool AcceptsAssignment { get; set; }

        public ListPayorsDomain MapToDomainModel()
        {
            ListPayorsDomain domain = new ListPayorsDomain
            {
                ListPayorID = ListPayorID,
                PatientID = PatientID,
                PayorsID = PayorsID,
                SubscriberID = SubscriberID,
                GuarantorID = GuarantorID,
                InsuranceType = InsuranceType,
                SubMemberNo = SubMemberNo,
                PtMemberNo = PtMemberNo,
                GroupNo = GroupNo,
                GroupName = GroupName,
                CoPay = CoPay,
                StartDate = StartDate,
                EndDate = EndDate,
                Notes = Notes,
                MedicareSecondaryCode = MedicareSecondaryCode,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                Active = Active,
                AcceptsAssignment = AcceptsAssignment
            };

            return domain;
        }

        public ListPayorsPoco() { }

        public ListPayorsPoco(ListPayorsDomain domain)
        {
            ListPayorID = domain.ListPayorID;
            PatientID = domain.PatientID;
            PayorsID = domain.PayorsID;
            SubscriberID = domain.SubscriberID;
            GuarantorID = domain.GuarantorID;
            InsuranceType = domain.InsuranceType;
            SubMemberNo = domain.SubMemberNo;
            PtMemberNo = domain.PtMemberNo;
            GroupNo = domain.GroupNo;
            GroupName = domain.GroupName;
            CoPay = domain.CoPay;
            StartDate = domain.StartDate;
            EndDate = domain.EndDate;
            Notes = domain.Notes;
            MedicareSecondaryCode = domain.MedicareSecondaryCode;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            Active = domain.Active;
            AcceptsAssignment = domain.AcceptsAssignment;
        }
    }
}
