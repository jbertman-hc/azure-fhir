using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ListHMsPoco
    {
        public int ListHMID { get; set; }
        public int PatientID { get; set; }
        public string HMruleName { get; set; }
        public int? HmRuleID { get; set; }
        public int? VaccineID { get; set; }
        public string VaccineName { get; set; }
        public int? CompositeVaccineID { get; set; }
        public string LotNo { get; set; }
        public DateTime? DateGiven { get; set; }
        public string RecordedBy { get; set; }
        public string Volume { get; set; }
        public string Route { get; set; }
        public string Site { get; set; }
        public string Manufacturer { get; set; }
        public DateTime? Expiration { get; set; }
        public string Comment { get; set; }
        public string Sequence { get; set; }
        public string Result { get; set; }
        public string Type { get; set; }
        public string CPT { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public bool? IsGivenElsewhere { get; set; }
        public bool? PatientRefused { get; set; }
        public string VISname { get; set; }
        public string VISversion { get; set; }
        public DateTime? VISDateGiven { get; set; }
        public bool? Deleted { get; set; }
        public DateTime? DateSentToRegistry { get; set; }
        public bool? PatientParentRefused { get; set; }
        public bool? PatientHadInfection { get; set; }
        public int? HowMigrated { get; set; }
        public string Reaction { get; set; }
        public DateTime? ReactionDate { get; set; }
        public string Locations { get; set; }
        public string NIP001 { get; set; }
        public Guid? ImmunizationId { get; set; }

        public ListHMsDomain MapToDomainModel()
        {
            ListHMsDomain domain = new ListHMsDomain
            {
                ListHMID = ListHMID,
                PatientID = PatientID,
                HMruleName = HMruleName,
                HmRuleID = HmRuleID,
                VaccineID = VaccineID,
                VaccineName = VaccineName,
                CompositeVaccineID = CompositeVaccineID,
                LotNo = LotNo,
                DateGiven = DateGiven,
                RecordedBy = RecordedBy,
                Volume = Volume,
                Route = Route,
                Site = Site,
                Manufacturer = Manufacturer,
                Expiration = Expiration,
                Comment = Comment,
                Sequence = Sequence,
                Result = Result,
                Type = Type,
                CPT = CPT,
                LastTouchedBy = LastTouchedBy,
                DateLastTouched = DateLastTouched,
                DateRowAdded = DateRowAdded,
                IsGivenElsewhere = IsGivenElsewhere,
                PatientRefused = PatientRefused,
                VISname = VISname,
                VISversion = VISversion,
                VISDateGiven = VISDateGiven,
                Deleted = Deleted,
                DateSentToRegistry = DateSentToRegistry,
                PatientParentRefused = PatientParentRefused,
                PatientHadInfection = PatientHadInfection,
                HowMigrated = HowMigrated,
                Reaction = Reaction,
                ReactionDate = ReactionDate,
                Locations = Locations,
                NIP001 = NIP001,
                ImmunizationId = ImmunizationId
            };

            return domain;
        }

        public ListHMsPoco() { }

        public ListHMsPoco(ListHMsDomain domain)
        {
            ListHMID = domain.ListHMID;
            PatientID = domain.PatientID;
            HMruleName = domain.HMruleName;
            HmRuleID = domain.HmRuleID;
            VaccineID = domain.VaccineID;
            VaccineName = domain.VaccineName;
            CompositeVaccineID = domain.CompositeVaccineID;
            LotNo = domain.LotNo;
            DateGiven = domain.DateGiven;
            RecordedBy = domain.RecordedBy;
            Volume = domain.Volume;
            Route = domain.Route;
            Site = domain.Site;
            Manufacturer = domain.Manufacturer;
            Expiration = domain.Expiration;
            Comment = domain.Comment;
            Sequence = domain.Sequence;
            Result = domain.Result;
            Type = domain.Type;
            CPT = domain.CPT;
            LastTouchedBy = domain.LastTouchedBy;
            DateLastTouched = domain.DateLastTouched;
            DateRowAdded = domain.DateRowAdded;
            IsGivenElsewhere = domain.IsGivenElsewhere;
            PatientRefused = domain.PatientRefused;
            VISname = domain.VISname;
            VISversion = domain.VISversion;
            VISDateGiven = domain.VISDateGiven;
            Deleted = domain.Deleted;
            DateSentToRegistry = domain.DateSentToRegistry;
            PatientParentRefused = domain.PatientParentRefused;
            PatientHadInfection = domain.PatientHadInfection;
            HowMigrated = domain.HowMigrated;
            Reaction = domain.Reaction;
            ReactionDate = domain.ReactionDate;
            Locations = domain.Locations;
            NIP001 = domain.NIP001;
            ImmunizationId = domain.ImmunizationId;
        }
    }
}
