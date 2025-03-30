using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class LatestVitalsTemporaryPoco
    {
        public int VitalsId { get; set; }
        public int PatientID { get; set; }
        public int? Systolic { get; set; }
        public int? Diastolic { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public LatestVitalsTemporaryDomain MapToDomainModel()
        {
            LatestVitalsTemporaryDomain domain = new LatestVitalsTemporaryDomain
            {
                VitalsId = VitalsId,
                PatientID = PatientID,
                Systolic = Systolic,
                Diastolic = Diastolic,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public LatestVitalsTemporaryPoco() { }

        public LatestVitalsTemporaryPoco(LatestVitalsTemporaryDomain domain)
        {
            VitalsId = domain.VitalsId;
            PatientID = domain.PatientID;
            Systolic = domain.Systolic;
            Diastolic = domain.Diastolic;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
