namespace TestForm.Repository.DTOs
{
    public class DropDownList
    {
        public int Id { get; set; }
        public string text { get; set; }
    }
    public class FormDataList
    {
        public Int64 id { get; set; }
        public string toolNo { get; set; }
        public string station { get; set; }
        public string positionNo { get; set; }
        public string reworkNo { get; set; }
        public string modelNo { get; set; } 
        public Int64 doneQuantity { get; set; } = 0;

        public Int64 givenQuantity { get; set; }
    }
    
    public class UT_ExpeditorForm
    {
        public long PkId { get; set; }
        public long POItemID { get; set; }
        public int OperationId { get; set; }
        public string entryBy { get; set; }
        public DateTime entryDate { get; set; }
        public int totalQuantity { get; set; }
        public int? doneQuantity { get; set; }
        public bool isActive { get; set; }
        public bool isCompleted { get; set; }
        public int POId { get; set; }
    }


}

