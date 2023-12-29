namespace SystemStores.ENUMData
{
    public static class EnumGlobal
    {
        public enum SignInStatus
        {
            Success = 0,
            LockedOut = 1,
            RequiresVerification = 2,
            Failure = 3,
            UserNameEmpty = 4,
            PasswordEmpty = 5,
            Exception = 6,
            ModelError = 7
        }

        public enum RoleType : int
        {
            SuperUser = 0,
            Admin = 1,
            RootUser = 2,
            NormalUser = 3,
            SectionAdmin = 4
        }

        public enum CRUDType
        {
            CREATE = 0,
            READ = 1,
            UPDATE = 2,
            DELETE = 3,
            DOWNLOAD = 4,
            UPLOAD = 5,
            PRINT = 6,
            EXPORT = 7,
            CANCEL = 8,
            RESET = 9,
            SEARCH = 10
        }

        public enum Pagination
        {
            PageNumber = 1,
            PageSize = 20,
            Id = 11,
            DESC = 12,
            ASC = 13
        }

        public enum NepaliMonths
        {
            Unknown = 0,
            बैशाख = 01,
            जेठ = 02,
            असार = 03,
            श्रावण = 04,
            भदौ = 05,
            आश्विन = 06,
            कार्तिक = 07,
            मंसिर = 08,
            पुष = 09,
            माघ = 10,
            फाल्गुन = 11,
            चैत्र = 12
        }

        public enum NepaliMasanth
        {
            Asar = 1,
            Aswin = 2,
            Poush = 3,
            Chaitra = 4
        }

        public enum AlertNotificationType
        {
            success,
            warning,
            danger,
            error,
            info
        }

        public enum ZKConnectVia
        {
            TCPIP = 0,
            USB = 1,
            SERIALPORT = 2
        }

        public enum SubmitButtonType
        {
            submit,
            button,
            reset
        }

        public enum SubmitButtonText
        {
            Submit,
            Print,
            Download,
            Upload,
            Export,
            Create,
            View,
            Update,
            Delete,
            Search,
            Cancel
        }

        public enum CancelButtonText
        {
            Cancel,
            Remove,
            Reset
        }

        public enum SubmitButtonID
        {
            btnSubmit,
            btnPrint,
            btnDownload,
            btnUpload,
            btnExport,
            btnView
        }

        public enum CancelButtonID
        {
            btnCancel,
            btnRemove,
            btnReset
        }

        public enum UserErrorMessage
        {
            USERNAMEEMPTY = 0,
            USERNAMEEXIST = 1,
            PISNUMBEREMPTY = 2,
            PISNUMBEREXIST = 3,
            IDENROLLEMPTY = 4,
            IDENROLLEXIST = 5,
            NATIONALIDENTITYEMPTY = 6,
            NATIONALIDENTITYEXIST = 7,
            IMAGESIZE = 8,
            MOBILEEXIST = 9,
            FromToGreaterThanToDate = 10,
            EXISTLeave = 11,
            SAMEYEAR = 12
        }

        public enum Priority
        {
            High,
            Medium,
            Low
        }

        public enum CommunicationProtocol
        {
            TCP = 1,
            UDP = 2
        }

        public enum DeviceModel : int
        {
            REALAND = 1,
            ZK = 2,
            REALTIME = 3
        }

        public enum FingerIndex : int
        {
            LeftThumb = 0,
            LeftPointerFinger = 1,
            LeftMiddleFinger = 2,
            LeftRingFinger = 3,
            LeftPinkyFinger = 4,
            RightThumb = 5,
            RightPointerFinger = 6,
            RightMiddleFinger = 7,
            RightRingFinger = 8,
            RightPinkyFinger = 9
        }

        public enum ActiveDeActiveEmployee
        {
            सक्रिय=0,
            निस्क्रिय=1
        }
    }
}

