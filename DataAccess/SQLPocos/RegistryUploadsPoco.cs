using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class RegistryUploadsPoco
    {
        public int ListHMID { get; set; }
        public int InterfaceID { get; set; }
        public DateTime? DateSentToRegistry { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public string LastTouchedBy { get; set; }

        public RegistryUploadsDomain MapToDomainModel()
        {
            RegistryUploadsDomain domain = new RegistryUploadsDomain
            {
                ListHMID = ListHMID,
                InterfaceID = InterfaceID,
                DateSentToRegistry = DateSentToRegistry,
                DateLastTouched = DateLastTouched,
                DateRowAdded = DateRowAdded,
                LastTouchedBy = LastTouchedBy
            };

            return domain;
        }

        public RegistryUploadsPoco() { }

        public RegistryUploadsPoco(RegistryUploadsDomain domain)
        {
            ListHMID = domain.ListHMID;
            InterfaceID = domain.InterfaceID;
            DateSentToRegistry = domain.DateSentToRegistry;
            DateLastTouched = domain.DateLastTouched;
            DateRowAdded = domain.DateRowAdded;
            LastTouchedBy = domain.LastTouchedBy;
        }
    }
}
