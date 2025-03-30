using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class HmRuleUserPermissionsPoco
    {
        public int HmRuleUserPermissionId { get; set; }
        public Guid HmRuleGUID { get; set; }
        public int UserPermission { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public HmRuleUserPermissionsDomain MapToDomainModel()
        {
            HmRuleUserPermissionsDomain domain = new HmRuleUserPermissionsDomain
            {
                HmRuleUserPermissionId = HmRuleUserPermissionId,
                HmRuleGUID = HmRuleGUID,
                UserPermission = UserPermission,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public HmRuleUserPermissionsPoco() { }

        public HmRuleUserPermissionsPoco(HmRuleUserPermissionsDomain domain)
        {
            HmRuleUserPermissionId = domain.HmRuleUserPermissionId;
            HmRuleGUID = domain.HmRuleGUID;
            UserPermission = domain.UserPermission;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
