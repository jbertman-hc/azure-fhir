﻿using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class GuardianPatientsPoco
    {
        public Guid GuardianPatientsID { get; set; }
        public int PatientID { get; set; }
        public int OtherPatientID { get; set; }
        public int? RelationID { get; set; }
        public bool IsPrimaryGuardian { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public string Comments { get; set; }

        public GuardianPatientsDomain MapToDomainModel()
        {
            GuardianPatientsDomain domain = new GuardianPatientsDomain
            {
                GuardianPatientsID = GuardianPatientsID,
                PatientID = PatientID,
                OtherPatientID = OtherPatientID,
                RelationID = RelationID,
                IsPrimaryGuardian = IsPrimaryGuardian,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                Comments = Comments
            };

            return domain;
        }

        public GuardianPatientsPoco() { }

        public GuardianPatientsPoco(GuardianPatientsDomain domain)
        {
            GuardianPatientsID = domain.GuardianPatientsID;
            PatientID = domain.PatientID;
            OtherPatientID = domain.OtherPatientID;
            RelationID = domain.RelationID;
            IsPrimaryGuardian = domain.IsPrimaryGuardian;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            Comments = domain.Comments;
        }
    }
}
