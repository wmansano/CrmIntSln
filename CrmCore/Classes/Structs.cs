using System;

namespace CrmCore
{
    public struct Settings
    {
        public static readonly bool EnableGlobalLastRun = false;
        public static readonly DateTime GlobalLastRunTime = DateTime.Today.AddDays(-1);
        public static readonly string CrmAdmin = "0051U000004mdtqQAA";
        public static readonly string LcAccountId = "0011U00000MSUz0QAH";
        public static readonly string EslProgIds = "'0011U00000MSVODQA5','0011U00000ckhCAQAY','0011U00000ck8ikQAA','0011U00000MSVONQA5','0011U00000ckhCdQAI','0011U00000ckhCeQAI'";
        public static readonly DateTime MigrationDateTime = Convert.ToDateTime("2019-04-18 11:30");
        public static readonly string CrmEmailAddress = "crmadmin@lethbridgecollege.ca";
    }

    public struct DbSettings {
        public static readonly string RunCrmEvents = "RunCrmEvents";
        public static readonly string RunCrmBarcodes = "RunCrmBarcodes";
        public static readonly string BackLoadPriorDays = "BackLoadPriorDays";
        public static readonly string RunCrmUpserts = "RunCrmUpserts";
        public static readonly string RunCrmDownloads = "RunCrmDownloads";
        public static readonly string RunCrmDeletions = "RunCrmDeletions";
        public static readonly string RunCrmCommunications = "RunCrmCommunications";
        public static readonly string RunCrmIntegration = "RunCrmIntegration";
        public static readonly string RunCrmBackLoad = "RunCrmBackLoad";
        public static readonly string BackLoadBatchSize = "BackLoadBatchSize";
        public static readonly string RunCrmDevelopment = "RunCrmDevelopment";
        public static readonly string ProcessForms = "ProcessForms";
        public static readonly string RunAcknowledgements = "RunAcknowledgements";
        public static readonly string ScheduleEmailBroadcasts = "ScheduleEmailBroadcasts";
        public static readonly string ScheduleBroadcastEmails = "QueueBroadcastEmails";
        public static readonly string SendBroadcastEmails = "SendBroadcastEmails";
    }

    public struct FormSourceTypes
    {
        //public const string MKT20AGS = "MKT20AGS";
        //public const string MKT20AGT = "MKT20AGT";
        public const string mkt20eng = "mkt20eng";
        public const string mkt20trn = "mkt20trn";
        public const string mkt20exp = "mkt20exp";
        public const string mkt20cho = "mkt20cho";
        public const string mkt20ber = "mkt20ber";
        public const string mkt20inv = "mkt20inv";
        public const string mkt20wtt = "mkt20wtt";
        public const string mkt20hea = "mkt20hea";
        public const string mkt20rtc = "mkt20rtc";
        public const string rcrirs1 = "rcrirs1";
        public const string rcrirs1a = "rcrirs1a";
        public const string rcrirs1b = "rcrirs1b";
        public const string rcrirs2 = "rcrirs2";
        public const string rcrirs3 = "rcrirs3";
        public const string rcrirs4 = "rcrirs4";
    }

    public struct RequestParameters {
        public const string UtmSource = "utm_source";
        public const string ProgramId = "program_id";
        public const string InquiryId = "inquiry_id";
        public const string EnableIframe = "iframe";
    }

    public struct FormSubmissionTypes
    {
        public const string LandingPage = "LandingPage";
        public const string RCRIRS = "RCRIRS";
    }

    public struct EmailTemplates {
        public const string RCIR_IR_IL_PRG = "00X1U000000NXtEUAW";
    }

    public struct SfRecordTypes
    {
        public static readonly string Account = "Account";
        public static readonly string ActivityExtender = "ActivityExtender";
        public static readonly string Event = "Event";
        public static readonly string Task = "Task";
        public static readonly string Affiliation = "Affiliation";
        public static readonly string Contact = "Contact";
        public static readonly string CourseOffering = "CourseOffering";
        public static readonly string CourseConnection = "CourseConnection";
        public static readonly string Course = "Course";
        public static readonly string EmailBroadcast = "EmailBroadcast";
        public static readonly string EmailCampaign = "EmailCampaign";
        public static readonly string EventRegistration = "EventRegistration";
        public static readonly string Inquiry = "Inquiry";
        public static readonly string Application = "Application";
        public static readonly string Unrecognized = "Unrecognized";
        public static readonly string ProgramEnrollment = "ProgramEnrollment";
        public static readonly string InquiryProgram = "InquiryProgram";
    }

    public struct LastModified
    {
        public static readonly string Account = "Account";
        public static readonly string ActivityExtender = "ActivityExtender";
        public static readonly string Event = "Event";
        public static readonly string EmailTemplate = "EmailTemplate";
        public static readonly string Task = "Task";
        public static readonly string Term = "Term";
        public static readonly string Program = "Program";
        public static readonly string Report = "Report";
        public static readonly string RoleValue = "RoleValue";
        public static readonly string Affiliation = "Affiliation";
        public static readonly string Contact = "Contact";
        public static readonly string CourseOffering = "CourseOffering";
        public static readonly string CourseConnection = "CourseConnection";
        public static readonly string Course = "Course";
        public static readonly string EmailBroadcast = "EmailBroadcast";
        public static readonly string EmailCampaign = "EmailCampaign";
        public static readonly string EventRegistration = "EventRegistration";
        public static readonly string Inquiry = "Inquiry";
        public static readonly string Application = "Application";
        public static readonly string ProgramEnrollment = "ProgramEnrollment";
        public static readonly string DynamicContent = "DynamicContent";
        public static readonly string InquiryProgram = "InquiryProgram";
    }

    public struct BroadcastStatus
    {
        public static readonly string Success = "Success";
        public static readonly string Failure = "Failure";
        public static readonly string Pending = "Pending";
        public static readonly string Cancelled = "Cancelled";
        public static readonly string Scheduled = "Scheduled";
        public static readonly string Completed = "Completed";
        public static readonly string Created = "Created";
    }

    public struct Weekdays
    {
        public static readonly string Monday = "Monday";
        public static readonly string Tuesday = "Tuesday";
        public static readonly string Wednesday = "Wednesday";
        public static readonly string Thursday = "Thursday";
        public static readonly string Friday = "Friday";
        public static readonly string Saturday = "Saturday";
        public static readonly string Sunday = "Sunday";
    }

    public struct AccountTypes
    {
        public static readonly string CollegeDepartment = "lc_university_department";
        public static readonly string AcademicProgram = "lc_academic_program";
        public static readonly string Administrative = "lc_administrative";
        public static readonly string BusinessOrganization = "lc_business_organization";
        public static readonly string EducationalInstitution = "lc_educational_organization";
        public static readonly string HouseholdAccount = "lc_household";
        public static readonly string SportsOrganization = "lc_sports_organization";
    }

    public struct PickListValues
    {
        public static readonly string PreferredEmailType = "Preferred";
        public static readonly string BothEmailType = "Both";
        public static readonly string CollegeEmailType = "College";
        public static readonly string AlternateEmailType = "Alternate";
    }

    public struct AccountRecordTypes
    {
        public static readonly string AcademicProgram = "0121U000001MGdWQAW";
        public static readonly string Administrative = "0121U000001MGdUQAW";
        public static readonly string BusinessOrganization = "0121U000001MGdYQAW";
        public static readonly string CceBusinessAccount = "0121U000000eAyvQAE";
        public static readonly string EducationalInstitution = "0121U000001MGdZQAW";
        public static readonly string HouseholdAccount = "0121U000001MGdaQAG";
        public static readonly string IsaAccount = "0121U000000Sh8TQAS";
        public static readonly string PlacementAccount = "0121U000000eAuKQAU";
        public static readonly string SportsOrganization = "0121U000001MGdXQAW";
        public static readonly string UniversityDepartment = "0121U000001MGdVQAW";
    }

    public struct AffiliationRecordTypes {
        public const string StudentProgramAffiliation = "0121U000000ShBIQA0";
        public const string DefaultAffiliation = "0121U000000ShBSQA0";
        public const string CceBusinessAffiliation = "0121U000000ShBcQAK";
        public const string CceCorporateAffiliation = "0121U000000ShBXQA0";
        public const string PlacementAffiliation = "0121U000000ShBNQA0";
        public const string ISAAffiliation = "0121U000000Sib4QAC";
    }

    public struct ContactRecordTypes {
        public static readonly string DefaultContactRecordType = "0121U000000e7eAQAQ";
        public static readonly string CceContactRecordType = "0121U000000eAy7QAE";
        public static readonly string DevContactRecordType = "0121U000000eB3qQAE";
        public static readonly string DuplicateContactRecordType = "0121U000000ShanQAC";
        public static readonly string IsaContactRecordType = "0121U000000Sh8YQAS";
        public static readonly string PoContactRecordType = "0121U000000eAyHQAU";
        public static readonly string PrivateContactRecordType = "0121U000000ShasQAC";
    }

    public struct PreferredEmailTypes {
        public static readonly string UniversityEmailType = "University";
        public static readonly string AlternateEmailType = "Alternate";
        public static readonly string WorkEmailType = "Work";
    }

    public struct EmailReminderDateTypes
    {
        public static readonly string Dayof = "Day of";
        //public static readonly string 1WeekdayPrior = "1 Weekday Prior";
        //public static readonly string 2WeekdayPrior = "2 Weekday Prior";
        //public static readonly string 3WeekdayPrior = "3 Weekday Prior";
        //public static readonly string 4WeekdayPrior = "4 Weekday Prior";
        //public static readonly string 5WeekdayPrior = "5 Weekday Prior";
    }
}
