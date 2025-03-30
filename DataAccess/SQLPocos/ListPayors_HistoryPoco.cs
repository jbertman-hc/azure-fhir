using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ListPayors_HistoryPoco
    {
        public Guid ListPayorHistoryID { get; set; }
        public Guid ListPayorID { get; set; }
        public DateTime Saved { get; set; }
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
        public bool Active { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public bool AcceptsAssignment { get; set; }

        public ListPayors_HistoryDomain MapToDomainModel()
        {
            ListPayors_HistoryDomain domain = new ListPayors_HistoryDomain
            {
                ListPayorHistoryID = ListPayorHistoryID,
                ListPayorID = ListPayorID,
                Saved = Saved,
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
                Active = Active,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                AcceptsAssignment = AcceptsAssignment
            };

            return domain;
        }

        public ListPayors_HistoryPoco() { }

        public ListPayors_HistoryPoco(ListPayors_HistoryDomain domain)
        {
            ListPayorHistoryID = domain.ListPayorHistoryID;
            ListPayorID = domain.ListPayorID;
            Saved = domain.Saved;
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
            Active = domain.Active;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            AcceptsAssignment = domain.AcceptsAssignment;
        }
    }
}
