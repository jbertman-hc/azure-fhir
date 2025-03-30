using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class VISInformationPoco
    {
        public int VISID { get; set; }
        public int ListHMID { get; set; }
        public string VISName { get; set; }
        public string VISVersion { get; set; }
        public DateTime? VISDateGiven { get; set; }
        public string CVXCode { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public VISInformationDomain MapToDomainModel()
        {
            VISInformationDomain domain = new VISInformationDomain
            {
                VISID = VISID,
                ListHMID = ListHMID,
                VISName = VISName,
                VISVersion = VISVersion,
                VISDateGiven = VISDateGiven,
                CVXCode = CVXCode,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public VISInformationPoco() { }

        public VISInformationPoco(VISInformationDomain domain)
        {
            VISID = domain.VISID;
            ListHMID = domain.ListHMID;
            VISName = domain.VISName;
            VISVersion = domain.VISVersion;
            VISDateGiven = domain.VISDateGiven;
            CVXCode = domain.CVXCode;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
