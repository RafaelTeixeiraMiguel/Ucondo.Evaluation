namespace Ucondo.Evaluation.API.Features.Bill
{
    public class CreateBillResponse
    {
        /// <summary>
        /// The unique identifier of the created bill
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// The unique code of the created bill
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Check if the created bill allow payments
        /// </summary>
        public bool AllowPayments { get; set; }
        /// <summary>
        /// Created bill's type
        /// </summary>
        public string Type { get; set; }
    }
}
