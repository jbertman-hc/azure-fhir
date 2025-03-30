using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ECouponCurrentProgramsPoco
    {
        public int ECouponCurrentProgramsId { get; set; }
        public int ScriptId { get; set; }
        public string ProgramId { get; set; }
        public string TransactionId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool Paid { get; set; }
        public string Image { get; set; }
        public string PaymentNotes { get; set; }
        public string Bin { get; set; }
        public string PCN { get; set; }
        public string Group { get; set; }
        public string CardholderId { get; set; }
        public DateTime? DateLastPrinted { get; set; }
        public DateTime? DateProgramConfirmed { get; set; }
        public string PharmacyCode { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public string ProgramType { get; set; }

        public ECouponCurrentProgramsDomain MapToDomainModel()
        {
            ECouponCurrentProgramsDomain domain = new ECouponCurrentProgramsDomain
            {
                ECouponCurrentProgramsId = ECouponCurrentProgramsId,
                ScriptId = ScriptId,
                ProgramId = ProgramId,
                TransactionId = TransactionId,
                Name = Name,
                Type = Type,
                Paid = Paid,
                Image = Image,
                PaymentNotes = PaymentNotes,
                Bin = Bin,
                PCN = PCN,
                Group = Group,
                CardholderId = CardholderId,
                DateLastPrinted = DateLastPrinted,
                DateProgramConfirmed = DateProgramConfirmed,
                PharmacyCode = PharmacyCode,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                ProgramType = ProgramType
            };

            return domain;
        }

        public ECouponCurrentProgramsPoco() { }

        public ECouponCurrentProgramsPoco(ECouponCurrentProgramsDomain domain)
        {
            ECouponCurrentProgramsId = domain.ECouponCurrentProgramsId;
            ScriptId = domain.ScriptId;
            ProgramId = domain.ProgramId;
            TransactionId = domain.TransactionId;
            Name = domain.Name;
            Type = domain.Type;
            Paid = domain.Paid;
            Image = domain.Image;
            PaymentNotes = domain.PaymentNotes;
            Bin = domain.Bin;
            PCN = domain.PCN;
            Group = domain.Group;
            CardholderId = domain.CardholderId;
            DateLastPrinted = domain.DateLastPrinted;
            DateProgramConfirmed = domain.DateProgramConfirmed;
            PharmacyCode = domain.PharmacyCode;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            ProgramType = domain.ProgramType;
        }
    }
}
