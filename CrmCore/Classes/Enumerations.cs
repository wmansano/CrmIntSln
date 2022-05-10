namespace CrmCore
{
    public class Enumerations
    {
        public enum EventStatus
        {
            Registered = 0,
            Checkedin = 1,
            Attended = 2,
            Canceled = 3,
        }

        public enum TypeEnum 
        { 
            Contact 
        };

        public enum DbSettingTypes {
            BoolType = 0,
            IntType = 1,
            StringType = 2,
            DateTimeType = 3,
        }

        public enum DbSettingsEnum
        {
            RunCrmEvents = 0,
            RunCrmBarcodes = 1,
            BackLoadPriorDays = 2,
            RunCrmUpserts = 3,
            RunCrmDownloads = 4,
            RunCrmDeletions = 5,
            RunCrmCommunications = 6,
            RunCrmIntegration = 7,
            RunCrmBackLoad = 8,
            BackLoadBatchSize = 9,
            RunCrmDevelopment = 10,
            ProcessForms = 11,
            RunAcknowledgements = 12,
        }

        public enum SoqlEnums
        {
            DownloadContacts = 0,
            DownloadTerms = 1,
            DownloadTasks = 2,
            DownloadInquiries = 3,
            DownloadAffiliations = 4,
            DownloadProgInterestFlags = 5,
            DownloadSrvsInterestFlags = 6,
            DownloadFlags = 7,
            DownloadEmailCampaigns = 8,
            DownloadEmailBroadcasts = 9,
            DownloadEmailTemplates = 10,
            DownloadEvents = 11,
            DownloadEventRegistrations = 12,
            DownloadActivityExtenders = 13,
            DownloadCourses = 14,
            DownloadCourseOfferings = 15,
            DownloadPrograms = 16,
            DownloadCourseConnections = 17,
            DownloadAccounts = 18,
            DownloadProgramEnrollments = 19,
            DownloadApplications = 20,
            DownloadReports = 21,
            GetEmailRecipients = 22,
            DownloadRoleValues = 23,
            DownloadSiteAssignments = 24,
            DownloadResponseSets = 25,
            DownloadQuestionSets = 26,
            DownloadQuestionResponses = 27,
            DownloadQuestionAssignments = 28,
            DownloadQuestions = 29,
            DownloadDynamicContent = 30,
            GetAffiliationByStudentProgram = 31,
            GetCourseBySisCourseId = 32,
            GetTermBySisTermCode = 33,
            DownloadInquiryPrograms = 34,
        }

        public enum InsertResults
        {
            Contact = 0,
            Term = 1,
            Affiliation = 2,
            Inquiry = 3,
            Task = 4,
            EmailCampaign = 5,
            EmailBroadcast = 6,
            EmailTemplate = 7,
            Event = 8,
            ActivityExtender = 9,
            EventRegistration = 10,
            Application = 11,
            ProgramEnrollment = 12,
            Account = 13,
            Course = 14,
            CourseOffering = 15,
            CourseConnection = 16,
            RoleValues = 17,
            DynamicContent = 18,
            Report = 19,
            AcademicProgram = 20,
            InquiryProgram,
        }

        public enum UpsertRecordTypes
        {
            UpsertContact = 0,
            UpsertTerm = 1,
            UpsertAffiliation = 2,
            UpsertInquiry = 3,
            UpsertTask = 4,
            UpsertEmailCampaign = 5,
            UpsertEmailBroadcast = 6,
            UpsertEmailTemplate = 7,
            UpsertEvent = 8,
            UpsertActivityExtender = 9,
            UpsertEventRegistration = 10,
            UpsertApplication = 11,
            UpsertProgramEnrollment = 12,
            UpsertAccount = 13,
            UpsertCourse = 14,
            UpsertCourseOffering = 15,
            UpsertCourseConnection = 16,
            UpsertRoleValues = 17, 
        }

        public enum DownloadTypes { 
            NewRecordType = 0,
            LatestModifiedType = 1,
            BackLoadModifiedType = 2,
            SpecificRecordType = 3
        }

        public enum FormSourceTypes
        {
            RCRSF = 0,
            RCRLF = 1,
            RCRDF = 2,
            RCRCT = 3,
            RCRSFAD = 4,
        }
    }
}
