using System;

namespace DataAccess.SQLPocos
{
    // These are simplified models for testing
    // The actual implementation would use the existing POCO classes
    
    /// <summary>
    /// Demographics information for a patient
    /// </summary>
    public class DemographicsPoco
    {
        public int PatientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Sex { get; set; }
        public DateTime? DOB { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string SSN { get; set; }
        public string Race { get; set; }
        public string Language { get; set; }
    }
    
    /// <summary>
    /// Patient index information
    /// </summary>
    public class PatientIndexPoco
    {
        public int PatientID { get; set; }
        public string PatientGUID { get; set; }
        public string ChartNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Active { get; set; }
    }
    
    /// <summary>
    /// Patient race information
    /// </summary>
    public class ListPatientRacesPoco
    {
        public int PatientID { get; set; }
        public int RaceID { get; set; }
        public string Race { get; set; }
        public bool IsPrimary { get; set; }
    }
    
    /// <summary>
    /// Patient language information
    /// </summary>
    public class ListPatientLanguagesPoco
    {
        public int PatientID { get; set; }
        public int LanguageID { get; set; }
        public string Language { get; set; }
        public bool IsPrimary { get; set; }
    }
}