using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class InsurancePoco
    {
        public int patientID { get; set; }
        public string Insurance1 { get; set; }
        public string Insurance1_number { get; set; }
        public string Insurance2 { get; set; }
        public string Insurance2_number { get; set; }
        public string notes { get; set; }

        public InsuranceDomain MapToDomainModel()
        {
            InsuranceDomain domain = new InsuranceDomain
            {
                patientID = patientID,
                Insurance1 = Insurance1,
                Insurance1_number = Insurance1_number,
                Insurance2 = Insurance2,
                Insurance2_number = Insurance2_number,
                notes = notes
            };

            return domain;
        }

        public InsurancePoco() { }

        public InsurancePoco(InsuranceDomain domain)
        {
            patientID = domain.patientID;
            Insurance1 = domain.Insurance1;
            Insurance1_number = domain.Insurance1_number;
            Insurance2 = domain.Insurance2;
            Insurance2_number = domain.Insurance2_number;
            notes = domain.notes;
        }
    }
}