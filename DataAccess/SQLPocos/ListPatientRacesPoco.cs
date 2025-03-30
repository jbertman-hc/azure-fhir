using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ListPatientRacesPoco
    {
        public Guid ListPatientRacesId { get; set; }
        public int PatientID { get; set; }
        public int RaceID { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public ListPatientRacesDomain MapToDomainModel()
        {
            ListPatientRacesDomain domain = new ListPatientRacesDomain
            {
                ListPatientRacesId = ListPatientRacesId,
                PatientID = PatientID,
                RaceID = RaceID,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public ListPatientRacesPoco() { }

        public ListPatientRacesPoco(ListPatientRacesDomain domain)
        {
            ListPatientRacesId = domain.ListPatientRacesId;
            PatientID = domain.PatientID;
            RaceID = domain.RaceID;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
