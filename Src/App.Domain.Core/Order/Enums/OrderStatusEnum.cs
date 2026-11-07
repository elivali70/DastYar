namespace App.Domain.Core.Order.Enums
{
    public enum OrderStatusEnum
    {
        WaitingForProposals = 1,
        WaitingForSpecialistSelection = 2,
        WaitingForSpecialistArrival = 3,
        InProgress = 4,
        Completed = 5,
        Paid = 6,
        Cancelled = 7
    }
}
