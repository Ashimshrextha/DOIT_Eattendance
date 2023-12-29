using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemModels.Auditable;

namespace SystemModels.Reports
{
    [Table("UserLogs")]
    public class UserLogsModel : EntityId<long>
    {
        [Display(Name = "लगइन")]
        public Nullable<System.Guid> IdLogin { get; set; }

        [Display(Name = "Session Id")]
        public string SessionId { get; set; }

        [Display(Name = "पर्योगकर्ता नाम")]
        public string UserName { get; set; }

        [Display(Name = "प्रयोगकर्ताको पुरा नाम")]
        public string FullName { get; set; }

        [Display(Name = "लगइन समय")]
        public System.DateTime LoginTime { get; set; }

        [Display(Name = "लगआउट समय")]
        public Nullable<System.DateTime> LogoutTime { get; set; }

        [Display(Name = "क्षेत्र नाम")]
        public string Area { get; set; }

        [Display(Name = "नियन्त्रक नाम")]
        public string Controller { get; set; }

        [Display(Name = "कार्य नाम")]
        public string ActionName { get; set; }

        [Display(Name = "उपकरण नाम")]
        public string DeviceName { get; set; }

        [Display(Name = "उपकरण ओएस")]
        public string DeviceOS { get; set; }

        [Display(Name = "ग्राहक ब्राउजर")]
        public string BrowserName { get; set; }

        [Display(Name = "ग्राहक ब्राउजर भरसन")]
        public string BrowserVersion { get; set; }

        [Display(Name = "ग्राहक स्थान")]
        public string LocationName { get; set; }

        [Display(Name = "एक्सेस युआरयल")]
        public string AccessURL { get; set; }

        [Display(Name = "ईलिगल एक्सेस")]
        public string IllegalAccessMessage { get; set; }

        [Display(Name = "इभेन्ट मेसेज")]
        public string EventMessage { get; set; }

        [Display(Name = "एरर मेसेज")]
        public string ErrorMessage { get; set; }

        [Display(Name = "टर्मिनल आई.पी")]
        [MaxLength(250)]
        public string TerminalIP { get; set; }

        [Display(Name = "सिर्जना मिति")]
        [DataType(DataType.DateTime)]
        public System.DateTime CreatedOn { get; set; }
    }
}
