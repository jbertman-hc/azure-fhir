using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class SpecialtyQuickCodesPoco
    {
        public Guid QuickCodeID { get; set; }
        public int? SpecialtyID { get; set; }
        public string QuickCode { get; set; }
        public string CodeDesc { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public SpecialtyQuickCodesDomain MapToDomainModel()
        {
            SpecialtyQuickCodesDomain domain = new SpecialtyQuickCodesDomain
            {
                QuickCodeID = QuickCodeID,
                SpecialtyID = SpecialtyID,
                QuickCode = QuickCode,
                CodeDesc = CodeDesc,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public SpecialtyQuickCodesPoco() { }

        public SpecialtyQuickCodesPoco(SpecialtyQuickCodesDomain domain)
        {
            QuickCodeID = domain.QuickCodeID;
            SpecialtyID = domain.SpecialtyID;
            QuickCode = domain.QuickCode;
            CodeDesc = domain.CodeDesc;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
