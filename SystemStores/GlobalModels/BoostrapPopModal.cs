using static SystemStores.ENUMData.EnumGlobal;

namespace SystemStores.GlobalModels
{
    public class BoostrapPopModal : ModalFooter
    {
        public string ModalTitle { get; set; }
    }
    public class ModalFooter : DropDownListModal
	{
        public SubmitButtonText SubmitButtonText { get; set; }
        public SubmitButtonType SubmitButtonType { get; set; }
        public CancelButtonText CancelButtonText { get; set; }
        public SubmitButtonID SubmitButtonID { get; set; }
        public CancelButtonID CancelButtonID { get; set; }
        public bool OnlyCancelButton { get; set; }
    }
}
