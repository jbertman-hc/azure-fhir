using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class AppInfoPoco
    {
        public int AppID { get; set; }
        public string AppName { get; set; }
        public string AppVersion { get; set; }
        public DateTime AppInstallDateTime { get; set; }
        public string AppInstallUser { get; set; }
        public string AppComment { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public AppInfoDomain MapToDomainModel()
        {
            AppInfoDomain domain = new AppInfoDomain
            {
                AppID = AppID,
                AppName = AppName,
                AppVersion = AppVersion,
                AppInstallDateTime = AppInstallDateTime,
                AppInstallUser = AppInstallUser,
                AppComment = AppComment,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public AppInfoPoco() { }

        public AppInfoPoco(AppInfoDomain domain)
        {
            AppID = domain.AppID;
            AppName = domain.AppName;
            AppVersion = domain.AppVersion;
            AppInstallDateTime = domain.AppInstallDateTime;
            AppInstallUser = domain.AppInstallUser;
            AppComment = domain.AppComment;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
