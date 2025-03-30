using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ListPmAccountsPoco
    {
        public int PmAccountId { get; set; }
        public int PatientID { get; set; }
        public string AccountId { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string AccountSeqNo { get; set; }
        public string PatientRelToRespParty { get; set; }
        public string RespPartyNo { get; set; }
        public string RespPartySalutation { get; set; }
        public string RespPartyFirstName { get; set; }
        public string RespPartyMiddleName { get; set; }
        public string RespPartyLastName { get; set; }
        public string RespPartySuffix { get; set; }
        public string RespPartyAddressLine1 { get; set; }
        public string RespPartyAddressLine2 { get; set; }
        public string RespPartyCity { get; set; }
        public string RespPartyState { get; set; }
        public string RespPartyZip { get; set; }
        public string RespPartyPhone { get; set; }
        public DateTime? RespPartyDob { get; set; }
        public string RespPartySex { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? ExpirationDate { get; set; }

        public ListPmAccountsDomain MapToDomainModel()
        {
            ListPmAccountsDomain domain = new ListPmAccountsDomain
            {
                PmAccountId = PmAccountId,
                PatientID = PatientID,
                AccountId = AccountId,
                AccountNumber = AccountNumber,
                AccountName = AccountName,
                AccountSeqNo = AccountSeqNo,
                PatientRelToRespParty = PatientRelToRespParty,
                RespPartyNo = RespPartyNo,
                RespPartySalutation = RespPartySalutation,
                RespPartyFirstName = RespPartyFirstName,
                RespPartyMiddleName = RespPartyMiddleName,
                RespPartyLastName = RespPartyLastName,
                RespPartySuffix = RespPartySuffix,
                RespPartyAddressLine1 = RespPartyAddressLine1,
                RespPartyAddressLine2 = RespPartyAddressLine2,
                RespPartyCity = RespPartyCity,
                RespPartyState = RespPartyState,
                RespPartyZip = RespPartyZip,
                RespPartyPhone = RespPartyPhone,
                RespPartyDob = RespPartyDob,
                RespPartySex = RespPartySex,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                EffectiveDate = EffectiveDate,
                ExpirationDate = ExpirationDate
            };

            return domain;
        }

        public ListPmAccountsPoco() { }

        public ListPmAccountsPoco(ListPmAccountsDomain domain)
        {
            PmAccountId = domain.PmAccountId;
            PatientID = domain.PatientID;
            AccountId = domain.AccountId;
            AccountNumber = domain.AccountNumber;
            AccountName = domain.AccountName;
            AccountSeqNo = domain.AccountSeqNo;
            PatientRelToRespParty = domain.PatientRelToRespParty;
            RespPartyNo = domain.RespPartyNo;
            RespPartySalutation = domain.RespPartySalutation;
            RespPartyFirstName = domain.RespPartyFirstName;
            RespPartyMiddleName = domain.RespPartyMiddleName;
            RespPartyLastName = domain.RespPartyLastName;
            RespPartySuffix = domain.RespPartySuffix;
            RespPartyAddressLine1 = domain.RespPartyAddressLine1;
            RespPartyAddressLine2 = domain.RespPartyAddressLine2;
            RespPartyCity = domain.RespPartyCity;
            RespPartyState = domain.RespPartyState;
            RespPartyZip = domain.RespPartyZip;
            RespPartyPhone = domain.RespPartyPhone;
            RespPartyDob = domain.RespPartyDob;
            RespPartySex = domain.RespPartySex;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            EffectiveDate = domain.EffectiveDate;
            ExpirationDate = domain.ExpirationDate;
        }
    }
}
