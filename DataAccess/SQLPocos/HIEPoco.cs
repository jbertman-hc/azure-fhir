using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class HIEPoco
    {
        public int RowID { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public string PageTitle { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public bool? GlobalLogin { get; set; }

        public HIEDomain MapToDomainModel()
        {
            HIEDomain domain = new HIEDomain
            {
                RowID = RowID,
                Name = Name,
                URL = URL,
                PageTitle = PageTitle,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                GlobalLogin = GlobalLogin
            };

            return domain;
        }

        public HIEPoco() { }

        public HIEPoco(HIEDomain domain)
        {
            RowID = domain.RowID;
            Name = domain.Name;
            URL = domain.URL;
            PageTitle = domain.PageTitle;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            GlobalLogin = domain.GlobalLogin;
        }
    }
}
