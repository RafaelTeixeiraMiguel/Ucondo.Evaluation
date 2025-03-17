namespace Ucondo.Evaluation.API.Features.Bill
{
    public class CreateBillRequest
    {
        /// <summary>
        /// Gets or sets the bill code. Must be unique and meet format requirements
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Gets or sets if the bill allow payments
        /// </summary>
        public bool AllowPayments { get; set; }
        /// <summary>
        /// Gets or sets bill's type. Must be the same as the parent account if any
        /// </summary>
        public string Type { get; set; }
    }
}
