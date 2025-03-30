using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class LabDemographicsPoco
    {
        public string LabLocationCode { get; set; }
        public string LabCompany { get; set; }
        public DateTime CreatedDt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? LastUpdDt { get; set; }
        public int? LastUpdBy { get; set; }
        public string LabName { get; set; }
        public string LabAddress1 { get; set; }
        public string LabAddress2 { get; set; }
        public string LabCity { get; set; }
        public string LabState { get; set; }
        public string LabZip { get; set; }
        public string LabPhone { get; set; }
        public string LabDirectorTitle { get; set; }
        public string LabDirectorNameLast { get; set; }
        public string LabDirectorNameFirst { get; set; }
        public string LabDirectorNameMI { get; set; }
        public string LabDirectorDegree { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public string FacilityUniversalId { get; set; }
        public string FacilityUniversalIdType { get; set; }
        public string LabDirectorSuffix { get; set; }
        public string LabDirectorPrefix { get; set; }
        public string LabDirectorId { get; set; }
        public string LabLocationIdTypeCode { get; set; }
        public string LabCountry { get; set; }
        public string LabCountyParish { get; set; }
        public int LabDemographicsId { get; set; }
        public string SpecimenNbr { get; set; }

        public LabDemographicsDomain MapToDomainModel()
        {
            LabDemographicsDomain domain = new LabDemographicsDomain
            {
                LabLocationCode = LabLocationCode,
                LabCompany = LabCompany,
                CreatedDt = CreatedDt,
                CreatedBy = CreatedBy,
                LastUpdDt = LastUpdDt,
                LastUpdBy = LastUpdBy,
                LabName = LabName,
                LabAddress1 = LabAddress1,
                LabAddress2 = LabAddress2,
                LabCity = LabCity,
                LabState = LabState,
                LabZip = LabZip,
                LabPhone = LabPhone,
                LabDirectorTitle = LabDirectorTitle,
                LabDirectorNameLast = LabDirectorNameLast,
                LabDirectorNameFirst = LabDirectorNameFirst,
                LabDirectorNameMI = LabDirectorNameMI,
                LabDirectorDegree = LabDirectorDegree,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                FacilityUniversalId = FacilityUniversalId,
                FacilityUniversalIdType = FacilityUniversalIdType,
                LabDirectorSuffix = LabDirectorSuffix,
                LabDirectorPrefix = LabDirectorPrefix,
                LabDirectorId = LabDirectorId,
                LabLocationIdTypeCode = LabLocationIdTypeCode,
                LabCountry = LabCountry,
                LabCountyParish = LabCountyParish,
                LabDemographicsId = LabDemographicsId,
                SpecimenNbr = SpecimenNbr
            };

            return domain;
        }

        public LabDemographicsPoco() { }

        public LabDemographicsPoco(LabDemographicsDomain domain)
        {
            LabLocationCode = domain.LabLocationCode;
            LabCompany = domain.LabCompany;
            CreatedDt = domain.CreatedDt;
            CreatedBy = domain.CreatedBy;
            LastUpdDt = domain.LastUpdDt;
            LastUpdBy = domain.LastUpdBy;
            LabName = domain.LabName;
            LabAddress1 = domain.LabAddress1;
            LabAddress2 = domain.LabAddress2;
            LabCity = domain.LabCity;
            LabState = domain.LabState;
            LabZip = domain.LabZip;
            LabPhone = domain.LabPhone;
            LabDirectorTitle = domain.LabDirectorTitle;
            LabDirectorNameLast = domain.LabDirectorNameLast;
            LabDirectorNameFirst = domain.LabDirectorNameFirst;
            LabDirectorNameMI = domain.LabDirectorNameMI;
            LabDirectorDegree = domain.LabDirectorDegree;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            FacilityUniversalId = domain.FacilityUniversalId;
            FacilityUniversalIdType = domain.FacilityUniversalIdType;
            LabDirectorSuffix = domain.LabDirectorSuffix;
            LabDirectorPrefix = domain.LabDirectorPrefix;
            LabDirectorId = domain.LabDirectorId;
            LabLocationIdTypeCode = domain.LabLocationIdTypeCode;
            LabCountry = domain.LabCountry;
            LabCountyParish = domain.LabCountyParish;
            LabDemographicsId = domain.LabDemographicsId;
            SpecimenNbr = domain.SpecimenNbr;
        }
    }
}
