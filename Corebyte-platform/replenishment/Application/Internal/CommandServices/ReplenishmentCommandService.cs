using Corebyte_platform.replenishment.Domain.Model.Commands;
using Corebyte_platform.replenishment.Domain.Respositories;
using Corebyte_platform.replenishment.Domain.Services;
using Corebyte_platform.Shared.Domain.Repositories;

namespace Corebyte_platform.replenishment.Application.Internal.CommandServices;

public class ReplenishmentCommandService(IReplenishmentRepository repository, IUnitOfWork unitOfWOrk): IReplenishmentCommandService
{
     public async Task<Domain.Model.Aggregate.Replenishment?> Handle(CreateReplenishmentCommand command)
    {
        var replenishment = new Domain.Model.Aggregate.Replenishment (command);
        /*
         * Restricciones 
         */
        await repository.AddAsync(replenishment);
        await unitOfWOrk.CompleteAsync();
        return replenishment;
    }
/*
 *     public async Task<OrderRequests?> Handle(UpdateOrderRequestsByIdCommand command)
    {
        var orderRequests = await orderRequestsRepository.FindByIdAsync(command.Id);
        if (orderRequests is null) throw new Exception("Order Request not found");
        var orderRequests2 = await orderRequestsRepository.FindByOrderNumberAsync(command.OrderNumber);
        if (orderRequests2 != null && orderRequests2.OrderNumber != command.OrderNumber) throw new Exception("Order Request with Order Number already exists");
        if (command.ConsumerPhone.Length != 9) throw new Exception("Consumer Phone must have 9 digits");
        if (command.ProducerPhone.Length != 9) throw new Exception("Producer Phone must have 9 digits");
        orderRequests.UpdateOrderRequestsById(command);
        orderRequestsRepository.Update(orderRequests);
        await unitOfWOrk.CompleteAsync();
        return orderRequests;
    }
 */
    public async Task<Domain.Model.Aggregate.Replenishment?> Handle(UpdateReplenishmentByIdCommand command)

    {
        var replenishment = await repository.FindByIdAsync(command.Id);
        if (replenishment is null) throw new Exception("Replenishment not found"); 
        repository.Update(replenishment);
        await unitOfWOrk.CompleteAsync();
        return replenishment;
    }
    public async Task<Domain.Model.Aggregate.Replenishment?> Handle(DeleteReplenishmentCommand command)
    {
        var replenishment = await repository.FindByIdAsync(command.Id);
        if (replenishment is null) throw new Exception("Replenishment not found");
        repository.Remove(replenishment);
        await unitOfWOrk.CompleteAsync();
        return replenishment;
    }
}